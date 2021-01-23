using MpcCore.Contracts.Mpd;
using System;

namespace MpcCore.Mpd
{
	public class Status : IStatus
	{
		/// <summary>
		/// Status has error
		/// </summary>
		public bool HasError => Error != null;

		/// <summary>
		/// Mpd playback is running
		/// </summary>
		public bool IsPlaying => State == "play";

		/// <summary>
		/// Mpd playback is paused
		/// </summary>
		public bool IsPaused => State == "pause";

		/// <summary>
		/// Mpd playback is stopped
		/// </summary>
		public bool IsStopped => State == "stop";

		/// <summary>
		/// The current partition
		/// </summary>
		public string Partition { get; internal set; }

		/// <summary>
		/// Current volume.
		/// 0 if it can not be determined.
		/// </summary>
		public int Volume { get; internal set; }

		/// <summary>
		/// Repeat mode
		/// </summary>
		public bool Repeat { get; internal set; }

		/// <summary>
		/// Random mode
		/// </summary>
		public bool Random { get; internal set; }

		/// <summary>
		/// Single 
		/// </summary>
		public bool Single { get; internal set; }

		/// <summary>
		/// Consume mode (tracks are removed from the queue after playing)
		/// </summary>
		public bool Consume { get; internal set; }

		/// <summary>
		/// Playlist version number
		/// </summary>
		public int Playlist { get; internal set; }

		/// <summary>
		/// Playlist length
		/// </summary>
		public int PlaylistLength { get; internal set; }

		/// <summary>
		/// play, pause or stop
		/// </summary>
		public string State { get; internal set; } = string.Empty;

		/// <summary>
		/// Playlist/queue number of the item currently playing or stopped
		/// </summary>
		public int Song { get; internal set; }

		/// <summary>
		/// Playlist/queue item id of the item currently playing or stopped
		/// </summary>
		public int SongId { get; internal set; }

		/// <summary>
		/// Playlist/queue item number of the item after the one currently playing or stopped
		/// </summary>
		public int NextSong { get; internal set; }

		/// <summary>
		/// Playlist/queue item id of the item after the one currently playing or stopped
		/// </summary>
		public int NextSongId { get; internal set; }

		/// <summary>
		/// Total time elapsed within the current item in seconds, with higher resolution than "time" used to have.
		/// </summary>
		public double Elapsed { get; internal set; }

		/// <summary>
		/// Duration of the current item in seconds
		/// </summary>
		public double Duration { get; internal set; }

		/// <summary>
		/// Instantaneous bitrate in kbps
		/// </summary>
		public int Bitrate { get; internal set; }

		/// <summary>
		/// Crossfade setting in seconds
		/// </summary>
		public int Crossfade { get; internal set; }

		/// <summary>
		/// Threshold in dB
		/// </summary>
		public double MixRampDb { get; internal set; }

		/// <summary>
		/// Mixrampdelay in dB
		/// </summary>
		public int MixRampDelay { get; internal set; }

		/// <summary>
		/// Jobid if an update is running
		/// </summary>
		public int UpdateJobId { get; internal set; }

		/// <summary>
		/// Error in case there is one
		/// </summary>
		public IMpdError Error { get; internal set; }

		/// <summary>
		/// The format emitted by the decoder plugin during playback
		/// </summary>
		public IAudio AudioSetting { get; internal set; }
	}
}
