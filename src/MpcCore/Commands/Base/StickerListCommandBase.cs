using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Base
{
	/// <summary>
	/// Base functionality for commands that return a job response
	/// </summary>
	public abstract class StickerListCommandBase : IMpcCoreCommand<IEnumerable<ISticker>>
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

		public virtual IEnumerable<ISticker> HandleResponse(IMpdResponse response)
		{
			if (response.IsErrorResponse)
			{
				return null;
			}

			var parser = new ResponseParser(response);

			return parser.GetListedSticker(Path, StickerType);
		}
	}
}
