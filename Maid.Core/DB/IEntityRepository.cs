using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Maid.Core.DB
{
	public interface IEntityRepository
	{
		Task<IEnumerable<BaseEntity>> GetAllAsync(Type type);

		Task<BaseEntity> GetAsync(Type type, Guid id);

		BaseEntity Get(Type type, Guid id);

		void Save();

		void Update(BaseEntity entity);

	}
}