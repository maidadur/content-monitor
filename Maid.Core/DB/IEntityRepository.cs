namespace Maid.Core.DB
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading.Tasks;

	public interface IEntityRepository<TEntity>
	{
		Task<IEnumerable<TEntity>> GetAllAsync();

		Task<IEnumerable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> expression);

		void Create(TEntity entity);

		void Update(TEntity entity);

		void Delete(TEntity entity);

		void Delete(Guid id);

		TEntity Get(Guid id);

		Task SaveAsync();

		void Save();
	}
}
