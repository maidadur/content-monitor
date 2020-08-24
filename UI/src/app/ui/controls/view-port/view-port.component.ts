import { Component, OnInit } from '@angular/core';

@Component({
	selector: 'app-view-port',
	templateUrl: './view-port.component.html',
	styleUrls: ['./view-port.component.css']
})
export class ViewPortComponent implements OnInit {

	//@ViewChild('view-port-component')
	private _viewPort;

	constructor() { }

	ngOnInit() {
	}
}
