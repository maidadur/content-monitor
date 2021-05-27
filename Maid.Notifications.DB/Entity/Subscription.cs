namespace Maid.Notifications.DB
{
	using Maid.Core;
	using Lib.Net.Http.WebPush;
	using Newtonsoft.Json;
	using System.Collections.Generic;

	public class Subscription : BaseEntity
	{
		public Subscription() {}

		public Subscription(PushSubscription pushSubscription) {
			Endpoint = pushSubscription.Endpoint;
			KeysObj = JsonConvert.SerializeObject(pushSubscription, Formatting.Indented);
		}

		public string Endpoint { get; set; }
		public string KeysObj { get; set; }

		public PushSubscription GetPushSubscription() {
			return new PushSubscription() {
				Endpoint = Endpoint,
				Keys = JsonConvert.DeserializeObject<Dictionary<string, string>>(KeysObj)
			};
		}

	}
}
