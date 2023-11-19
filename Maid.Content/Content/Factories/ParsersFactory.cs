namespace Maid.Content
{
	public interface IParsersFactory
	{
		IContentParser GetParser(string serviceName);
	}

	public class ParsersFactory : IParsersFactory
	{
		public IContentParser GetParser(string serviceName) {
			serviceName = serviceName?.ToLower();
			switch (serviceName) {
				default:
					return new DefaultPageParser();
			}
		}
	}
}