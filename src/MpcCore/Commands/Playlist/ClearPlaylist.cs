using MpcCore.Commands.Base;

namespace MpcCore.Commands.Playlist
{
	/// <summary>
	/// Clears the playlist NAME.m3u.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#stored-playlists"/>
	/// </summary>
	public class ClearPlaylist : SimpleCommandBase
	{
		/// <summary>
		/// Clears the playlist NAME.m3u.
		/// </summary>
		/// <param name="name">playlist name (can omit .m3u ending)</param>
		public ClearPlaylist(string name)
		{
			Command = $"playlistclear {name}";
		}
	}
}
