using MpcCore.Commands.Base;

namespace MpcCore.Commands.Playlist
{
	/// <summary>
	/// Moves the item at position fromPosition in the playlist NAME.m3u to the position toPosition.
	/// <see cref="https://www.musicpd.org/doc/html/protocol.html#stored-playlists"/>
	/// </summary>
	public class MovePositionOnPlaylist : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="name">playlist name (can omit .m3u ending)</param>
		/// <param name="fromPosition">current playlist position</param>
		/// <param name="toPosition">target playlist position</param>
		public MovePositionOnPlaylist(string name, int fromPosition, int toPosition)
		{
			Command = $"playlistmove {name} {fromPosition} {toPosition}";
		}
	}
}
