using MpcCore.Contracts.Mpd;

namespace MpcCore.Mpd
{
	public class Audio : IAudio
	{
		public int SampleRate { get; set; }
		public int Bits { get; set; }
		public int Channels { get; set; }
	}
}
