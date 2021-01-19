using MpcCore.Commands.Base;

namespace MpcCore.Commands.Playlist
{
	/// <summary>
	/// Lists the items with metadata in the playlist.
	/// MPD playlist plugins are supported.
	/// <see cref="https://www.musicpd.org/doc/html/protocol.html#stored-playlists"/>
	/// </summary>
	public class ListPlaylistInfo : QueryPlaylistCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="name">playlist name</param>
		public ListPlaylistInfo(string name)
		{
			PlaylistName = name;
			Command = $"listplaylistinfo {name}";
		}
	}
}
