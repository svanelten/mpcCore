using MpcCore.Contracts.Mpd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MpcCore.Mpd
{
	public class Directory : IDirectory
	{
		/// <summary>
		/// Directory contains files
		/// </summary>
		public bool HasFiles => Files?.Any() ?? false;

		/// <summary>
		/// Directory contains subdirectories
		/// </summary>
		public bool HasDirectories => Directories?.Any() ?? false;

		/// <summary>
		/// path for this directory
		/// </summary>
		public string Path { get; internal set; }

		/// <summary>
		/// Name of this directory
		/// </summary>
		public string Name { get; internal set; }

		/// <summary>
		/// Last modified date for this directory
		/// </summary>
		public DateTime? LastModified { get; set; }

		/// <summary>
		/// List of MPD files in this directory
		/// </summary>
		public List<IItem> Files { get; set; } = new List<IItem>();

		/// <summary>
		/// List of subdirectories in this directory
		/// </summary>
		public List<IDirectory> Directories { get; set; } = new List<IDirectory>();
	}
}
