using MpcCore.Contracts.Mpd;
using System.Collections.Generic;
using System.Linq;

namespace MpcCore.Mpd
{
	public class OutputDevice : IOutputDevice
	{
		public bool HasAttributes => Attributes != null && Attributes.Any();

		/// <summary>
		/// Device Id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Device name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Sound plugin
		/// </summary>
		public string Plugin { get; set; }

		/// <summary>
		/// Is output enabled?
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// Attribute list
		/// </summary>
		public IEnumerable<string> Attributes { get; set; } = new List<string>();
	}
}
