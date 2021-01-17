using MpcCore.Commands.Base;
using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Playlist
{
	/// <summary>
	/// Returns a list of the playlist directory and the last-modified date
	/// To avoid problems due to clock differences between clients and the server, clients should not compare this value with their local clock.
	/// <see cref="https://www.musicpd.org/doc/html/protocol.html#stored-playlists"/>
	/// </summary>
	public class ListPlayLists : IMpcCoreCommand<IEnumerable<IPlaylist>>
	{
		public string Command { get; internal set; } = "listplaylists";

		public IEnumerable<IPlaylist> HandleResponse(IEnumerable<string> response)
		{
			var parser = new ResponseParser(response);

			if (parser.ResponseHasNoContent)
			{
				return null;
			}

			return parser.GetListedPlaylists();
		}
	}
}
