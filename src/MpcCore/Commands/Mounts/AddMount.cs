using MpcCore.Commands.Base;
using MpcCore.Contracts.Mpd;

namespace MpcCore.Commands.Mounts
{
	/// <summary>
	/// Mount the specified remote storage URI at the given path.
	/// Find accessible <see cref="INeighbor"/> with the <see cref="ListNeighbors"/> command.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#mounts-and-neighbors"/>
	/// </summary>
	public class AddMount : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="name">Mount name</param>
		/// <param name="path">Storage uri, either a directory path or a file server uri</param>
		public AddMount(string name, string path)
		{
			Command = $"mount {name} \"{path}\"";
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="mount">Mount DTO with name and storage uri</param>
		public AddMount(IMount mount)
		{
			Command = $"mount {mount.Name} \"{mount.Path}\"";
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="neighbor">Neighbor DTO with name and storage uri</param>
		public AddMount(INeighbor neighbor)
		{
			Command = $"mount {neighbor.Name} \"{neighbor.Path}\"";
		}
	}
}
