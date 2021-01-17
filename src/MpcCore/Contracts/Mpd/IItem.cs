using MpcCore.Contracts.Mpd.Metadata;
using System.Collections.Generic;

namespace MpcCore.Contracts.Mpd
{
	public interface IItem
	{
		string Album { get; }
		string AlbumArtist { get; }
		string AlbumArtistSortable { get; }
		string AlbumSortable { get; }
		string Artist { get; }
		string ArtistSortable { get; }
		string Comment { get; }
		string Composer { get; }
		string Conductor { get; }
		string Date { get; }
		int Disc { get; }
		double Duration { get; }
		string Format { get; }
		string Genre { get; }
		int Id { get; }
		string Label { get; }
		IMusicBrainz MusicBrainz { get; }
		string Name { get; }
		string OriginalDate { get; }
		string Path { get; }
		string Performer { get; }
		int Position { get; }
		string Range { get; }
		int Time { get; }
		string Title { get; }
		int Track { get; }
		IDictionary<string, string> UnknownMetadata { get; }
		string Work { get; }
	}
}