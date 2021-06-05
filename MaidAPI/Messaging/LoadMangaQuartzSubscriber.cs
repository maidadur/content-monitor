namespace Maid.Manga.API
{
	using Maid.Core;
	using System.Threading.Tasks;

	public class LoadMangaQuartzSubscriber : IMessageConsumer
	{
		private MangaLoadTask _mangaLoadTask;

		public LoadMangaQuartzSubscriber(MangaLoadTask mangaLoader) {
			_mangaLoadTask = mangaLoader;
		}

		public async Task ProcessAsync(byte[] data) {
			await _mangaLoadTask.LoadMangaInfosAsync();
		}
	}
}