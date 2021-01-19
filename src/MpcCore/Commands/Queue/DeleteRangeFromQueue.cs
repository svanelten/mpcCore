using MpcCore.Commands.Base;
using MpcCore.Extensions;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Deletes a range of items from the queue.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class DeleteRangeFromQueue : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="rangeStart">start of the range</param>
		/// <param name="rangeEnd">end of the range</param>
		public DeleteRangeFromQueue(int? rangeStart = null, int? rangeEnd = null)
		{
			Command = $"delete {rangeStart.GetParamString()}:{rangeEnd.GetParamString()}";
		}
	}
}
