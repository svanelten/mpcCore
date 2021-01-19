using MpcCore.Commands.Base;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Displays a list of items in the queue, or for the item identified by the given id.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class GetQueue : QueryQueueCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public GetQueue()
		{
			Command = $"playlistid";
		}

		/// <summary>
		/// <inheritdoc/> 
		/// </summary>
		/// <param name="id">optional item id to display info for</param>
		public GetQueue(int id)
		{
			Command = $"playlistid {id}";
		}
	}
}
