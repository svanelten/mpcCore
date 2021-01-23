using MpcCore.Commands.Base;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Locates a picture for the given song and returns a chunk of the image file at the given offset.
	/// This is usually implemented by reading embedded pictures from binary tags (e.g. ID3v2’s APIC tag).
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class ReadPicture : BinaryResponseCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">path to item</param>
		/// <param name="offSet">the offset to begin reading</param>
		public ReadPicture(string path = "", int offSet = 0)
		{
			Path = path;
			Command = $"readpicture \"{path}\" {offSet}";
		}
	}
}
