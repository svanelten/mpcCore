using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using System.Collections.Generic;
using System.Linq;

namespace MpcCore.Mpd
{
	public class Queue : IQueue
	{
		public IEnumerable<IItem> Items { get; set; } = new List<IItem>();

		public int Count => Items?.Count() ?? 0;
	}
}
