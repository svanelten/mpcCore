using MpcCore.Commands.Base;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Deletes the item at the given position from the queue.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class DeleteFromQueue : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="position">item position to delete</param>
		public DeleteFromQueue(int position)
		{
			Command = $"delete {position}";
		}
	}
}
