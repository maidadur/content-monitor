using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maid.Core
{
	public static partial class DbContextUtils
	{
		public static IQueryable<BaseEntity> Set(this DbContext _context, Type t) {
			return (IQueryable<BaseEntity>)_context.GetType().GetMethod("Set", 1, Type.EmptyTypes).MakeGenericMethod(t).Invoke(_context, null);
		}

		public static IQueryable<TEntity> Include<TEntity>(this IQueryable<TEntity> query, DbContext context)
				where TEntity : BaseEntity {
			IEnumerable<INavigation> navigationProperties = context.Model.FindEntityType(typeof(TEntity)).GetNavigations();
			query.AsNoTracking();
			foreach (INavigation navigationProperty in navigationProperties) {
				if (!(navigationProperty.ClrType.IsGenericType &&
						navigationProperty.ClrType.GetGenericTypeDefinition() == typeof(List<>))) {
					query = query.Include(navigationProperty.Name);
				}
			}
			return query;
		}
	}
}