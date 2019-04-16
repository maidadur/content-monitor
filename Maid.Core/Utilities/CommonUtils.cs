namespace Maid.Core
{
	using System;

	public static class CommonUtils
	{
		public static void CheckArgumentNull(this object obj, string message) {
			if (obj == null) {
				throw new ArgumentNullException(message);
			}
		}
	}
}
