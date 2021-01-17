using MpcCore.Commands.Base;

namespace MpcCore.Commands.Status
{
	/// <summary>
	/// Clears the current error message in status (this is also accomplished by any command that starts playback).
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#querying-mpd-s-status"/>
	/// </summary>
	public class ClearError : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public ClearError()
		{
			Command = "clearerror";
		}
	}
}
