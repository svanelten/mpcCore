using System.Collections.Generic;

namespace MpcCore.Contracts.Mpd
{
	public interface IOutputDevice
	{
		bool HasAttributes { get; }
		IEnumerable<string> Attributes { get; set; }
		int Id { get; set; }
		string Name { get; set; }
		bool Enabled { get; set; }
		string Plugin { get; set; }
	}
}