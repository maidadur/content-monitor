export class UrlUtils {
	public static replaceUrlDomain(url: string) {
		return url.replace("{domain}", window.location.hostname);
	}
}