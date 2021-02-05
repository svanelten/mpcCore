using MpcCore.Contracts.Mpd;
using MpcCore.Contracts.Mpd.Metadata;
using MpcCore.Mpd.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace MpcCore.Mpd
{
	/// <summary>
	/// Describes an MPD item, usually a song or a stream and its tag metadata.
	/// Tags might contain multiple values, or none.
	/// All tags return only the first value, e.g. "Artists" will return "BandNameX", but there might be other artists involved.
	/// You can retrieve a list of all values for a given tag by using <see cref="GetTagValues(string)"/> with one of the <see cref="Tag"/> values
	/// or querying <see cref="MetaData"/> directly.
	/// </summary>
	public class Item : IItem
	{
		/// <summary>
		/// Dictionary to hold all metadata for this item.
		/// </summary>
		public Dictionary<string, List<string>> MetaData { get; internal set; } = new Dictionary<string, List<string>>();

		/// <summary>
		/// Method to add a new metadata entry
		/// </summary>
		/// <param name="key">tagname or other metadata signifier</param>
		/// <param name="value">tag value</param>
		public void AddMetaData(string key, string value)
		{
			key = key.ToLower();

			if (!MetaData.ContainsKey(key))
			{
				MetaData.Add(key, new List<string> { value });
			}
			else
			{
				MetaData[key].Add(value);
			}
		}

		/// <summary>
		/// Get a list of all metadata values for a given tagname.
		/// Select a tagname from <see cref="Tag"/> or use any other value.
		/// </summary>
		/// <param name="tagName">the tagname for which you want to get info</param>
		/// <returns>list of string values for this tag if any are present</returns>
		public IEnumerable<string> GetTagValues(string tagName)
		{
			tagName = tagName.ToLower();

			return MetaData.ContainsKey(tagName) ? MetaData[tagName] : new List<string>();
		}

		/// <summary>
		/// Path to this item or url
		/// </summary>
		public string Path { get; internal set; }

		/// <summary>
		/// Playtime in seconds. Is 0 if unknown or a stream
		/// </summary>
		public double Duration { get; internal set; }

		/// <summary>
		/// The album name.
		/// </summary>
		public string Album => _getFirstValue(Tag.Album);

		/// <summary>
		/// The conductor who conducted the song.
		/// </summary>
		public string Conductor => _getFirstValue(Tag.Conductor);

		/// <summary>
		/// Sortable version of the album name.
		/// This usually omits prefixes such as “The”.
		/// </summary>
		public string AlbumSortable => _getFirstValue(Tag.AlbumSort);

		/// <summary>
		/// On multi-artist albums, this is the artist name which shall be used for the whole album. The exact meaning of this tag is not well-defined.
		/// </summary>
		public string AlbumArtist => _getFirstValue(Tag.AlbumArtist);

		/// <summary>
		/// Same as <see cref="AlbumArtist"/>, but for sorting.
		/// This usually omits prefixes such as “The”.
		/// </summary>
		public string AlbumArtistSortable => _getFirstValue(Tag.AlbumArtistSort);

		/// <summary>
		/// The artist name. Its meaning is not well-defined; see <see cref="Composer"/> and <see cref="Performer"/> for more specific tags.
		/// </summary>
		public string Artist => _getFirstValue(Tag.Artist);

		/// <summary>
		/// Same as <see cref="Artist"/>, but for sorting.
		/// This usually omits prefixes such as “The”.
		/// </summary>
		public string ArtistSortable => _getFirstValue(Tag.ArtistSort);

		/// <summary>
		/// The song title.
		/// </summary>
		public string Title => _getFirstValue(Tag.Title);

		/// <summary>
		/// A name for this song. This is not the song title. The exact meaning of this tag is not well-defined. 
		/// It is often used by badly configured internet radio stations with broken tags to squeeze both the artist name and the song title in one tag.
		/// </summary>
		public string Name => _getFirstValue(Tag.Name);

		/// <summary>
		/// The music genre.
		/// </summary>
		public string Genre => _getFirstValue(Tag.Genre);

		/// <summary>
		/// The song’s release date. This is usually a 4-digit year.
		/// </summary>
		public string Date => _getFirstValue(Tag.Date);

		/// <summary>
		/// The song’s original release date.
		/// </summary>
		public string OriginalDate => _getFirstValue(Tag.OriginalDate);

		/// <summary>
		/// The artist who composed the song.
		/// </summary>
		public string Composer => _getFirstValue(Tag.Composer);

		/// <summary>
		/// A work is a distinct intellectual or artistic creation, which can be expressed in the form of one or more audio recordings.
		/// Could be anything.
		/// </summary>
		public string Work => _getFirstValue(Tag.Work);

		/// <summary>
		/// The artist who performed the song.
		/// </summary>
		public string Performer => _getFirstValue(Tag.Performer);

		/// <summary>
		/// Used if the sound belongs to a larger category of sounds/music (from the IDv2.4.0 TIT1 description).
		/// </summary>
		public string Grouping => _getFirstValue(Tag.Grouping);

		/// <summary>
		/// The name of the label or publisher.
		/// </summary>
		public string Label => _getFirstValue(Tag.Label);

		/// <summary>
		/// A human-readable comment about this song. The exact meaning of this tag is not well-defined.
		/// </summary>
		public string Comment => _getFirstValue(Tag.Comment);

		/// <summary>
		/// The decimal disc number in a multi-disc album.
		/// </summary>
		public int Disc { get; internal set; }

		/// <summary>
		/// The decimal track number within the album.
		/// </summary>
		public int Track { get; internal set; }

		/// <summary>
		/// If this is a queue item referring only to a portion of the song file, 
		/// then this attribute contains the time range in the form START-END or START- (open ended).
		/// Both START and END are time stamps within the song in seconds (may contain a fractional part). 
		/// Example: 60-120 plays only the second minute; 180 skips the first three minutes.
		/// </summary>
		// TODO #maybe #nth add expanded property with DateTime values
		public string Range { get; internal set; } = string.Empty;

		/// <summary>
		/// The audio format of the song (or an approximation to a format supported by MPD and the decoder plugin being used).
		/// When playing this file, the audio value in the status response should be the same.
		/// </summary>
		public string Format { get; internal set; } = string.Empty;

		/// <summary>
		/// Position in a queue/playlist
		/// </summary>
		public int Position { get; internal set; }

		/// <summary>
		/// Id assigned by MPD
		/// </summary>
		public int Id { get; internal set; }

		/// <summary>
		/// MusicBrainz Metadata
		/// </summary>
		public IMusicBrainz MusicBrainz { get; internal set; } = new MusicBrainz();

		private string _getFirstValue(string key)
		{
			return MetaData.ContainsKey(key) ? MetaData[key].FirstOrDefault() ?? string.Empty : string.Empty;
		}
	}
}
