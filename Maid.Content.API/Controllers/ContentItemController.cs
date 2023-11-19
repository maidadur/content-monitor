namespace Maid.Content.API.Controllers
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Content.DB;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Identity.Web.Resource;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	[Route("api/contentitem")]
	[ApiController]
	[Authorize]
	[RequiredScope(AcceptedScope = new []{ "tasks.read", "tasks.write" })]
	public class ContentItemController : BaseApiController<ContentItemInfo>
	{
		public ContentItemController(IEntityRepository<ContentItemInfo> repository)
			: base(repository) {
		}

		[HttpPost("contentinfo/{id:guid}")]
		public async Task<ActionResult<IEnumerable<ContentItemInfo>>> GetByContentInfoAsync(Guid id, SelectOptions options) {
			return Ok(await EntityRepository.GetByAsync(i => i.ContentInfoId == id, options));
		}
	}
}