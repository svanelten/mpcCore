using System.Collections.Generic;

namespace MpcCore.Contracts.Mpd
{
	public interface IQueue
	{
		int Count { get; }
		IEnumerable<IItem> Items { get; set; }
	}
}