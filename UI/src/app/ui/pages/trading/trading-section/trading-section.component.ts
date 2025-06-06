import { Component } from "@angular/core";
import { Location } from "@angular/common";
import { ActivatedRoute, Router } from "@angular/router";
import { BinanceOrderService } from "@app/services/trading/binance-order.service";
import { tap } from "rxjs";
import { BinanceOrder } from "@app/entity/trading/binance-order";

export interface TradingSectionItem {
	id: string;
	caption: string;
	code: string;
	date: Date;
	cleanPnl: number;
	profitable: boolean;
	numberOfTrades?: number;
}

@Component({
	selector: "app-trading-section",
	templateUrl: "./trading-section.component.html",
	styleUrl: "./trading-section.component.less",
	standalone: false,
})
export class TradingSectionComponent {
	public orders: BinanceOrder[] = [];
	public items: TradingSectionItem[] = [];
	public headerCaption: string;
	public mode: string;
	public year: number;
	public month: number;
	public day: number;
	public currentWinRate: number;
	public cleanPnl: number;

	startDate: Date;
	dueDate: Date;
	averagePnl: number;

	constructor(
		private readonly _location: Location,
		private readonly _route: ActivatedRoute,
		private readonly _router: Router,
		private readonly _loadDataService: BinanceOrderService
	) {}

	public ngOnInit(): void {
		this._route.queryParams.subscribe(() => {
			this._initDateValues();
			this._loadDataService
				.loadOrdersByDate(this.startDate, this.dueDate)
				.pipe(
					tap((orders) => {
						this.orders = orders;
						this._initItems();
					})
				)
				.subscribe();
		});
	}

	private _initItems() {
		if (this.mode === "year") {
			this.headerCaption = `${this.year} Year`;
			const monthlyData = Array.from({ length: 12 }, (_, index) => ({
				caption: new Date(this.year, index, 1).toLocaleString(
					"default",
					{ month: "long" }
				),
				code: `${this.year}_${index + 1}`,
				date: new Date(this.year, index, 1),
				cleanPnl: 0,
				profitable: false,
				id: null
			}));

			this.orders.forEach((order) => {
				const orderMonth = new Date(order.time).getMonth();
				monthlyData[orderMonth].cleanPnl += order.cleanPnl;
			});

			this.items = monthlyData.map((data) => ({
				...data,
				profitable: data.cleanPnl > 0,
			}));
		} else if (this.mode === "month") {
			this.headerCaption = `${this.year} ${new Date(
				this.year,
				this.month - 1
			).toLocaleString("default", { month: "long" })}`;
			const daysInMonth = new Date(this.year, this.month, 0).getDate();
			const dailyData = Array.from(
				{ length: daysInMonth },
				(_, index) => ({
					caption: (index + 1).toString(),
					code: `${this.year}_${this.month}_${index + 1}`,
					date: new Date(this.year, this.month - 1, index + 1),
					cleanPnl: 0,
					profitable: false,
					id: null,
					numberOfTrades: 0,
				})
			);

			this.orders.forEach((order) => {
				const orderDate = new Date(order.time).getDate();
				dailyData[orderDate - 1].cleanPnl += order.cleanPnl;
				dailyData[orderDate - 1].numberOfTrades++;
				dailyData[orderDate - 1].profitable = dailyData[orderDate - 1].cleanPnl > 0;
			});

			this._generateEmptyWeekItems(dailyData);
		} else if (this.mode === "day") {
			this.headerCaption = `${this.year} ${new Date(
				this.year,
				this.month - 1
			).toLocaleString("default", { month: "long" })} ${this.day}`;
			this.items = this.orders.map((order) => ({
				id: order.id,
				caption: `${order.side === 'BUY' ? 'LONG' : 'SHORT'} ${order.symbol.replace('USDT', '')} ${new Date(order.time).toTimeString().slice(0, 5)}`,
				code: `${this.year}_${this.month}_${this.day}_${order.id}`,
				date: new Date(order.time),
				cleanPnl: order.cleanPnl,
				profitable: order.cleanPnl > 0,
			}));
		}
		this.currentWinRate = this.orders.filter((item) => item.cleanPnl > 0).length / this.orders.length;
		this.cleanPnl = this.orders.reduce((acc, order) => acc + order.cleanPnl, 0);
		this.averagePnl = this.cleanPnl / this.orders.length;
	}

