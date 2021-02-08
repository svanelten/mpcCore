using Moq;
using MpcCore.Commands.Base;
using MpcCore.Contracts.Mpd;
using MpcCore.Mpd;
using MpcCore.Test.Mock;
using System;
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
			var date1 = DateTime.Now;
			var date2 = DateTime.Now.AddYears(-10).AddDays(-2);

			var connection = GetConnectionMock();
			connection.Setup(m => m.SendAsync<IDirectory>(It.IsAny<DirectoryCommandBase>()))
				.ReturnsAsync(MockResponse.Builder.New().Add(new List<string> { 
					"directory: /foo",
					$"last-modified: {date1.ToString("o", System.Globalization.CultureInfo.InvariantCulture)}",
					"file: /foo/bar1.mp3",
					"file: /foo/bar2.mp3",
					"directory: /foo/sub1",
					"directory: /foo/sub2",
					$"last-modified: {date2.ToString("o", System.Globalization.CultureInfo.InvariantCulture)}",
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
			Assert.Equal(date1, result.Result.LastModified.Value);

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
			Assert.Equal(date2, result.Result.Directories.Last().LastModified.Value);

			// file in sub2 dir
			Assert.Equal("/foo/sub2/file1.mp3", result.Result.Directories.Last().Files.First().Path);

			Assert.False(result.Status.HasMpdError);
			Assert.False(result.Status.HasError);
		}

		[Fact]
		public async Task HandlePlaylistResult()
		{
			var date1 = DateTime.Now;
			var connection = GetConnectionMock();
			connection.Setup(m => m.SendAsync<IPlaylist>(It.IsAny<QueryPlaylistCommandBase>()))
				.ReturnsAsync(MockResponse.Builder.New().Add(new List<string> {
					$"last-modified: {date1.ToString("o", System.Globalization.CultureInfo.InvariantCulture)}",
					"file: /foo/bar1.mp3",
					"file: /foo/bar2.mp3"
					}).Create());

			var client = new MpcCoreClient(connection.Object);
			var result = await client.SendAsync(new Commands.Playlist.ListPlaylist("playlistname1"));

			Assert.True(result.Result is IPlaylist);
			Assert.Equal("playlistname1", result.Result.Name);
			Assert.Equal(2, result.Result.Count);
			Assert.Equal(2, result.Result.Items.Count());
			Assert.Equal(date1, result.Result.LastModified.Value);

			Assert.Equal("/foo/bar1.mp3", result.Result.Items.First().Path);
			Assert.Equal("/foo/bar2.mp3", result.Result.Items.Last().Path);

			Assert.False(result.Status.HasMpdError);
			Assert.False(result.Status.HasError);
		}

		[Fact]
		public async Task HandleItemResult()
		{
			var date1 = DateTime.Now;
			var connection = GetConnectionMock();
			connection.Setup(m => m.SendAsync<IItem>(It.IsAny<Commands.Status.GetCurrentSong>()))
				.ReturnsAsync(MockResponse.Builder.New().Add(new List<string> {
					"file: foo/songname.mp3",
					$"last-modified: {date1.ToString("o", System.Globalization.CultureInfo.InvariantCulture)}",
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

		[Fact]
		public async Task HandleStatusResult()
		{
			var date1 = DateTime.Now;
			var connection = GetConnectionMock();
			connection.Setup(m => m.SendAsync<IStatus>(It.IsAny<Commands.Status.GetStatus>()))
				.ReturnsAsync(MockResponse.Builder.New().Add(new List<string> {
					"audio: 44800:16:4",
					"bitrate: 44800",
					"consume: 1",
					"crossfade: 3",
					"duration: 120.34",
					"elapsed: 45.65",
					"mixrampdb: 2.5",
					"mixrampdelay: 2",
					"nextsong: 12",
					"nextsongid: 345",
					"playlist: 456",
					"playlistlength: 9001",
					"random: 0",
					"repeat: 0",
					"single: 0",
					"song: 8345",
					"songid: 935",
					"volume: 98",
					"state: play",
					"partition: default",
					"updating_db: 14"
					}).Create());

			var client = new MpcCoreClient(connection.Object);
			var result = await client.SendAsync(new Commands.Status.GetStatus());

			Assert.True(result.Result is IStatus);
			Assert.Equal(44800, result.Result.AudioSetting.SampleRate);
			Assert.Equal(16, result.Result.AudioSetting.Bits);
			Assert.Equal(4, result.Result.AudioSetting.Channels);
			Assert.Equal(44800, result.Result.Bitrate);
			Assert.True(result.Result.Consume);
			Assert.Equal(3, result.Result.Crossfade);
			Assert.Equal(120.34d, result.Result.Duration);
			Assert.Equal(45.65d, result.Result.Elapsed);
			Assert.False(result.Result.IsPaused);
			Assert.True(result.Result.IsPlaying);
			Assert.False(result.Result.IsStopped);
			Assert.Equal(2.5d, result.Result.MixRampDb);
			Assert.Equal(2, result.Result.MixRampDelay);
			Assert.Equal(12, result.Result.NextSong);
			Assert.Equal(345, result.Result.NextSongId);
			Assert.Equal("default", result.Result.Partition);
			Assert.Equal(456, result.Result.Playlist);
			Assert.Equal(9001, result.Result.PlaylistLength);
			Assert.False(result.Result.Random);
			Assert.False(result.Result.Repeat);
			Assert.False(result.Result.Single);
			Assert.Equal(8345, result.Result.Song);
			Assert.Equal(935, result.Result.SongId);
			Assert.Equal("play", result.Result.State);
			Assert.Equal(14, result.Result.UpdateJobId);
			Assert.Equal(98, result.Result.Volume);

			Assert.False(result.Status.HasMpdError);
			Assert.False(result.Status.HasError);
		}
	}
}
 