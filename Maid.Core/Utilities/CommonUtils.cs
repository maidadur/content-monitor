namespace Maid.Core
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Runtime.Serialization.Formatters.Binary;

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

		public static byte[] ToBytesArray(this object obj) {
			BinaryFormatter bf = new BinaryFormatter();
			using (var ms = new MemoryStream()) {
				bf.Serialize(ms, obj);
				return ms.ToArray();
			}
		}
	}
}
