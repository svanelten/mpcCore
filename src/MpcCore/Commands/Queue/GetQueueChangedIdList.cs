using MpcCore.Commands.Base;
using MpcCore.Extensions;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Returns a list of items ids in the queue that were changed since the given version.
	/// Set the range to limit the list to items changed in part of the queue.
	/// This is more resource-friendly version of <see cref="GetQueueChangedList"/>
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class GetQueueChangedIdList : QueryQueueCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="version">queue version</param>
		/// <param name="rangeStart">optional start of range</param>
		/// <param name="rangeEnd">optional end of range</param>
		public GetQueueChangedIdList(string version, int? rangeStart = null, int? rangeEnd = null)
		{
			if (rangeStart.HasValue || rangeEnd.HasValue)
			{
				Command = $"plchangesposid  {version} {rangeStart.GetParamString()}:{rangeEnd.GetParamString()}";
			}
			else
			{
				Command = $"plchangesposid  {version}";
			}
		}
	}
}
