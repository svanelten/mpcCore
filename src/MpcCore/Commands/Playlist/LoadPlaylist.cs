using MpcCore.Commands.Base;

namespace MpcCore.Commands.Playlist
{
	/// <summary>
	/// Loads the playlist into the current queue.
	/// MPD playlist plugins are supported.
	/// <see cref="https://www.musicpd.org/doc/html/protocol.html#stored-playlists"/>
	/// </summary>
	public class LoadPlaylist : SimpleCommandBase
	{
		/// <summary>
		/// Loads the playlist into the current queue.
		/// MPD playlist plugins are supported.
		/// </summary>
		/// <param name="name">playlist name (can omit .m3u ending)</param>
		public LoadPlaylist(string name)
		{
			Command = $"load {name}";
		}
	}
}
