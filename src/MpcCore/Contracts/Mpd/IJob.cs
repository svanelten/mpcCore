namespace MpcCore.Contracts.Mpd
{
	public interface IJob
	{
		int JobId { get; set; }
		string JobName { get; set; }
	}
}