namespace Maid.Core
{
	using Maid.Core.DB;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	[EnableCors("AllowOrigin")]
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
		public async Task<ActionResult<IEnumerable<T>>> GetAllItems(bool loadLookups = false) {
			var data = await EntityRepository.GetAllAsync(loadLookups);
			return Ok(data);
		}

		[HttpGet("{id:guid}")]
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
			EntityRepository.Save();
		}

		[HttpPut()]
		public void EditItem(T item) {
			EntityRepository.Update(item);
			EntityRepository.Save();
		}

		[HttpDelete()]
		public void DeleteItem(T item) {
			EntityRepository.Delete(item);
			EntityRepository.Save();
		}

		[HttpDelete("{id}")]
		public void DeleteItem(Guid id) {
			EntityRepository.Delete(id);
			EntityRepository.Save();
		}
	}
}
