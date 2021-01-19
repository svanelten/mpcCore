using MpcCore.Commands.Base;
using MpcCore.Extensions;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Returns a list of all items in the queue within the given range
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class GetQueueRange : QueryQueueCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="rangeStart">range start</param>
		/// <param name="rangeEnd">range end</param>
		public GetQueueRange(int? rangeStart = null, int? rangeEnd = null)
		{
			Command = $"playlistinfo {rangeStart.GetParamString()}:{rangeEnd.GetParamString()}";
		}
	}
}
