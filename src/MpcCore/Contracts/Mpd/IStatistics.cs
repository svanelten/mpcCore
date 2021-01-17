using System;

namespace MpcCore.Contracts.Mpd
{
	public interface IStatistics
	{
		int Albums { get; }
		int Artists { get; }
		DateTime? DatabaseLastUpdate { get; }
		int DatabasePlaytime { get; }
		int Playtime { get; set; }
		int Songs { get; }
		int Uptime { get; }
	}
}