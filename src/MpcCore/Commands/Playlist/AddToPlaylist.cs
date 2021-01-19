using MpcCore.Commands.Base;

namespace MpcCore.Commands.Playlist
{
	/// <summary>
	/// Adds the given path to the playlist NAME.m3u. NAME.m3u will be created if it does not exist.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#stored-playlists"/>
	/// </summary>
	public class AddToPlaylist : SimpleCommandBase
	{
		/// <summary>
		/// Adds URI to the playlist NAME.m3u. 
		/// The playlist will be created if it does not exist.
		/// </summary>
		/// <param name="name">playlist name (can omit .m3u ending)</param>
		/// <param name="path">path of file/stream</param>
		public AddToPlaylist(string name, string path)
		{
			Command = $"playlistadd {name} \"{path}\"";
		}
	}
}
