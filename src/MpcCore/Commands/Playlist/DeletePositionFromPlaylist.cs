using MpcCore.Commands.Base;

namespace MpcCore.Commands.Playlist
{
	/// <summary>
	/// Deletes the item on the given position from the playlist
	/// <see cref="https://www.musicpd.org/doc/html/protocol.html#stored-playlists"/>
	/// </summary>
	public class DeletePositionFromPlaylist : SimpleCommandBase
	{
		/// <summary>
		/// Deletes the item on the given position from the playlist
		/// </summary>
		/// <param name="name">playlist name (can omit .m3u ending)</param>
		/// <param name="position">playlist position</param>
		public DeletePositionFromPlaylist(string name, int position)
		{
			Command = $"playlistdelete {name} {position}";
		}
	}
}
