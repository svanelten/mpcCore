using MpcCore.Contracts.Mpd;
using MpcCore.Contracts.Mpd.Metadata;
using MpcCore.Mpd.Metadata;
using System.Collections.Generic;

namespace MpcCore.Mpd
{
	public class Item : IItem
	{
		public string Path { get; internal set; }
		public double Duration { get; internal set; }
		public string Album { get; internal set; }
		public string Conductor { get; internal set; }
		public string AlbumSortable { get; internal set; }
		public string AlbumArtist { get; internal set; }
		public string AlbumArtistSortable { get; internal set; }
		public string Artist { get; internal set; }
		public string ArtistSortable { get; internal set; }
		public string Title { get; internal set; }
		public int Track { get; internal set; }
		public string Name { get; internal set; }
		public string Genre { get; internal set; }
		public string Date { get; internal set; }
		public string OriginalDate { get; internal set; }
		public string Composer { get; internal set; }
		public string Work { get; internal set; }
		public string Performer { get; internal set; }
		public string Label { get; internal set; }
		public string Comment { get; internal set; }
		public int Disc { get; internal set; }

		// TODO #maybe #nth add expanded property with DateTime values
		public string Range { get; internal set; }
		public string Format { get; internal set; }
		public int Position { get; internal set; }
		public int Id { get; internal set; }
		public IDictionary<string, string> UnknownMetadata { get; internal set; } = new Dictionary<string, string>();
		public IMusicBrainz MusicBrainz { get; internal set; } = new MusicBrainz();
	}
}
