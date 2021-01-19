using MpcCore.Commands.Base;
using MpcCore.Extensions;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Shuffles the queue. START:END is optional and specifies a range of items.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class ShuffleQueue : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="rangeStart">optional range start</param>
		/// <param name="rangeEnd">optional range end</param>
		public ShuffleQueue(int? rangeStart = null, int? rangeEnd = null)
		{
			Command = rangeStart.HasValue || rangeEnd.HasValue
				? $"shuffle {rangeStart.GetParamString()}:{rangeEnd.GetParamString()}"
				: "shuffle";
		}
	}
}
