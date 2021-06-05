using Maid.Core.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Maid.Core.Utilities
{
	public static class RepositoryUtils
	{
		private static LambdaExpression CreateExpression(Type type, string propertyName) {
			var param = Expression.Parameter(type, "x");

			Expression body = param;
			foreach (var member in propertyName.Split('.')) {
				body = Expression.PropertyOrField(body, member);
			}

			return Expression.Lambda(body, param);
		}

		/// <summary>
		/// Sorts the elements of a sequence according to a key and the sort order.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="query" />.</typeparam>
		/// <param name="query">A sequence of values to order.</param>
		/// <param name="key">Name of the property of <see cref="TSource"/> by which to sort the elements.</param>
		/// <param name="ascending">True for ascending order, false for descending order.</param>
		/// <returns>An <see cref="T:System.Linq.IOrderedQueryable`1" /> whose elements are sorted according to a key and sort order.</returns>
		public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, string key, bool ascending = true) {
			if (string.IsNullOrWhiteSpace(key)) {
				return query;
			}

			var lambda = (dynamic)CreateExpression(typeof(TSource), key);

			return ascending
				? Queryable.OrderBy(query, lambda)
				: Queryable.OrderByDescending(query, lambda);
		}

		public static IQueryable<TSource> ApplyOrderOptions<TSource>(this IQueryable<TSource> query, IEnumerable<OrderOptions> orderOptions) {
			if (orderOptions.IsEmpty()) {
				return query;
			}
			for (int i = 0; i < orderOptions.Count(); i++) {
				var option = orderOptions.ElementAt(i);
				query = query.OrderBy(option.Column, option.IsAscending);
			}
			return query;
		}

		public static IQueryable<TSource> ApplyOffsetOptions<TSource>(this IQueryable<TSource> query, SelectOptions selectOptions) {
			if (selectOptions == null) {
				return query;
			}
			if (selectOptions.Count == -1 || selectOptions.Offset == -1) {
				return query;
			}
			return query.Skip(selectOptions.Offset)
				.Take(selectOptions.Count);
		}
	}
}