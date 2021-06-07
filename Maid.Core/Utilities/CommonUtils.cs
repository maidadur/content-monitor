namespace Maid.Core
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Runtime.Serialization.Formatters.Binary;
	using System.Text;
	using System.Threading.Tasks;

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

		public static async Task ForEachAsync<T>(this IEnumerable<T> list, Func<T, Task> func) {
			foreach (var value in list) {
				await func(value);
			}
		}

		public static byte[] ToBytesArray(this string data) {
			//BinaryFormatter bf = new BinaryFormatter();
			//using (var ms = new MemoryStream()) {
			//	bf.Serialize(ms, obj);
			//	return ms.ToArray();
			//}
			return Encoding.UTF8.GetBytes(data);
		}

		public static bool IsEmpty<T>(this IEnumerable<T> collection) {
			return collection == null || collection.Count() == 0;
		}

		public static bool IsNotEmpty<T>(this IEnumerable<T> collection) {
			return !collection.IsEmpty();
		}
	}
}