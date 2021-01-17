namespace MpcCore.Contracts.Mpd.Metadata
{
	public interface IMusicBrainz
	{
		string AlbumArtistId { get; set; }
		string AlbumId { get; set; }
		string ArtistId { get; set; }
		string ReleaseTrackId { get; set; }
		string TrackId { get; set; }
		string WorkId { get; set; }
	}
}