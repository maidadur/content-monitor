import { Directive, ElementRef } from '@angular/core';

@Directive({ selector: 'img' })
export class LazyImgDirective {
	constructor({ nativeElement }: ElementRef<HTMLImageElement>) {
		if ('loading' in HTMLImageElement.prototype) {
			nativeElement.setAttribute('loading', 'lazy');
		}
	}
}