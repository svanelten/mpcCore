using MpcCore.Commands.Base;
using MpcCore.Response;

namespace MpcCore.Commands.Reflection
{
	/// <summary>
	/// Shows which commands the current user does not have access to.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#reflection"/>
	/// </summary>
	public class GetUnavailableCommands : ValueListCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public GetUnavailableCommands()
		{
			Command = "notcommands";
			Key = ResponseParserKeys.Command;
		}
	}
}
