using MpcCore.Contracts.Mpd;
using System;

namespace MpcCore.Mpd
{
	public class Playlist : Queue, IPlaylist
	{
		public string Name { get; set; }
		public DateTime? LastModified { get; set; }
	}
}
