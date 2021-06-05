using Lib.Net.Http.WebPush;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Maid.Notifications
{
	public class Notification
	{
		public class NotificationAction
		{
			[JsonProperty("action")]
			public string Action { get; }

			[JsonProperty("title")]
			public string Title { get; }

			public NotificationAction(string action, string title) {
				Action = action;
				Title = title;
			}
		}

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("body")]
		public string Body { get; set; }

		[JsonProperty("icon")]
		public string Icon { get; set; }

		[JsonProperty("vibrate")]
		public ICollection<int> Vibrate { get; set; } = new List<int>();

		[JsonProperty("data")]
		public IDictionary<string, object> Data { get; set; }

		[JsonProperty("actions")]
		public IList<NotificationAction> Actions { get; set; } = new List<NotificationAction>();

		public PushMessage ToPushMessage(string topic = null, int? timeToLive = null, PushMessageUrgency urgency = PushMessageUrgency.Normal) {
			string json = JsonConvert.SerializeObject(new { notification = this });
			System.Console.WriteLine(json);
			return new PushMessage(json) {
				Topic = topic,
				TimeToLive = timeToLive,
				Urgency = urgency
			};
		}
	}
}