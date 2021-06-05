namespace Maid.Manga
{
	using Microsoft.Extensions.Configuration;

	public class ConfigHelper
	{
		private IConfiguration _conf;

		public ConfigHelper(IConfiguration configuration) {
			_conf = configuration;
		}

		public ServiceConfigrationSection GetServiceConfig(string serviceName) {
			var serviceSection = _conf.GetSection(serviceName);
			if (serviceSection == null) {
				return null;
			}
			ServiceConfigrationSection config = new ServiceConfigrationSection();
			serviceSection.Bind(config);
			return config;
		}
	}
}