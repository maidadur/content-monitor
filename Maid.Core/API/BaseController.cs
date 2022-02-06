﻿namespace Maid.Core
{
	using Maid.Core.DB;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	[EnableCors()]
	public class BaseApiController<T> : ControllerBase
		where T : BaseEntity
	{
		protected IEntityRepository<T> EntityRepository;

		public BaseApiController(IEntityRepository<T> entityRepository) {
			EntityRepository = entityRepository;
		}

		[HttpGet("/ping")]
		public ActionResult Ping() {
			return Ok();
		}

		[HttpPost("list")]
		public async Task<ActionResult<IEnumerable<T>>> GetAllItems(SelectOptions selectOptions) {
			var data = await EntityRepository.GetAllAsync(selectOptions);
			return Ok(data);
		}

		[HttpGet("{id:guid}")]
		public async Task<ActionResult<T>> GetItem(Guid id) {
			var item = await EntityRepository.GetAsync(id);
			if (item == null) {
				return NotFound(id);
			}
			return item;
		}

		[HttpPost()]
		public ActionResult AddItem(T item) {
			if (item == null) {
				return BadRequest("Parameter object is null");
			}
			EntityRepository.Create(item);
			EntityRepository.Save();
			return Ok();
		}

		[HttpPut()]
		public ActionResult EditItem(T item) {
			if (item == null) {
				return BadRequest("Parameter object is null");
			}
			EntityRepository.Update(item);
			EntityRepository.Save();
			return Ok();
		}

		[HttpDelete()]
		public ActionResult DeleteItem(T item) {
			if (item == null) {
				return BadRequest("Parameter object is null");
			}
			EntityRepository.Delete(item);
			EntityRepository.Save();
			return Ok();
		}

		[HttpDelete("{id}")]
		public ActionResult DeleteItem(Guid id) {
			try {
				EntityRepository.Delete(id);
				EntityRepository.Save();
				return Ok();
			} catch (KeyNotFoundException) {
				return NotFound(id);
			}
		}
	}
}