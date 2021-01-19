using MpcCore.Commands.Base;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Updates modified / deleted / added files. If you want to rescan everything, use <see cref="Rescan"/>
	/// Point to a directory or item/file to update.If you do not specify it, everything is updated.
	/// Prints updating_db: JOBID where JOBID is a positive number identifying the update job. You can read the current job id in the status response.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class Update : JobCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">path to rescan, optional</param>
		public Update(string path = "")
		{
			Command = $"update \"{path}\"";
		}
	}
}
