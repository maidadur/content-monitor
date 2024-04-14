using System;
using System.Threading.Tasks;

namespace Maid.Core.Utilities
{
	public class TaskUtils
	{
		public static async Task RepeatActionUntilSuccess(Action action) { 
			if (action == null) {
				throw new ArgumentNullException("action");
			}
			await Task.Delay(0);
			while (true) {
				try {  
					action(); 
					break; 
				} catch(Exception ex) {
					Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
					await Task.Delay(10000);
				}
			}
		}
	}
}
