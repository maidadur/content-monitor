namespace Maid.Manga.API.Controllers
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga.DB;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	[Route("api/mangachapter")]
	[ApiController]
	[Authorize]
	public class MangaChapterController : BaseApiController<MangaChapterInfo>
	{
		public MangaChapterController(IEntityRepository<MangaChapterInfo> repository)
			: base(repository) {
		}

		[HttpPost("manga/{id:guid}")]
		public async Task<ActionResult<IEnumerable<MangaChapterInfo>>> GetByMangaIdAsync(Guid id, SelectOptions options) {
			return Ok(await EntityRepository.GetByAsync(i => i.MangaId == id, options));
		}
	}
}