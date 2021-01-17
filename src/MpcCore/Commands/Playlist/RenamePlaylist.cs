using MpcCore.Commands.Base;

namespace MpcCore.Commands.Playlist
{
	/// <summary>
	/// Renames the playlist NAME.m3u to NEWNAME.m3u.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#stored-playlists"/>
	/// </summary>
	public class RenamePlaylist : SimpleCommandBase
	{
		/// <summary>
		/// Renames the playlist NAME.m3u to NEWNAME.m3u.
		/// </summary>
		/// <param name="name">playlist name (can omit .m3u ending)</param>
		/// <param name="newName">new playlist name (can omit .m3u ending)</param>
		public RenamePlaylist(string name, string newName)
		{
			Command = $"rename {name} {newName}";
		}
	}
}
