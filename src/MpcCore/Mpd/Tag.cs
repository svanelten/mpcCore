namespace MpcCore.Mpd
{
	/// <summary>
	/// Contains predefined tag string for usage with MPD
	/// </summary>
	public class Tag
	{
		/// <summary>
		/// The artist name. Its meaning is not well-defined; see "composer" and "performer" for more specific tags.
		/// </summary>
		public const string Artist = "artist";

		/// <summary>
		/// Same as <see cref="Artist"/>, but for sorting. This usually omits prefixes such as "The".
		/// </summary>
		public const string ArtistSort = "artistsort";

		/// <summary>
		/// The album name.
		/// </summary>
		public const string Album = "album";

		/// <summary>
		/// Same as <see cref="Album"/>, but for sorting.
		/// </summary>
		public const string AlbumSort = "albumsort";

		/// <summary>
		/// On multi-artist albums, this is the <see cref="Artist"/> name which shall be used for the whole album. The exact meaning of this tag is not well-defined.
		/// </summary>
		public const string AlbumArtist = "albumartist";

		/// <summary>
		/// Same as <see cref="AlbumArtist"/>, but for sorting.
		/// </summary>
		public const string AlbumArtistSort = "albumartistsort";

		/// <summary>
		/// The item title.
		/// </summary>
		public const string Title = "title";

		/// <summary>
		/// The decimal track number within the album.
		/// </summary>
		public const string Track = "track";

		/// <summary>
		/// A name for this item. This is not the item title. 
		/// The exact meaning of this tag is not well-defined. 
		/// It is often used by badly configured internet radio stations with broken tags to squeeze both the artist name and the item title in one tag.
		/// </summary>
		public const string Name = "name";

		/// <summary>
		/// The music genre.
		/// </summary>
		public const string Genre = "genre";

		/// <summary>
		/// The item’s release date. This is usually a 4-digit year.
		/// </summary>
		public const string Date = "date";

		/// <summary>
		/// The item’s original release date.
		/// </summary>
		public const string OriginalDate = "originaldate";

		/// <summary>
		/// The artist who composed the item.
		/// </summary>
		public const string Composer = "composer";

		/// <summary>
		/// The conductor who conducted the item.
		/// </summary>
		public const string Conductor = "conductor";

		/// <summary>
		/// The artist who performed the item.
		/// </summary>
		public const string Performer = "performer";

		/// <summary>
		/// A work is a distinct intellectual or artistic creation, which can be expressed in the form of one or more audio recordings.
		/// </summary>
		public const string Work = "work";

		/// <summary>
		/// Used if the sound belongs to a larger category of sounds/music (from the IDv2.4.0 TIT1 description).
		/// </summary>
		public const string Grouping = "grouping";

		/// <summary>
		/// A human-readable comment about this item. The exact meaning of this tag is not well-defined.
		/// </summary>
		public const string Comment = "comment";

		/// <summary>
		/// The decimal disc number in a multi-disc album.
		/// </summary>
		public const string Disc = "disc";

		/// <summary>
		/// The name of the label or publisher.
		/// </summary>
		public const string Label = "label";

		/// <summary>
		/// The artist id in the MusicBrainz database.
		/// <seealso cref="https://picard.musicbrainz.org/docs/mappings/"/>
		/// </summary>
		public const string MusicBrainzArtistId = "musicbrainz_artistid";

		/// <summary>
		/// The album id in the MusicBrainz database.
		/// <seealso cref="https://picard.musicbrainz.org/docs/mappings/"/>
		/// </summary>
		public const string MusicBrainzAlbumId = "musicbrainz_albumid";

		/// <summary>
		/// The album artist id in the MusicBrainz database.
		/// <seealso cref="https://picard.musicbrainz.org/docs/mappings/"/>
		/// </summary>
		public const string MusicBrainzAlbumArtistId = "musicbrainz_albumartistid";

		/// <summary>
		/// The track id in the MusicBrainz database.
		/// <seealso cref="https://picard.musicbrainz.org/docs/mappings/"/>
		/// </summary>
		public const string MusicBrainzTrackId = "musicbrainz_trackid";

		/// <summary>
		/// The release track id in the MusicBrainz database.
		/// <seealso cref="https://picard.musicbrainz.org/docs/mappings/"/>
		/// </summary>
		public const string MusicBrainzReleaseTrackId = "musicbrainz_releasetrackid";

		/// <summary>
		/// The work id in the MusicBrainz database.
		/// <seealso cref="https://picard.musicbrainz.org/docs/mappings/"/>
		/// </summary>
		public const string MusicBrainzWorkId = "musicbrainz_workid";
	}
}
