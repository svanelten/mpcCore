using MpcCore.Commands.Base;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Locate album art for the given item and return a chunk of an album art image file at offset.
	/// MPD currently searches the directory the file resides in for a file called cover.png, cover.jpg, cover.tiff or cover.bmp.
	/// Returns the file size and actual number of bytes read at the requested offset, followed by the chunk requested as raw bytes.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class GetAlbumArt : BinaryResponseCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">path</param>
		/// <param name="offset">offset for binary, default is 0 (start)</param>
		public GetAlbumArt(string path, int offset = 0)
		{
			Path = path;
			Command = $"albumart \"{path}\" {offset}";
		}
	}
}
