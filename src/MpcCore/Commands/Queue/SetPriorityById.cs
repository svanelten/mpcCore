using MpcCore.Commands.Base;
using MpcCore.Extensions;
using System;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Sets the priority for a one or more of items identified by id in the queue.
	///  A higher priority means that it will be played first when "random" mode is enabled.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class SetPriorityById : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="priority">priority between 0-255. Default for new items is 0.</param>
		/// <param name="rangeStart">start of the range</param>
		/// <param name="rangeEnd">end of the range</param>
		public SetPriorityById(int priority, int? rangeStart = null, int? rangeEnd = null)
		{
			if (priority < 0 || priority > 255)
			{
				throw new ArgumentOutOfRangeException("prioritiy must be a value between 0-255");
			}

			Command = $"prio {priority} {rangeStart.GetParamString()}:{rangeEnd.GetParamString()}";
		}
	}
}
