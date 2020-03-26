namespace Maid.Manga
{
	public interface IParsersFactory
	{
		IMangaParser GetParser(string serviceName);
	}

	public class ParsersFactory : IParsersFactory
	{
		public IMangaParser GetParser(string serviceName) {
			serviceName = serviceName.ToLower();
			switch (serviceName) {
				case "fanfox": {
						return new FanFoxParser();
					}
				case "mangarock": {
						return new MangaRockParser();
					}
				default:
					break;
			}
			return null;
		}
	}
}
