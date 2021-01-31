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

		// other
		public const string Fingerprint = "chromaprint";
		public const string ReplayGainMode = "replay_gain_mode";
		public const string Size = "size";
		public const string Binary = "binary";
		public const string MimeType = "type";
		public const string Id = "id";

		// sticker
		public const string Sticker = "sticker";
	}
}
