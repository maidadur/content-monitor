using System.Threading.Tasks;

namespace Maid.Core
{
	public interface IMessageConsumer
	{
		Task ProcessAsync(byte[] data);
	}
}
