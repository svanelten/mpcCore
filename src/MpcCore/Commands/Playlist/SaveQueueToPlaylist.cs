using MpcCore.Commands.Base;

namespace MpcCore.Commands.Playlist
{
	/// <summary>
	/// Saves the queue to NAME.m3u in the playlist directory.
	/// <see cref="https://www.musicpd.org/doc/html/protocol.html#stored-playlists"/>
	/// </summary>
	public class SaveQueueToPlaylist : SimpleCommandBase
	{
		/// <summary>
		/// Saves the queue to NAME.m3u in the playlist directory.
		/// </summary>
		/// <param name="name">playlist name (can omit .m3u ending)</param>
		public SaveQueueToPlaylist(string name)
		{
			Command = $"save {name}";
		}
	}
}
