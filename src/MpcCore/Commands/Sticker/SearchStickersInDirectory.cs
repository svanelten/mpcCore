using MpcCore.Commands.Base;
using MpcCore.Mpd;

namespace MpcCore.Commands.Sticker
{
	/// <summary>
	/// Searches the sticker database for stickers with the specified name, below the specified directory path. 
	/// For each item with this sticker, it returns the URI and that one sticker’s value.
	/// You can also pass a value that this sticker has to match.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#stickers"/>
	/// </summary>
	public class SearchStickersInDirectory : StickerListCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">path to a MPD directory</param>
		/// <param name="name">sticker name</param>
		/// <param name="value">optional searchterm to match sticker value</param>
		/// <param name="type">sticker type default is <see cref="StickerType.Song"/></param>
		public SearchStickersInDirectory(string path, string name, string value = "", string type = Mpd.StickerType.Song)
		{
			Path = path;
			StickerType = type;
			Command = $"sticker find {type} \"{path}\" {name}";

			if (!string.IsNullOrEmpty(value))
			{
				Command += $" = {value}";
			}
		}
	}
}
