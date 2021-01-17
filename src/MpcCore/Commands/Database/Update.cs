using MpcCore.Commands.Base;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Updates the music database: find new files, remove deleted files, update modified files.
	/// URI is a particular directory or song/file to update.If you do not specify it, everything is updated.
	/// Prints updating_db: JOBID where JOBID is a positive number identifying the update job. You can read the current job id in the status response.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class Update : JobCommandBase
	{
		/// <summary>
		/// Updates the database, checking the given path or everything if no path is specified.
		/// Updates modified / deleted / added files. If you want to rescan everything, use <see cref="Rescan"/>
		/// </summary>
		/// <param name="uri">path to rescan, optional</param>
		public Update(string uri = "")
		{
			Command = $"update {uri}";
		}
	}
}
