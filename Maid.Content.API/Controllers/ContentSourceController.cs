namespace Maid.Content.API.Controllers
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Content.DB;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Identity.Web.Resource;

	[Route("api/contentsource")]
	[ApiController]
	[Authorize]
	[RequiredScope(AcceptedScope = new[] { "tasks.read", "tasks.write" })]
	public class ContentSourceController : BaseApiController<ContentSource>
	{
		public ContentSourceController(IEntityRepository<ContentSource> entityRepository) : base(entityRepository) {
		}
	}
}