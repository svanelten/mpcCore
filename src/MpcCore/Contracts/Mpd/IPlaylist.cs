using System;

namespace MpcCore.Contracts.Mpd
{
	public interface IPlaylist : IQueue
	{
		string Name { get; set; }
		DateTime? LastModified { get; set; }
	}
}