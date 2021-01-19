using MpcCore.Contracts.Mpd;
using System;

namespace MpcCore.Mpd
{
	public class Statistics : IStatistics
	{
		/// <summary>
		/// Number of artists in the database
		/// </summary>
		public int Artists { get; internal set; }

		/// <summary>
		/// Number of albums in the database
		/// </summary>
		public int Albums { get; internal set; }

		/// <summary>
		/// Number of items in the database
		/// </summary>
		public int Songs { get; internal set; }

		/// <summary>
		/// Mpd daemon uptime in seconds
		/// </summary>
		public int Uptime { get; internal set; }

		/// <summary>
		/// Sum of all item times in the database in seconds
		/// </summary>
		public int DatabasePlaytime { get; internal set; }

		/// <summary>
		/// Date of last database update
		/// </summary>
		public DateTime? DatabaseLastUpdate { get; internal set; }

		/// <summary>
		/// Time length of music played
		/// </summary>
		public int Playtime { get; set; }
	}
}
