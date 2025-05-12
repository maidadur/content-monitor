using Maid.Core.DB;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Maid.Core
{
	public class SaveImageToEntityTask
	{
		protected IServiceProvider Provider { get; }

		public SaveImageToEntityTask(IServiceProvider provider) {
			Provider = provider;
		}

		public async Task SaveImageToEntity(byte[] data) {
			string strData = System.Text.Encoding.UTF8.GetString(data);
			SaveImageMessage message = JsonConvert.DeserializeObject<SaveImageMessage>(strData);
			var type = Type.GetType(message.EntityName);
			var genericType = typeof(IEntityRepository);
			var repository = Provider.GetService(genericType) as IEntityRepository;
			var entity = await repository.GetAsync(type, message.EntityId);
			PropertyInfo propertyInfo = entity.GetType().GetProperty("ImageUrl");
			propertyInfo.SetValue(entity, Convert.ChangeType(message.ImageUrl, propertyInfo.PropertyType), null);
			repository.Update(entity);
			repository.Save();
		}
	}
}
