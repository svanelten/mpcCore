using MpcCore.Contracts.Mpd;

namespace MpcCore.Mpd
{
	public class Sticker : ISticker
	{
		/// <summary>
		/// Path to the MPD item this sticker data refers to
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// Sticker name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Sticker value
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// The sticker type.
		/// Use the predefined strings from <see cref="Mpd.StickerType"/>.
		/// Currently only "song" is implemented by MPD, this is also the default value.
		/// </summary>
		public string Type { get; set; } = StickerType.Song;
	}
}
