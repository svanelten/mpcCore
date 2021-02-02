using MpcCore.Contracts.Mpd.Metadata;
using System.Collections.Generic;

namespace MpcCore.Contracts.Mpd
{
	public interface IItem
	{
		Dictionary<string, List<string>> MetaData { get; }
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
		string Title { get; }
		int Track { get; }
		string Work { get; }
		IEnumerable<string> GetTagValues(string tagName);
	}
}