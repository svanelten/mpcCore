using Moq;
using MpcCore.Commands.Base;
using MpcCore.Contracts.Mpd;
using MpcCore.Mpd;
using MpcCore.Test.Mock;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MpcCore.Test.Client
{
	[ExcludeFromCodeCoverage]
	public class ResponseParsingTests : DefaultTestsBase
	{
		[Fact]
		public async Task HandleSimpleErrorReponse()
		{
			var connection = GetConnectionMock();
			connection.Setup(m => m.SendAsync<bool>(It.IsAny<Commands.Player.Stop>()))
				.ReturnsAsync(MockResponse.BasicErrorResponse);

			var client = new MpcCoreClient(connection.Object);
			var result = await client.SendAsync(new Commands.Player.Stop());

			Assert.False(result.Result);
			Assert.True(result.Status.HasError);
			Assert.True(result.Status.HasMpdError);
			Assert.NotNull(result.Status.MpdError);
			Assert.Equal("Unspecified error", result.Status.MpdError.Message);
			Assert.Equal(Constants.Ack, result.Status.MpdError.RawError);
		}

		[Fact]
		public async Task HandleErrorReponse()
		{
			var connection = GetConnectionMock();
			connection.Setup(m => m.SendAsync<bool>(It.IsAny<Commands.Player.Play>()))
				.ReturnsAsync(MockResponse.Builder.New().AddErrorResponse("ACK [50@0] {play} No such song").Create());

			var client = new MpcCoreClient(connection.Object);
			var result = await client.SendAsync(new Commands.Player.Play());

			Assert.False(result.Result);
			Assert.True(result.Status.HasError);
			Assert.Equal("mpd returned an error", result.Status.ErrorMessage);
			Assert.True(result.Status.HasMpdError);
			Assert.NotNull(result.Status.MpdError);
			Assert.Equal("50", result.Status.MpdError.Code);
			Assert.Equal("0", result.Status.MpdError.Line);
			Assert.Equal("play", result.Status.MpdError.Command);
			Assert.Equal("No such song", result.Status.MpdError.Message);
			Assert.Equal("ACK [50@0] {play} No such song", result.Status.MpdError.RawError);
		}

		[Fact]
		public async Task HandleBoolResult()
		{
			var connection = GetConnectionMock();
			connection.Setup(m => m.SendAsync<bool>(It.IsAny<SimpleCommandBase>()))
				.ReturnsAsync(MockResponse.BasicOkResponse);

			var client = new MpcCoreClient(connection.Object);
			var result = await client.SendAsync(new Commands.Player.Play());

			Assert.True(result.Result);
			Assert.False(result.Status.HasError);
			Assert.False(result.Status.HasMpdError);
			Assert.Null(result.Status.MpdError);
			Assert.True(string.IsNullOrEmpty(result.Status.ErrorMessage));
		}

		[Fact]
		public async Task HandleJobResult()
		{
			var connection = GetConnectionMock();
			connection.Setup(m => m.SendAsync<IJob>(It.IsAny<JobCommandBase>()))
				.ReturnsAsync(MockResponse.Builder.New().Add("updating_db: 123").Create());

			var client = new MpcCoreClient(connection.Object);
			var result = await client.SendAsync(new Commands.Database.Update());

			Assert.True(result.Result is IJob);
			Assert.Equal(123, result.Result.JobId);
			Assert.Equal("updating_db", result.Result.JobName);
			Assert.False(result.Status.HasMpdError);
			Assert.False(result.Status.HasError);
		}

		[Fact]
		public async Task HandleValueListResult()
		{
			var connection = GetConnectionMock();
			connection.Setup(m => m.SendAsync<IEnumerable<string>>(It.IsAny<ValueListCommandBase>()))
				.ReturnsAsync(MockResponse.Builder.New().Add(new List<string> { "tagtype: artist", "tagtype: album", "tagtype: title" }).Create());

			var client = new MpcCoreClient(connection.Object);
			var result = await client.SendAsync(new Commands.Connection.GetAvailableTags());

			Assert.True(result.Result is IEnumerable<string>);
			Assert.True(result.Result.Count() == 3);
			Assert.Equal("artist", result.Result.First());
			Assert.False(result.Status.HasMpdError);
			Assert.False(result.Status.HasError);
		}

		[Fact]
		public async Task HandleStickerListResult()
		{
			var connection = GetConnectionMock();
			connection.Setup(m => m.SendAsync<IEnumerable<ISticker>>(It.IsAny<StickerListCommandBase>()))
				.ReturnsAsync(MockResponse.Builder.New().Add(new List<string> { "file: /foo/bar", "sticker: name1=value1", "file: /foo/baz", "sticker: name1=value1" }).Create());

			var client = new MpcCoreClient(connection.Object);
			var result = await client.SendAsync(new Commands.Sticker.GetStickerList("/foo"));

			Assert.True(result.Result is IEnumerable<ISticker>);
			Assert.True(result.Result.Count() == 2);
			Assert.Equal("/foo/bar", result.Result.First().Path);
			Assert.Equal("song", result.Result.First().Type);
			Assert.Equal("name1", result.Result.First().Name);
			Assert.Equal("value1", result.Result.First().Value);
			Assert.False(result.Status.HasMpdError);
			Assert.False(result.Status.HasError);
		}

		[Fact]
		public async Task HandleQueueResult()
		{
			var connection = GetConnectionMock();
			connection.Setup(m => m.SendAsync<IQueue>(It.IsAny<QueryQueueCommandBase>()))
				.ReturnsAsync(MockResponse.Builder.New().Add(new List<string> { "file: /foo/bar.mp3", "name: song1", "file: /foo/baz.mp3", "name: song2" }).Create());

			var client = new MpcCoreClient(connection.Object);
			var result = await client.SendAsync(new Commands.Queue.GetQueue());

			Assert.True(result.Result is IQueue);
			Assert.True(result.Result.Count == 2);
			Assert.Equal("/foo/bar.mp3", result.Result.Items.First().Path);
			Assert.Equal("song1", result.Result.Items.First().Name);
			Assert.False(result.Status.HasMpdError);
			Assert.False(result.Status.HasError);
		}

		[Fact]
		public async Task HandleDirectoryResult()
		{
			var connection = GetConnectionMock();
			connection.Setup(m => m.SendAsync<IDirectory>(It.IsAny<DirectoryCommandBase>()))
				.ReturnsAsync(MockResponse.Builder.New().Add(new List<string> { 
					"directory: /foo",
					"last-modified: 2019-08-07T16:50:00.0000000+02:00",
					"file: /foo/bar1.mp3",
					"file: /foo/bar2.mp3",
					"directory: /foo/sub1",
					"directory: /foo/sub2",
					"last-modified: 2018-07-06T15:40:30.0000000+02:00",
					"file: /foo/sub2/file1.mp3",
					}).Create());

			var client = new MpcCoreClient(connection.Object);
			var result = await client.SendAsync(new Commands.Database.ListFiles("/foo"));

			Assert.True(result.Result is IDirectory);
			Assert.True(result.Result.Directories.Count == 2);
			Assert.True(result.Result.Files.Count == 2);
			
			// main dir
			Assert.Equal("/foo", result.Result.Path);
			Assert.Equal("foo", result.Result.Name);
			Assert.Equal("2019-08-07T16:50:00.0000000+02:00", result.Result.LastModified.Value.ToString("o", System.Globalization.CultureInfo.InvariantCulture));

			// sub dir without date and without files
			Assert.Equal("sub1", result.Result.Directories.First().Name);
			Assert.False(result.Result.Directories.First().HasFiles);
			Assert.False(result.Result.Directories.First().HasDirectories);
			Assert.False(result.Result.Directories.First().LastModified.HasValue);

			// sub dir with files
			Assert.Equal("sub2", result.Result.Directories.Last().Name);
			Assert.True(result.Result.Directories.Last().HasFiles);
			Assert.Single(result.Result.Directories.Last().Files);
			Assert.False(result.Result.Directories.Last().HasDirectories);
			Assert.Equal("2018-07-06T15:40:30.0000000+02:00", result.Result.Directories.Last().LastModified.Value.ToString("o", System.Globalization.CultureInfo.InvariantCulture));

			// file in sub2 dir
			Assert.Equal("/foo/sub2/file1.mp3", result.Result.Directories.Last().Files.First().Path);

			Assert.False(result.Status.HasMpdError);
			Assert.False(result.Status.HasError);
		}

		[Fact]
		public async Task HandlePlaylistResult()
		{
			var connection = GetConnectionMock();
			connection.Setup(m => m.SendAsync<IPlaylist>(It.IsAny<QueryPlaylistCommandBase>()))
				.ReturnsAsync(MockResponse.Builder.New().Add(new List<string> {
					"last-modified: 2019-08-07T16:50:00.0000000+02:00",
					"file: /foo/bar1.mp3",
					"file: /foo/bar2.mp3"
					}).Create());

			var client = new MpcCoreClient(connection.Object);
			var result = await client.SendAsync(new Commands.Playlist.ListPlaylist("playlistname1"));

			Assert.True(result.Result is IPlaylist);
			Assert.Equal("playlistname1", result.Result.Name);
			Assert.Equal(2, result.Result.Count);
			Assert.Equal(2, result.Result.Items.Count());
			Assert.Equal("2019-08-07T16:50:00.0000000+02:00", result.Result.LastModified.Value.ToString("o", System.Globalization.CultureInfo.InvariantCulture));

			Assert.Equal("/foo/bar1.mp3", result.Result.Items.First().Path);
			Assert.Equal("/foo/bar2.mp3", result.Result.Items.Last().Path);

			Assert.False(result.Status.HasMpdError);
			Assert.False(result.Status.HasError);
		}

		[Fact]
		public async Task HandleItemResult()
		{
			var connection = GetConnectionMock();
			connection.Setup(m => m.SendAsync<IItem>(It.IsAny<Commands.Status.GetCurrentSong>()))
				.ReturnsAsync(MockResponse.Builder.New().Add(new List<string> {
					"file: foo/songname.mp3",
					"Last-Modified: 2021-01-12T15:36:47Z",
					"Artist: the artist",
					"ArtistSort: artist, the",
					"AlbumArtist: the artist",
					"AlbumArtistSort: artist, the",
					"performer: the performer",
					"Title: inspiring song title",
					"Album: the albumtitle",
					"AlbumSort: albumtitle, the",
					"Track: 15",
					"Date: 1992",
					"Genre: Rap",
					"Genre: Hip-Hop",
					"Genre: Street Music",
					"Name: songname",
					"Label: Columbia",
					"Time: 215",
					"duration: 214.592",
					"Pos: 5",
					"Id: 31",
					"Disc: 1",
					"Composer: ",
					"Work: work data",
					"Comment: the comment",
					"Format: mp3",
					"originaldate: 1500",
					"range: 10-100",
					"musicbrainz_artistid: musicbrainz_artistid",
					"musicbrainz_albumid: musicbrainz_albumid",
					"musicbrainz_albumartistid: musicbrainz_albumartistid",
					"musicbrainz_trackid: musicbrainz_trackid",
					"musicbrainz_releasetrackid: musicbrainz_releasetrackid",
					"musicbrainz_workid: musicbrainz_workid",
					"unknowntagname: unknowntagvalue"
					}).Create());

			var client = new MpcCoreClient(connection.Object);
			var result = await client.SendAsync(new Commands.Status.GetCurrentSong());

			Assert.True(result.Result is IItem);
			Assert.Equal("foo/songname.mp3", result.Result.Path);
			Assert.Equal("the albumtitle", result.Result.Album);
			Assert.Equal("the artist", result.Result.AlbumArtist);
			Assert.Equal("artist, the", result.Result.AlbumArtistSortable);
			Assert.Equal("albumtitle, the", result.Result.AlbumSortable);
			Assert.Equal("the artist", result.Result.Artist);
			Assert.Equal("artist, the", result.Result.ArtistSortable);
			Assert.Equal("the comment", result.Result.Comment);
			Assert.Equal("1992", result.Result.Date);
			Assert.Equal(1, result.Result.Disc);
			Assert.Equal(214.592d, result.Result.Duration);
			Assert.Equal("mp3", result.Result.Format);
			Assert.Equal(31, result.Result.Id);
			Assert.Equal("Columbia", result.Result.Label);
			Assert.Equal("songname", result.Result.Name);
			Assert.Equal("1500", result.Result.OriginalDate);
			Assert.Equal("the performer", result.Result.Performer);
			Assert.Equal(5, result.Result.Position);
			Assert.Equal("10-100", result.Result.Range);
			Assert.Equal("inspiring song title", result.Result.Title);
			Assert.Equal(15, result.Result.Track);
			Assert.Equal("work data", result.Result.Work);

			// music brainz data
			Assert.Equal("musicbrainz_albumartistid", result.Result.MusicBrainz.AlbumArtistId);
			Assert.Equal("musicbrainz_albumid", result.Result.MusicBrainz.AlbumId);
			Assert.Equal("musicbrainz_artistid", result.Result.MusicBrainz.ArtistId);
			Assert.Equal("musicbrainz_trackid", result.Result.MusicBrainz.TrackId);
			Assert.Equal("musicbrainz_workid", result.Result.MusicBrainz.WorkId);
			Assert.Equal("musicbrainz_releasetrackid", result.Result.MusicBrainz.ReleaseTrackId);

			// field that is null in response (no key)
			Assert.Equal("", result.Result.Conductor);

			// field that is empty in response
			Assert.Equal("", result.Result.Composer);

			// see that there are two values for genre, rap should be first
			Assert.Equal("Rap", result.Result.Genre);

			// get other tags for genre
			var genreTags = result.Result.GetTagValues("genre");
			Assert.Equal(3, genreTags.Count());
			Assert.Equal("Rap", genreTags.First());
			Assert.Equal("Hip-Hop", genreTags.ElementAt(1));
			Assert.Equal("Street Music", genreTags.ElementAt(2));

			// get unknown metadata
			var unknownTags = result.Result.GetTagValues("unknowntagname");
			Assert.Single(unknownTags);
			Assert.Equal("unknowntagvalue", unknownTags.First());

			// we should have 19 elements
			Assert.Equal(19, result.Result.MetaData.Count);

			Assert.False(result.Status.HasMpdError);
			Assert.False(result.Status.HasError);
		}
	}
}
 