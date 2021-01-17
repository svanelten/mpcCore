using MpcCore.Commands.Base;

namespace MpcCore.Commands.Playlist
{
	/// <summary>
	/// Deletes the given playlist
	/// <see cref="https://www.musicpd.org/doc/html/protocol.html#stored-playlists"/>
	/// </summary>
	public class DeletePlaylist : SimpleCommandBase
	{
		/// <summary>
		/// Deletes the given playlist
		/// </summary>
		/// <param name="name">playlist name (can omit .m3u ending)</param>
		public DeletePlaylist(string name)
		{
			Command = $"rm {name}";
		}
	}
}
