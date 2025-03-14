import { Component, OnInit, ViewChild, ElementRef, Input, NgZone, Output, EventEmitter } from '@angular/core';
import { DomUtils } from '@app/utils/dom-utils';

@Component({
    selector: 'app-view-port',
    templateUrl: './view-port.component.html',
    styleUrls: ['./view-port.component.css'],
    standalone: false
})
export class ViewPortComponent implements OnInit {

	@ViewChild('viewport')
	private _viewPortEl: ElementRef;

	constructor(private _zone: NgZone) { }

	@Input()
	public scrollableParentDomEl: any;

	@Input()
	public stopPropagation: boolean;

	@Output()
	public onVisible: EventEmitter<any> = new EventEmitter();

	ngOnInit() {
		this._zone.runOutsideAngular(() => {
			this.scrollableParentDomEl.addEventListener('scroll', this.onParentScroll.bind(this));
		});
	}

	public onParentScroll() {
		if (this.stopPropagation) {
			return;
		}
		if (DomUtils.isElementInViewport(this._viewPortEl && this._viewPortEl.nativeElement)) {
			this._zone.run(() => {
				this.onVisible.emit();
			});
		}
	}
}
