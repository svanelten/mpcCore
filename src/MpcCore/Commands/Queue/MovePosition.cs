using MpcCore.Commands.Base;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Move the item at the given position to the new position on the queue.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class MovePosition : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="position">item position to move</param>
		/// <param name="newPosition">target position</param>
		public MovePosition(int position, int newPosition)
		{
			Command = $"move {position} {newPosition}";
		}
	}
}
