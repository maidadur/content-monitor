namespace Maid.Manga.API.Controllers
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga.DB;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	[EnableCors("AllowOrigin")]
	[Route("api/mangachapter")]
	[ApiController]
	public class MangaChapterController : BaseApiController<MangaChapterInfo>
	{
		public MangaChapterController(IEntityRepository<MangaChapterInfo> repository)
			: base (repository) {
		}

		[HttpGet("manga/{id:guid}")]
		public async Task<ActionResult<IEnumerable<MangaChapterInfo>>> GetByMangaIdAsync(Guid id) {
			return Ok(await EntityRepository.GetByAsync(i => i.MangaId == id));
		}
	}
}
