namespace Maid.Core
{
	using System;

	public static class StringUtils
	{
		public static bool IsNullOrEmpty(this string str) {
			return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
		}

		public static void CheckArgumentEmptyOrNull(this string str, string message) {
			if (str.IsNullOrEmpty()) {
				throw new ArgumentException(message);
			}
		}
	}
}
