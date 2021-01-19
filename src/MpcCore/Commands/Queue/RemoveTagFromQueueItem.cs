using MpcCore.Commands.Base;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Removes tags from the specified item. 
	/// If the tag is not specified, then all tag values will be removed. Editing tags is only possible for remote items.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class RemoveTagFromQueueItem : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="id">item id</param>
		/// <param name="tagName">optional tag name, <see cref="MpcCore.Mpd.Tag"/></param>
		public RemoveTagFromQueueItem(int id, string tagName = "")
		{
			Command = $"cleartagid {id} {tagName}";
		}
	}
}
