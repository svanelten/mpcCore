using MpcCore.Commands.Base;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Move the item with the given id to the new position on the queue.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class MoveIdToPosition : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="id">item id</param>
		/// <param name="newPosition">target position</param>
		public MoveIdToPosition(int itemId, int newPosition)
		{
			Command = $"moveid {itemId} {newPosition}";
		}
	}
}
