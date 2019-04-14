using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Maid.Core
{
	public class LookupTypeManager
	{
		private static Lazy<LookupTypeManager> _instance = new Lazy<LookupTypeManager>(() => new LookupTypeManager());

		public List<Type> LookupTypes { get; set; }

		private LookupTypeManager() {
			LookupTypes = new List<Type>();
		}

		public static LookupTypeManager Instance => _instance.Value;

		public void LoadLookupTypes(Assembly assembly) {
			var types = assembly.GetTypes();
			LookupTypes.AddRange(types.Where(t => t.IsSubclassOf(typeof(BaseLookup))).ToList());
		}

		public Type GetLookupType(string typeName) {
			return LookupTypes.FirstOrDefault(t => t.Name == typeName);
		}
	}
}
