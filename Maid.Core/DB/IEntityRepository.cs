using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maid.Core.DB
{
	public interface IEntityRepository
	{
		Task<IEnumerable<BaseEntity>> GetAllAsync(Type type);
	}
}