using MpcCore.Commands.Base;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Deletes the item with the given id from the queue
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class DeleteFromIdQueue : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="id">item id to delete</param>
		public DeleteFromIdQueue(string id)
		{
			Command = $"deleteid {id}";
		}
	}
}
