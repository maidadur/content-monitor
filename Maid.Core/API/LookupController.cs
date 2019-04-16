using Maid.Core.DB;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maid.Core.API
{
	[ApiController]
	[Route("api/lookup")]
	[EnableCors("AllowOrigin")]
	public class LookupController : ControllerBase
	{

		protected IEntityRepository EntityRepository;

		public LookupController(IEntityRepository entityRepository) {
			EntityRepository = entityRepository;
		}

		[HttpGet("{lookupTypeName}")]
		public async Task<ActionResult<IEnumerable<BaseLookup>>> GetAllItems(string lookupTypeName) {
			var type = LookupTypeManager.Instance.GetLookupType(lookupTypeName);
			if (type == null) {
				return new List<BaseLookup>();
			}
			return Ok(await EntityRepository.GetAllAsync(type));
		}
	}
}
