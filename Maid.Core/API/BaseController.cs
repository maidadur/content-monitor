namespace Maid.Core
{
	using Maid.Core.DB;
	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class BaseApiController<T>: ControllerBase
		where T: BaseEntity
	{

		protected IEntityRepository<T> EntityRepository;

		public BaseApiController(IEntityRepository<T> entityRepository) {
			EntityRepository = entityRepository;
		}

		[HttpGet("/ping")]
		public ActionResult Ping() {
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<T>>> GetAllItems() {
			return Ok(await EntityRepository.GetAllAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<T>> GetItem(Guid id) {
			var item = await EntityRepository.GetAsync(id);
			if (item == null) {
				return NotFound();
			}
			return item;
		}

		[HttpPost()]
		public void AddItem(T item) { 
			EntityRepository.Create(item);
		}
	}
}
