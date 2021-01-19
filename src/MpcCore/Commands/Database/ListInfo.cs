using MpcCore.Commands.Base;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Lists the contents of the directory path. Returns items, directories and playlists in this directory, all with metadata.
	/// When listing the root directory, this currently returns the list of stored playlists. This behavior is deprecated; use <see cref="Playlist.ListPlayLists"/> instead.
	/// This command may be used to list metadata of remote files (e.g. URI beginning with “http://” or “smb://”).
	/// Clients that are connected via local socket may use this command to read the tags of an arbitrary local file with an absolute path.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class ListInfo : DirectoryCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">path to list, optional</param>
		public ListInfo(string path = "")
		{
			Path = path;
			Command = $"lsinfo \"{path}\"";
		}
	}
}
