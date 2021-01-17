using MpcCore.Commands.Base;

namespace MpcCore.Commands.Queue
{
	public class GetQueueRange : QueryQueueCommandBase
	{
		public GetQueueRange(int rangeStart, int? rangeEnd = null)
		{
			Command = $"playlistinfo {rangeStart}:{(rangeEnd.HasValue ? rangeEnd.Value.ToString() : string.Empty)}";
		}
	}
}
