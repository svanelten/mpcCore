using MpcCore.Contracts.Mpd;
using System.Collections.Generic;

namespace MpcCore.Mpd
{
	public interface IDirectory
	{
		List<IDirectory> Directories { get; set; }
		List<IItem> Files { get; set; }
		bool HasFiles { get; }
		bool HasDirectories { get; }
		string Name { get; }
		string Path { get; }
	}
}