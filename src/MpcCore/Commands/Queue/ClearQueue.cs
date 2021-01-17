using MpcCore.Commands.Base;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Queue commands: clear
	/// Clears the current queue
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class ClearQueue : SimpleCommandBase
	{
		public ClearQueue()
		{
			Command = "clear";
		}
	}
}
