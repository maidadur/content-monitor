namespace Maid.Core.API
{
	using Maid.Core.DB;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	[ApiController]
	[Route("api/lookup")]
	[EnableCors()]
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
				throw new ArgumentException($"No lookup '{lookupTypeName}'t exists");
			}
			return Ok(await EntityRepository.GetAllAsync(type));
		}
	}
}
