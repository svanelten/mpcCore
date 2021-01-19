using MpcCore.Commands.Base;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Rescans both modified & unmodified files. If you only want new elements, use <see cref="Update"/>
	/// Point to a directory or item/file to rescan.If you do not specify it, everything is scanned.
	/// Prints updating_db: JOBID where JOBID is a positive number identifying the update job. You can read the current job id in the status response.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class Rescan : JobCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">path to rescan, optional</param>
		public Rescan(string path = "")
		{
			Command = $"rescan \"{path}\"";
		}
	}
}
