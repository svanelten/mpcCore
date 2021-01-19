using MpcCore.Commands.Base;
using MpcCore.Extensions;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Moves a range of items to the new position on the queue.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class MoveRangeToPosition : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="newPosition">target position</param>
		/// <param name="rangeStart">start of the range</param>
		/// <param name="rangeEnd">end of the range</param>
		public MoveRangeToPosition(int newPosition, int? rangeStart = null, int? rangeEnd = null)
		{
			Command = $"move {newPosition} {rangeStart.GetParamString()}:{rangeEnd.GetParamString()}";
		}
	}
}
