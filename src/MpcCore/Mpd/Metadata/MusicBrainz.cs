using MpcCore.Contracts.Mpd.Metadata;

namespace MpcCore.Mpd.Metadata
{
	public class MusicBrainz : IMusicBrainz
	{
		public string ArtistId { get; set; }
		public string AlbumId { get; set; }
		public string AlbumArtistId { get; set; }
		public string TrackId { get; set; }
		public string ReleaseTrackId { get; set; }
		public string WorkId { get; set; }
	}
}
