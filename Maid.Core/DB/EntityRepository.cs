namespace Maid.Core.DB
{
	using Maid.Core.Utilities;
	using Microsoft.EntityFrameworkCore;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Threading.Tasks;

	public class EntityRepository : IEntityRepository
	{
		protected DbContext Context { get; set; }

		public EntityRepository(DbContext repositoryContext) {
			Context = repositoryContext;
		}

		public async Task<IEnumerable<BaseEntity>> GetAllAsync(Type type) {
			return await Context.Set(type).ToListAsync();
		}
	}

	public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : BaseEntity
	{
		protected DbContext Context { get; set; }

		public EntityRepository(DbContext repositoryContext) {
			Context = repositoryContext;
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(SelectOptions options = null) {
			options = options ?? new SelectOptions();
			var dbSet = Context.Set<TEntity>();
			if (options.LoadLookups) {
				return await dbSet
					.Include(Context)
					.ApplyOffsetOptions(options)
					.ApplyOrderOptions(options.OrderOptions)
					.ToListAsync();
			}
			return await dbSet
				.ApplyOffsetOptions(options)
				.ApplyOrderOptions(options.OrderOptions)
				.ToListAsync();
		}

		public IEnumerable<TEntity> GetAll(SelectOptions options = null) {
			options = options ?? new SelectOptions();
			var dbSet = Context.Set<TEntity>();
			if (options.LoadLookups) {
				return dbSet
					.Include(Context)
					.ApplyOffsetOptions(options)
					.ApplyOrderOptions(options.OrderOptions)
					.ToList();
			}
			return dbSet
				.ApplyOffsetOptions(options)
				.ApplyOrderOptions(options.OrderOptions)
				.ToList();
		}

		public async Task<IEnumerable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> expression, SelectOptions options = null) {
			options = options ?? new SelectOptions();
			return await Context.Set<TEntity>()
				.ApplyOrderOptions(options.OrderOptions)
				.Where(expression)
				.ApplyOffsetOptions(options)
				.ToListAsync();
		}

		public IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression) {
			return Context.Set<TEntity>()
				.Where(expression)
				.ToList();
		}

		public void Create(TEntity entity) {
			if (entity.CreatedOn == DateTime.MinValue) {
				entity.CreatedOn = DateTime.UtcNow;
			}
			Context.Attach(entity);
			Context.Set<TEntity>().Add(entity);
		}

		public void Update(TEntity entity) {
			Context.Set<TEntity>().Update(entity);
		}

		public void Delete(TEntity entity) {
			Context.Set<TEntity>().Remove(entity);
		}

		public void Delete(Expression<Func<TEntity, bool>> expression) {
			var collection = Context.Set<TEntity>().Where(expression);
			Context.Set<TEntity>().RemoveRange(collection);
		}

		public void Delete(Guid id) {
			var entityToDelete = Context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
			if (entityToDelete != null) {
				Context.Set<TEntity>().Remove(entityToDelete);
			}
		}

		public TEntity Get(Guid id) {
			return Context.Set<TEntity>().AsQueryable()
				.Include(Context)
				.SingleOrDefault(item => item.Id == id);
		}

		public Task<TEntity> GetAsync(Guid id) {
			return Context.Set<TEntity>().AsQueryable()
				.Include(Context)
				.SingleOrDefaultAsync(item => item.Id == id);
		}

		public async Task SaveAsync() {
			await Context.SaveChangesAsync();
		}

		public void Save() {
			Context.SaveChanges();
		}
	}
}