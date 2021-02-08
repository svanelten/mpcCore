
namespace MpcCore.Response
{
	/// <summary>
	/// Magic values = bad
	/// </summary>
	internal class ResponseParserKeys
	{
		// statistics
		public const string Artists = "artists";
		public const string Albums = "albums";
		public const string Songs = "songs";
		public const string Uptime = "uptime";
		public const string DbPlaytime = "db_playtime";
		public const string DbUpdate = "db_update";
		public const string Playtime = "playtime";

		// multiple use
		public const string LastModified = "last-modified";
		public const string UpdatingDb = "updating_db";
		public const string Playlist = "playlist";
		public const string File = "file";
		public const string Directory = "directory";
		public const string Position = "pos";
		public const string Range = "range";
		public const string Format = "format";
		public const string Plugin = "plugin";
		public const string Suffix = "suffix";

		// status keys
		public const string Partition = "partition";
		public const string Volume = "volume";
		public const string Repeat = "repeat";
		public const string Random = "random";
		public const string Single = "single";
		public const string Consume = "consume";
		public const string PlaylistLength = "playlistlength";
		public const string State = "state";
		public const string Song = "song";
		public const string SongId = "songid";
		public const string NextSong = "nextsong";
		public const string NextSongId = "nextsongid";
		public const string Time = "time";
		public const string Elapsed = "elapsed";
		public const string Duration = "duration";
		public const string Bitrate = "bitrate";
		public const string Crossfade = "crossfade";
		public const string MixRampDb = "mixrampdb";
		public const string MixRampDelay = "mixrampdelay";
		public const string Audio = "audio";
		public const string Error = "error";

		// output devices
		public const string OutputDeviceId = "outputid";
		public const string OutputDeviceName = "outputname";
		public const string OutputDeviceEnabled = "outputenabled";
		public const string OutputDeviceAttribute = "attribute";

		// mounts
		public const string MountName = "mount";
		public const string MountStorage = "storage";

		// neighbors
		public const string NeighborName = "name";
		public const string NeighborStorage = "neighbor";

		// other
		public const string Fingerprint = "chromaprint";
		public const string ReplayGainMode = "replay_gain_mode";
		public const string Size = "size";
		public const string Binary = "binary";
		public const string MimeType = "type";
		public const string TagType = "tagtype";
		public const string Id = "id";
		public const string Command = "command";

		// sticker
		public const string Sticker = "sticker";

		// urlhandlers
		public const string Handler = "handler";
	}
}
