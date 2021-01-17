using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Base
{
	public abstract class QueryPlaylistCommandBase : IMpcCoreCommand<IPlaylist>
	{
		public string Command { get; internal set; }

		public string PlaylistName { get; internal set; }

		public virtual IPlaylist HandleResponse(IEnumerable<string> response)
		{
			var parser = new ResponseParser(response);

			if (parser.ResponseHasMpdError)
			{
				return null;
			}

			return new Mpd.Playlist
			{
				Name = PlaylistName,
				LastModified = parser.GetLastModified(),
				Items = parser.GetListedTracks()
			};
		}
	}
}
