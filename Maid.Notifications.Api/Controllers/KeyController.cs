using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Maid.Notifications.Api.Controllers
{
	[Route("api/key")]
	[ApiController]
	[EnableCors()]
	public class KeyController : ControllerBase
	{

		public KeyController(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public ContentResult Get() {
			return Content(Configuration["Push_Public_Key"], "text/plain");
		}
	}
}
