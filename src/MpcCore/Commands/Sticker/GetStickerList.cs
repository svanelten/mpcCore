using MpcCore.Commands.Base;
using MpcCore.Mpd;

namespace MpcCore.Commands.Sticker
{
	/// <summary>
	/// Lists all stickers for the specified MPD item.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#stickers"/>
	/// </summary>
	public class GetStickerList : StickerListCommandBase
	{
		/// <summary>
		/// Gets the sticker value of the given type with the given name from a file.
		/// </summary>
		/// <param name="path">path to a MPD item</param>
		/// <param name="type">sticker type default is <see cref="StickerType.Song"/></param>
		public GetStickerList(string path, string type = Mpd.StickerType.Song)
		{
			Path = path;
			StickerType = type;
			Command = $"sticker list {type} \"{path}\"";
		}
	}
}
