using MpcCore.Contracts.Mpd;
using System.Collections.Generic;

namespace MpcCore.Mpd
{
	/// <summary>
	/// Information about a MPD decoder plugin, and its supported suffixes and MIME types
	/// </summary>
	public class DecoderPlugin : IDecoderPlugin
	{
		/// <summary>
		/// Plugin name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// List of file name suffixes this plugin supports
		/// </summary>
		public IEnumerable<string> SupportedSuffixes { get; set; } = new List<string>();

		/// <summary>
		/// List of mimetypes this plugin supports
		/// </summary>
		public IEnumerable<string> SupportedMimeTypes { get; set; } = new List<string>();
	}
}
