using MpcCore.Commands.Base;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Recursive listing of all items and directories for the given path that are recognized by MPD.
	/// Be aware that this can return you a REALLY big list. Handle with care.
	/// --------------------------------------------------
	/// MPD documentation warning:
	/// Do not use this command. Do not manage a client-side copy of MPD’s database. 
	/// That is fragile and adds huge overhead.It will break with large databases.Instead, query MPD whenever you need something.
	/// --------------------------------------------------
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class ListItems : DirectoryCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">path to list, optional</param>
		public ListItems(string path = "")
		{
			Path = path;
			Command = $"listall \"{path}\"";
		}
	}
}
