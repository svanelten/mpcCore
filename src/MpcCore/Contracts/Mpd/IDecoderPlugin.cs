using System.Collections.Generic;

namespace MpcCore.Contracts.Mpd
{
	public interface IDecoderPlugin
	{
		IEnumerable<string> SupportedMimeTypes { get; set; }
		string Name { get; set; }
		IEnumerable<string> SupportedSuffixes { get; set; }
	}
}