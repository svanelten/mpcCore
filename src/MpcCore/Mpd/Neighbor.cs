using MpcCore.Contracts.Mpd;

namespace MpcCore.Mpd
{
	/// <summary>
	/// A neighbor is an accessible file server on the local net that is reachable by MPD.
	/// MPD returns a list of these neighbors with <see cref="Commands.Mounts.ListNeighbors"/>.
	/// </summary>
	public class Neighbor : INeighbor
	{
		/// <summary>
		/// The name of this neighbor
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The uri for this neighbor.
		/// </summary>
		public string Path { get; set; }
	}
}
