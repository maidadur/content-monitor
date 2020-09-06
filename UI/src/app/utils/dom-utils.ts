export class DomUtils {

	/**
	 * Checks if dom element is currency visible on screen.
	 * @param el Dom element.
	 */
	public static isElementInViewport(el: any): boolean {
		if (!el) {
			return false;
		}
		var rect = el.getBoundingClientRect();
		return (
			rect.top >= 0 &&
			rect.left >= 0 &&
			rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) && /* or $(window).height() */
			rect.right <= (window.innerWidth || document.documentElement.clientWidth) /* or $(window).width() */
		);
	}

}