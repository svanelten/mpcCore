using MpcCore.Commands.Base;

namespace MpcCore.Commands.Playlist
{
	/// <summary>
	/// Lists the songs with metadata in the playlist.
	/// MPD playlist plugins are supported.
	/// <see cref="https://www.musicpd.org/doc/html/protocol.html#stored-playlists"/>
	/// </summary>
	public class ListPlaylistInfo : QueryPlaylistCommandBase
	{
		/// <summary>
		/// Lists the songs with metadata in the playlist.
		/// MPD playlist plugins are supported.
		/// </summary>
		/// <param name="name">playlist name</param>
		public ListPlaylistInfo(string name)
		{
			PlaylistName = name;
			Command = $"listplaylistinfo {name}";
		}
	}
}
