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
			var data = await EntityRepository.GetAllAsync(new SelectOptions {
				LoadLookups = loadLookups
			});
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
		public ActionResult AddItem(T item) {
			EntityRepository.Create(item);
			EntityRepository.Save();
			return Ok();
		}

		[HttpPut()]
		public ActionResult EditItem(T item) {
			EntityRepository.Update(item);
			EntityRepository.Save();
			return Ok();
		}

		[HttpDelete()]
		public ActionResult DeleteItem(T item) {
			EntityRepository.Delete(item);
			EntityRepository.Save();
			return Ok();
		}

		[HttpDelete("{id}")]
		public ActionResult DeleteItem(Guid id) {
			EntityRepository.Delete(id);
			EntityRepository.Save();
			return Ok();
		}
	}
}
