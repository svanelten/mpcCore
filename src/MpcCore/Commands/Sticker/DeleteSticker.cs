using MpcCore.Commands.Base;
using MpcCore.Contracts.Mpd;
using MpcCore.Mpd;

namespace MpcCore.Commands.Sticker
{
	/// <summary>
	/// Deletes a sticker value for the specified object.
	/// If you do not specify a sticker name, all sticker values are deleted.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#stickers"/>
	/// </summary>
	public class DeleteSticker : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">path to a mpd item</param>
		/// <param name="name">sticker name, deletes all stickers if empty</param>
		/// <param name="type">sticker type default is <see cref="Mpd.StickerType.Song"/></param>
		public DeleteSticker(string path, string name = "", string type = StickerType.Song)
		{
			Command = $"sticker delete {type} \"{path}\" {name}";
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="sticker">Sticker object to delete, needs at least a path.</param>
		public DeleteSticker(ISticker sticker)
		{
			Command = $"sticker set {sticker.Type} \"{sticker.Path}\" {sticker.Name ?? ""}";
		}
	}
}
