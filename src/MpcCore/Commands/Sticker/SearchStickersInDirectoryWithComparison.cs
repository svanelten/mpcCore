using MpcCore.Commands.Base;
using MpcCore.Mpd;

namespace MpcCore.Commands.Sticker
{
	/// <summary>
	/// Searches the sticker database for stickers with the specified name, below the specified directory path. 
	/// For each item with this sticker, it returns the URI and that one sticker’s value.
	/// You can also pass a value that this sticker is compared to, either "<" or ">".
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#stickers"/>
	/// </summary>
	public class SearchStickersInDirectoryWithComparison : StickerListCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">path to a MPD directory</param>
		/// <param name="name">sticker name</param>
		/// <param name="value">searchterm to match sticker value</param>
		/// <param name="valueGreater">comparison for sticker value against searchterm. Default is ">", set to true for "<" </param>
		/// <param name="type">sticker type default is <see cref="StickerType.Song"/></param>
		public SearchStickersInDirectoryWithComparison(string path, string name, string value, bool valueGreater, string type = Mpd.StickerType.Song)
		{
			Path = path;
			StickerType = type;
			Command = $"sticker find {type} \"{path}\" {name} {(valueGreater ? "<" : ">")} {value}";
		}
	}
}
