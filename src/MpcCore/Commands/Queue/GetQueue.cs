using MpcCore.Commands.Base;

namespace MpcCore.Commands.Queue
{
	public class GetQueue : QueryQueueCommandBase
	{
		public GetQueue()
		{
			Command = $"playlistinfo";
		}

		public GetQueue(int queuePosition)
		{
			Command = $"playlistinfo {queuePosition}";
		}
	}
}
