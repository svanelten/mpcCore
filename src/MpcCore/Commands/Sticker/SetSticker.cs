using MpcCore.Commands.Base;
using MpcCore.Contracts.Mpd;
using MpcCore.Mpd;

namespace MpcCore.Commands.Sticker
{
	/// <summary>
	/// Sets a sticker value for the specified object.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#stickers"/>
	/// </summary>
	public class SetSticker : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">path to a MPD item</param>
		/// <param name="name">sticker name</param>
		/// <param name="value">sticker value</param>
		/// <param name="type">sticker type default is <see cref="Mpd.StickerType.Song"/></param>
		public SetSticker(string path, string name, string value, string type = StickerType.Song)
		{
			Command = $"sticker set {type} \"{path}\" {name} {value}";
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="sticker">Sticker object with all data to set</param>
		public SetSticker(ISticker sticker)
		{
			Command = $"sticker set {sticker.Type} \"{sticker.Path}\" {sticker.Name} {sticker.Value}";
		}
	}
}
