using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MpcCore.Test
{
	public class LocalTest : IDisposable
	{
		public MpcCoreClient Client;
		public LocalTest()
		{
			Client = new MpcCoreClient(new MpcCoreConnection("10.76.0.5", "6600"));
		}

		[Fact]
		public async Task Connection()
		{
			var result = await Client.ConnectAsync();

			await Client.DisconnectAsync();

			Assert.True(result);
		}

		[Fact]
		public async Task AddPlaylistToQueue()
		{
			await Client.ConnectAsync();
			var result1 = await Client.SendAsync(new Commands.Queue.ClearQueue());
			var result2 = await Client.SendAsync(new Commands.Playlist.LoadPlaylist("radio"));

			var result3 = await Client.SendAsync(new Commands.Queue.GetQueue());
			Assert.True(result3.Result.Items.ToList().Count == 5);

			var result4 = await Client.SendAsync(new Commands.Queue.GetQueue(2));
			Assert.True(result4.Result.Items.ToList().Count == 1);

			var result5 = await Client.SendAsync(new Commands.Queue.GetQueueRange(2));
			Assert.True(result5.Result.Items.ToList().Count == 3);

			var result6 = await Client.SendAsync(new Commands.Queue.GetQueueRange(2, 4));
			Assert.True(result6.Result.Items.ToList().Count == 2);

			//var result7 = await Client.SendAsync(new Commands.Queue);
		}

		[Fact]
		public async Task LoadStoredPlaylist()
		{
			await Client.ConnectAsync();
			var result = await Client.SendAsync(new Commands.Playlist.ListPlaylistInfo("test_playlist_5items"));

			Assert.False(result.Result.Count > 0);
		}

		[Fact]
		public async Task LoadMissingStoredPlaylist()
		{
			await Client.ConnectAsync();
			var result = await Client.SendAsync(new Commands.Playlist.ListPlaylistInfo("not_existing"));

			Assert.True(result.Result == null);
		}

		[Fact]
		public async Task LoadPlaylistList()
		{
			await Client.ConnectAsync();
			var result = await Client.SendAsync(new Commands.Playlist.ListPlayLists());

			Assert.True(result.Result != null);
		}

		[Fact]
		public async Task AddToPlaylist()
		{
			await Client.ConnectAsync();
			var result1 = await Client.SendAsync(new Commands.Queue.ClearQueue());
			var result2 = await Client.SendAsync(new Commands.Playlist.LoadPlaylist("radio"));
			var result3 = await Client.SendAsync(new Commands.Playlist.SaveQueueToPlaylist("test_playlist_5items"));

			Assert.True(result3.Result);
		}

		[Fact]
		public async Task DeletePlaylist()
		{
			await Client.ConnectAsync();
			var result3 = await Client.SendAsync(new Commands.Playlist.DeletePlaylist("test_playlist_5items"));

			Assert.True(result3.Result);
		}

		public void Dispose()
		{
			Client?.DisconnectAsync();
		}
	}
}