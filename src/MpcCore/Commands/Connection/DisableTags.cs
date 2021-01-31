using MpcCore.Commands.Base;
using System.Collections.Generic;

namespace MpcCore.Commands.Connection
{
	/// <summary>
	/// Remove one or more tags from the list of tag types the client is interested in. These will be omitted from responses to this client.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#connection-settings"/>
	/// </summary>
	public class DisableTags : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="tagTypes">List of tag types to remove from responses</param>
		public DisableTags(IEnumerable<string> tagTypes)
		{
			Command = $"tagtypes disable {string.Join(" ", tagTypes)}";
		}
	}
}
