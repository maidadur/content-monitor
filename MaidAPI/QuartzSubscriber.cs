namespace Maid.Manga.API
{
	using Maid.Core;
	using System.Threading.Tasks;

	public class QuartzSubscriber : IMessageConsumer
	{
		private MangaLoadTask _mangaLoadTask;

		public QuartzSubscriber(MangaLoadTask mangaLoader) {
			_mangaLoadTask = mangaLoader;
		}

		public async Task ProcessAsync(byte[] data) {
			await _mangaLoadTask.LoadMangaInfos();
		}
	}
}
