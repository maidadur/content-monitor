import { Component, HostListener } from "@angular/core";
import { Location } from "@angular/common";
import { ActivatedRoute } from "@angular/router";
import { BinanceOrder } from "@app/entity/trading/binance-order";
import { BasePageComponent } from "../../base/base-page.component";
import { BinanceOrderService } from "@app/services/trading/binance-order.service";
import { EmotionOption, EMOTIONS } from "@app/consts/emotions";
import { TagOption, TRADE_TAGS } from "@app/consts/trade-tags";
import { BinanceOrderEmotionService } from "@app/services/trading/binance-order-emotion.service.";
import { BinanceOrderEmotion } from "@app/entity/trading/binance-order-emotion";
import { Guid } from "guid-typescript";
import { BinanceOrderTagService } from "@app/services/trading/binance-order-tag.service.";
import { BinanceOrderTag } from "@app/entity/trading/binance-order-tag";

@Component({
	selector: "app-order-page",
	templateUrl: "./order-page.component.html",
	styleUrl: "./order-page.component.less",
	standalone: false,
})
export class OrderPageComponent extends BasePageComponent<BinanceOrder> {
	public model: BinanceOrder = new BinanceOrder();

	public emotions = EMOTIONS;
	public tradeTags = TRADE_TAGS;
	public selectedEmotions: BinanceOrderEmotion[] = [];
	public selectedTradeTags: BinanceOrderTag[] = [];

	constructor(
		public service: BinanceOrderService,
		public router: ActivatedRoute,
		public location: Location,
		private _emotionService: BinanceOrderEmotionService,
		private _tagService: BinanceOrderTagService,
	) {
		super(service, router, location);
	}

	@HostListener("document:paste", ["$event"])
	public handlePaste(event: ClipboardEvent) {
		const items = event.clipboardData?.items;
		for (let i = 0; i < items?.length; i++) {
			const item = items[i];
			if (item.type.indexOf("image") !== -1) {
				this.loadImage(item.getAsFile());
			}
		}
	}

	public loadData(): void {
		super.loadData();
		this._emotionService.loadEmotionsByOrderId(this.model.id).subscribe((emotions) => {
			this.selectedEmotions = emotions;
		});
		this._tagService.loadTagsByOrderId(this.model.id).subscribe((tags) => {
			this.selectedTradeTags = tags;
		});
	}

	public loadImage(file: File) {
		const reader = new FileReader();
		reader.onload = () => {
			this.model.imageUrl = reader.result as string;
		};
		reader.readAsDataURL(file);
	}

	public showFullscreenImage() {
		const imageElement = document.createElement("img");
		imageElement.src = this.model.imageUrl;
		imageElement.style.position = "fixed";
		imageElement.style.top = "0";
		imageElement.style.left = "0";
		imageElement.style.width = "100%";
		imageElement.style.height = "100%";
		imageElement.style.objectFit = "contain";
		imageElement.style.backgroundColor = "rgba(0, 0, 0, 0.8)";
		imageElement.style.zIndex = "1000";
		imageElement.id = "fullscreenImage";

		imageElement.addEventListener("click", () =>
			this.closeFullscreenImage()
		);
		document.body.appendChild(imageElement);
	}

	public closeFullscreenImage() {
		const imageElement = document.getElementById("fullscreenImage");
		if (imageElement) {
			document.body.removeChild(imageElement);
		}
	}

	public isEmotionSelected(emotion: EmotionOption): boolean {
		return this.selectedEmotions.some(
			(selected) => selected.emotion === emotion.emoji
		);
	}

	public isTagSelected(tag: TagOption): boolean {
		return this.selectedTradeTags.some(
			(selected) => selected.tag === tag.emoji
		);
	}

	public toggleEmotion(emotion: EmotionOption): void {
		if (this.isEmotionSelected(emotion)) {
			const emotionToDelete = this.selectedEmotions.filter(
				(selected) => selected.emotion === emotion.emoji
			);
			this.selectedEmotions = this.selectedEmotions.filter(
				(selected) => selected.emotion !== emotion.emoji
			);
			this._emotionService.delete(Guid.parse(emotionToDelete[0].id)).subscribe();
		} else {
			const newEmotion = new BinanceOrderEmotion({
				id: Guid.create().toString(),
				binanceOrderId: this.model.id,
				emotion: emotion.emoji,
			});
			this.selectedEmotions.push(newEmotion);
			this._emotionService.add(newEmotion).subscribe();
		}
	}

	public toggleTradeTag(tag: TagOption): void {
		if (this.isTagSelected(tag)) {
			const tagToDelete = this.selectedTradeTags.filter(
				(selected) => selected.tag === tag.emoji
			);
			this.selectedTradeTags = this.selectedTradeTags.filter(
				(selected) => selected.tag !== tag.emoji
			);
			this._tagService.delete(Guid.parse(tagToDelete[0].id)).subscribe();
		} else {
			const newTag = new BinanceOrderTag({
				id: Guid.create().toString(),
				binanceOrderId: this.model.id,
				tag: tag.emoji,
			});
			this.selectedTradeTags.push(newTag);
			this._tagService.add(newTag).subscribe();
		}
	}
}