	private _generateEmptyWeekItems(dailyData: TradingSectionItem[]) {
		var items = dailyData;
		const firstItem = dailyData[0];
		if (firstItem) {
			const firstDayOfWeek = firstItem.date.getDay(); // 0 (Sunday) - 6 (Saturday)
			const daysToAdd = (firstDayOfWeek === 0 ? 6 : firstDayOfWeek - 1); // 0 for Monday, 1 for Tuesday, etc.
			if (daysToAdd > 0) {
				const emptyItems = [];
				for (let i = 0; i < daysToAdd; i++) {
					const date = new Date(firstItem.date);
					date.setDate(date.getDate() - (daysToAdd - i));
					emptyItems.push({
						caption: date.getDate().toString(),
						code: `${date.getFullYear()}_${date.getMonth() + 1}_${date.getDate()}`,
						date,
						cleanPnl: 0,
						id: null,
						numberOfTrades: 0,
					});
				}
				items = [...emptyItems, ...items];
			}
		}

		const lastItem = items[items.length - 1];
		if (lastItem) {
			const lastDayOfWeek = lastItem.date.getDay(); // 0 (Sunday) - 6 (Saturday)
			const daysToAdd = lastDayOfWeek === 0 ? 0 : 7 - lastDayOfWeek;
			if (daysToAdd > 0) {
				const emptyItems = [];
				for (let i = 1; i <= daysToAdd; i++) {
					const date = new Date(lastItem.date);
					date.setDate(date.getDate() + i);
					emptyItems.push({
						caption: date.getDate().toString(),
						code: `${date.getFullYear()}_${date.getMonth() + 1}_${date.getDate()}`,
						date,
						cleanPnl: 0,
						profitable: false,
						id: null,
						numberOfTrades: 0,
					});
				}
				items = [...items, ...emptyItems];
			}
		}
		const result: TradingSectionItem[] = this._generateWeekTotalItems(items);
		this.items = result;
	}

	private _generateWeekTotalItems(items: TradingSectionItem[]) {
		let weekSum = 0;
		let weekStartIndex = 0;
		const result: TradingSectionItem[] = [];
		for (let i = 0; i < items.length; i++) {
			const item = items[i];
			weekSum += item.cleanPnl;
			result.push(item);
			const isLastDayOfWeek = item.date.getDay() === 0 || i === items.length - 1;
			if (isLastDayOfWeek) {
				result.push({
					caption: "Σ",
					code: `week_${item.date.getFullYear()}_${item.date.getMonth() + 1}_${item.date.getDate()}`,
					date: new Date(item.date),
					cleanPnl: weekSum,
					profitable: weekSum > 0,
					id: null,
					numberOfTrades: items.slice(weekStartIndex, i + 1).reduce((acc, d) => acc + (d.numberOfTrades || 0), 0),
				});
				weekSum = 0;
				weekStartIndex = i + 1;
			}
		}
		return result;
	}

	private _initDueDate() {
		if (this.mode == "day") {
			this.dueDate = new Date(this.year, this.month - 1, this.day, 23, 59, 59);
		} else if (this.mode == "month") {
			this.dueDate = new Date(this.year, this.month, 0, 23, 59, 59);
		} else {
			this.dueDate = new Date(this.year + 1, 0, 0, 23, 59, 59);
		}
	}

	private _initStartDate() {
		if (this.mode == "day") {
			this.startDate = new Date(this.year, this.month - 1, this.day, 0, 0, 0);
		} else if (this.mode == "month") {
			this.startDate = new Date(this.year, this.month - 1, 1, 0, 0, 0);
		} else {
			this.startDate = new Date(this.year, 0, 1, 0, 0, 0);
		}
	}

	private _initDateValues() {
		let year = this._route.snapshot.queryParams["year"];
		let month = this._route.snapshot.queryParams["month"];
		let day = this._route.snapshot.queryParams["day"];
		if (day) {
			this.mode = "day";
		} else if (month) {
			this.mode = "month";
		} else {
			this.mode = "year";
		}
		const currentDate = new Date();
		this.year = +(year ?? currentDate.getFullYear());
		this.month = +(month ?? currentDate.getMonth() + 1);
		this.day = +(day ?? currentDate.getDate());
		this._initStartDate();
		this._initDueDate();
	}

	public goBack() {
		this._location.back();
	}

	public onTileClick(item: TradingSectionItem) {
		const dateParts = item.code.split("_").map(Number);
		const year = dateParts[0];
		const month = dateParts[1];
		const day = dateParts[2];
		if (this.mode === "day") {
			this._router.navigate([`/workspace/trading/order/${item.id}`]);
		} else {
			this._router.navigate(["/workspace/trading"], {
				queryParams: { year: year, month: month, day: day },
			});
		}
	}

	public onPreviousClick(): void {
		if (this.mode === "day") {
			const date = new Date(this.year, this.month - 1, this.day - 1);
			this._router.navigate(["/workspace/trading"], {
				queryParams: { year: date.getFullYear(), month: date.getMonth() + 1, day: date.getDate()},
			});
		} else if (this.mode === "month") {
			const date = new Date(this.year, this.month - 2, 1);
			this._router.navigate(["/workspace/trading"], {
				queryParams: { year: date.getFullYear(), month: date.getMonth() + 1},
			});
		} else {
			const date = new Date(this.year - 1, this.month - 1, 1);
			this._router.navigate(["/workspace/trading"], {
				queryParams: { year: date.getFullYear()},
			});
		}
	}

	public onNextClick(): void {
		if (this.mode === "day") {
			const date = new Date(this.year, this.month - 1, this.day + 1);
			this._router.navigate(["/workspace/trading"], {
				queryParams: { year: date.getFullYear(), month: date.getMonth() + 1, day: date.getDate()},
			});
		} else if (this.mode === "month") {
			const date = new Date(this.year, this.month, 1);
			this._router.navigate(["/workspace/trading"], {
				queryParams: { year: date.getFullYear(), month: date.getMonth() + 1},
			});
		} else {
			const date = new Date(this.year + 1, this.month - 1, 1);
			this._router.navigate(["/workspace/trading"], {
				queryParams: { year: date.getFullYear()},
			});
		}
	}
}
