using MpcCore.Commands.Base;
using MpcCore.Extensions;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Displays a list of items with metadata in the queue that were changed since the given version.
	/// Set the range to limit the list to items changed in part of the queue.
	/// To get a smaller list with Ids only use <see cref="GetQueueChangedIdList"/>
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class GetQueueChangedList : QueryQueueCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="version">queue version</param>
		/// <param name="rangeStart">optional start of range</param>
		/// <param name="rangeEnd">optional end of range</param>
		public GetQueueChangedList(string version, int? rangeStart = null, int? rangeEnd = null)
		{
			if (rangeStart.HasValue || rangeEnd.HasValue)
			{
				Command = $"plchanges {version} {rangeStart.GetParamString()}:{rangeEnd.GetParamString()}";
			}
			else
			{
				Command = $"plchanges {version}";
			}
		}
	}
}
