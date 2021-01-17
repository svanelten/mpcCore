using MpcCore.Contracts.Mpd;

namespace MpcCore.Mpd
{
	public class Job : IJob
	{
		public string JobName { get; set; }
		public int JobId { get; set; }
	}
}
