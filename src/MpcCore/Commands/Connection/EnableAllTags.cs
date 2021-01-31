using MpcCore.Commands.Base;

namespace MpcCore.Commands.Connection
{
	/// <summary>
	/// Announce that this client is interested in all tag types. This is the default setting for new clients.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#connection-settings"/>
	/// </summary>
	public class EnableAllTags : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public EnableAllTags()
		{
			Command = "tagtypes all";
		}
	}
}
