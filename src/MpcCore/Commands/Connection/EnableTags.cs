using MpcCore.Commands.Base;
using System.Collections.Generic;

namespace MpcCore.Commands.Connection
{
	/// <summary>
	/// Re-enable one or more tags from the list of tag types for this client. These will no longer be hidden from responses to this client.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#connection-settings"/>
	/// </summary>
	public class EnableTags : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="tagTypes">List of tag types to enable for responses</param>
		public EnableTags(IEnumerable<string> tagTypes)
		{
			Command = $"tagtypes enable {string.Join(" ", tagTypes)}";
		}
	}
}
