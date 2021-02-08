namespace MpcCore.Contracts.Mpd
{
	public interface IAudio
	{
		int SampleRate { get; set; }
		int Bits { get; set; }
		int Channels { get; set; }
	}
}
