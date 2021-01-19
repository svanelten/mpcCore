using MpcCore.Commands.Base;

namespace MpcCore.Commands.Playlist
{
	/// <summary>
	/// Lists the items in the playlist.
	/// MPD playlist plugins are supported.
	/// <see cref="https://www.musicpd.org/doc/html/protocol.html#stored-playlists"/>
	/// </summary>
	public class ListPlaylist : QueryPlaylistCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="name">playlist name (can omit .m3u ending)</param>
		public ListPlaylist(string name)
		{
			PlaylistName = name;
			Command = $"listplaylist {name}";
		}
	}
}
