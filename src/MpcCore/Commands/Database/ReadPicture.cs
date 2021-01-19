using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;
using System.Collections.Generic;
using System.Linq;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Locates a picture for the given song and returns a chunk of the image file at the given offset.
	/// This is usually implemented by reading embedded pictures from binary tags (e.g. ID3v2’s APIC tag).
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class ReadPicture : IMpcCoreCommand<IAlbumArt>
	{
		/// <summary>
		/// Requested path to an item
		/// </summary>
		public string Path { get; internal set; }

		/// <summary>
		/// The command sent to the MPD server
		/// </summary>
		public string Command { get; internal set; }

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">path to item</param>
		/// <param name="offSet">the offset to begin reading</param>
		public ReadPicture(string path = "", int offSet = 0)
		{
			Command = $"readpicture \"{path}\" {offSet}";
		}

		/// <summary>
		/// Handles the response and returns an AlbumArt DTO or null if none is found
		/// </summary>
		/// <param name="response">MPD response</param>
		/// <returns>AlbumArt DTO with metadata</returns>
		public IAlbumArt HandleResponse(IEnumerable<string> response)
		{
			var parser = new ResponseParser(response);

			if (parser.ResponseHasNoContent)
			{
				return null;
			}

			return parser.GetAlbumArt(Path);
		}
	}
}
