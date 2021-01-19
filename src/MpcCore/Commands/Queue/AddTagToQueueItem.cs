using MpcCore.Commands.Base;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Adds a tag to the specified item. Editing song tags is only possible for remote items. 
	/// This change is volatile: it may be overwritten by tags received from the server, 
	/// and the data is gone when the item gets removed from the queue.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class AddTagToQueueItem : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="id">item id</param>
		/// <param name="tagName">tag name, <see cref="MpcCore.Mpd.Tag"/></param>
		/// <param name="tagValue">tag value to set</param>
		public AddTagToQueueItem(int id, string tagName, string tagValue)
		{
			Command = $"addtagid {id} {tagName} \"{tagValue}\"";
		}
	}
}
