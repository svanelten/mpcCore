using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Mpd;
using MpcCore.Response;
using System.Linq;

namespace MpcCore.Commands.Sticker
{
	/// <summary>
	/// Reads a sticker value for the specified object.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#stickers"/>
	/// </summary>
	public class GetSticker : IMpcCoreCommand<ISticker>
	{
		/// <summary>
		/// The internal command string
		/// </summary>
		public string Command { get; internal set; }

		/// <summary>
		/// The path to the MPD item for this sticker operation
		/// </summary>
		public string Path { get; internal set; }

		/// <summary>
		/// The sticker type used to search for this sticker operation
		/// </summary>
		public string StickerType { get; internal set; }

		/// <summary>
		/// Gets the sticker value of the given type with the given name from a file.
		/// </summary>
		/// <param name="path">path to a MPD item</param>
		/// <param name="name">sticker name</param>
		/// <param name="type">sticker type default is <see cref="StickerType.Song"/></param>
		public GetSticker(string path, string name, string type = Mpd.StickerType.Song)
		{
			Path = path;
			StickerType = type;
			Command = $"sticker get {type} \"{path}\" {name}";
		}


		public ISticker HandleResponse(IMpdResponse response)
		{
			if (response.IsErrorResponse)
			{
				return null;
			}

			var parser = new ResponseParser(response);

			return parser.GetListedSticker(Path, StickerType).FirstOrDefault();
		}
	}
}