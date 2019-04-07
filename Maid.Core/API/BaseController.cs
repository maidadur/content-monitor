namespace Maid.Core
{
	using Microsoft.AspNetCore.Mvc;

	public class BaseApiController: ControllerBase
	{
		[HttpGet("")]
		public ActionResult Ping() {
			return Ok();
		}
	}
}
