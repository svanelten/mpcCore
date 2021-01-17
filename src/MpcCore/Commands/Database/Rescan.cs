using MpcCore.Commands.Base;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Updates the music database: find new files, remove deleted files, update modified files & unmodified files.
	/// URI is a particular directory or song/file to update.If you do not specify it, everything is updated.
	/// Prints updating_db: JOBID where JOBID is a positive number identifying the update job. You can read the current job id in the status response.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class Rescan : JobCommandBase
	{
		/// <summary>
		/// Rescans the given path or everything if no path is specified.
		/// Rescans both modified & unmodified files. If you only want new elements, use <see cref="Update"/>
		/// </summary>
		/// <param name="uri">path to rescan, optional</param>
		public Rescan(string uri = "")
		{
			Command = $"rescan {uri}";
		}
	}
}
