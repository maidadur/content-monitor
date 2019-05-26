namespace Maid.Core
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public static class CommonUtils
	{
		public static void CheckArgumentNull(this object obj, string message) {
			if (obj == null) {
				throw new ArgumentNullException(message);
			}
		}

		public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action) {
			foreach (var item in collection) {
				action.Invoke(item);
			}
		}
	}
}
