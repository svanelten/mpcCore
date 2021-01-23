using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;

namespace MpcCore.Commands.Base
{
	public abstract class QueryPlaylistCommandBase : IMpcCoreCommand<IPlaylist>
	{
		public string Command { get; internal set; }

		public string PlaylistName { get; internal set; }

		public virtual IPlaylist HandleResponse(IMpdResponse response)
		{
			if (response.IsErrorResponse)
			{
				return null;
			}

			var parser = new ResponseParser(response);

			return new Mpd.Playlist
			{
				Name = PlaylistName,
				LastModified = parser.GetLastModified(),
				Items = parser.GetListedTracks()
			};
		}
	}
}
