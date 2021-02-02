namespace MpcCore.Contracts.Mpd
{
	public interface IMount
	{
		bool IsOnRemoteServer { get; }
		string Name { get; set; }
		string Path { get; set; }
	}
}