using MpcCore.Commands.Base;
using MpcCore.Extensions;

namespace MpcCore.Commands.Playlist
{
	/// <summary>
	/// Loads the specified part of the playlist into the queue.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#stored-playlists"/>
	/// </summary>
	public class LoadPlaylistRange : SimpleCommandBase
	{
		/// <summary>
		/// Loads the specified part of the playlist into the queue.
		/// </summary>
		/// <param name="name">playlist name (can omit .m3u ending)</param>
		/// <param name="rangeStart">start position of range</param>
		/// <param name="rangeEnd">end position of range, will go to end of playlist if emptry</param>
		public LoadPlaylistRange(string name, int? rangeStart = null, int? rangeEnd = null)
		{
			Command = $"load {name} {rangeStart.GetParamString()}:{rangeEnd.GetParamString()}";
		}
	}
}
