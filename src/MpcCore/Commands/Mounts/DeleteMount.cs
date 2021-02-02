using MpcCore.Commands.Base;
using MpcCore.Contracts.Mpd;

namespace MpcCore.Commands.Mounts
{
	/// <summary>
	/// Unmounts the specified path or uri.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#mounts-and-neighbors"/>
	/// </summary>
	public class DeleteMount : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">Mounted storage uri, either a directory path or a file server uri</param>
		public DeleteMount(string path)
		{
			Command = $"unmount \"{path}\"";
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="mount">Mount DTO with storage uri to remove</param>
		public DeleteMount(IMount mount)
		{
			Command = $"unmount \"{mount.Path}\"";
		}
	}
}
