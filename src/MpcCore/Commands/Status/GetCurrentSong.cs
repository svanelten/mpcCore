using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;
using System.Linq;

namespace MpcCore.Commands.Status
{
	/// <summary>
	/// Displays the info of the current queue item (same item that is identified in status)
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#querying-mpd-s-status"/>
	/// </summary>
	public class GetCurrentSong : IMpcCoreCommand<IItem>
	{
		public string Command { get; internal set; } = "currentsong";

		public IItem HandleResponse(IMpdResponse response)
		{
			if (!response.HasContent)
			{
				return null;
			}

			var parser = new ResponseParser(response);
			
			return parser.GetListedTracks().FirstOrDefault();
		}
	}
}
