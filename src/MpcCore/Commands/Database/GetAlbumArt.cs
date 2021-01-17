using MpcCore.Commands.Base;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Locate album art for the given song and return a chunk of an album art image file at offset OFFSET.
	/// MPD currently searches the directory the file resides in for a file called cover.png, cover.jpg, cover.tiff or cover.bmp.
	/// Returns the file size and actual number of bytes read at the requested offset, followed by the chunk requested as raw bytes.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class GetAlbumArt : SimpleCommandBase
	{
		/// <summary>
		/// Locate album art for the given song and return a chunk of an album art image file at offset OFFSET.
		/// MPD currently searches the directory the file resides in for a file called cover.png, cover.jpg, cover.tiff or cover.bmp.
		/// Returns the file size and actual number of bytes read at the requested offset, followed by the chunk requested as raw bytes.
		/// </summary>
		/// <param name="uri">path</param>
		/// <param name="offset">offset for binary, default is 0 (start)</param>
		public GetAlbumArt(string uri, int offset = 0)
		{
			Command = $"albumart {uri} {offset}";
		}
	}
}
