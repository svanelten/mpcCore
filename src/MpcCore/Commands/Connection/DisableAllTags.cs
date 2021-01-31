using MpcCore.Commands.Base;

namespace MpcCore.Commands.Connection
{
	/// <summary>
	/// Clear the list of tag types this client is interested in. This means that MPD will not send any tags to this client.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#connection-settings"/>
	/// </summary>
	public class DisableAllTags : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public DisableAllTags()
		{
			Command = "tagtypes clear";
		}
	}
}
