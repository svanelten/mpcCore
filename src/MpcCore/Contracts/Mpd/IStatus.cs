using MpcCore.Contracts.Mpd;

namespace MpcCore.Contracts.Mpd
{
	public interface IStatus
	{
		IAudio AudioSetting { get; }
		int Bitrate { get; }
		bool Consume { get; }
		int Crossfade { get; }
		double Duration { get; }
		double Elapsed { get; }
		IMpdError Error { get; }
		bool HasError { get; }
		bool IsPaused { get; }
		bool IsPlaying { get; }
		bool IsStopped { get; }
		double MixRampDb { get; }
		int MixRampDelay { get; }
		int NextSong { get; }
		int NextSongId { get; }
		string Partition { get; }
		int Playlist { get; }
		int PlaylistLength { get; }
		bool Random { get; }
		bool Repeat { get; }
		bool Single { get; }
		int Song { get; }
		int SongId { get; }
		string State { get; }
		int UpdateJobId { get; }
		int Volume { get; }
	}
}