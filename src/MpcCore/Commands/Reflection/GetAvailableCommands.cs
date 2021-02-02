using MpcCore.Commands.Base;
using MpcCore.Response;

namespace MpcCore.Commands.Reflection
{
	/// <summary>
	/// Shows which commands the current user has access to.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#reflection"/>
	/// </summary>
	public class GetAvailableCommands : ValueListCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public GetAvailableCommands()
		{
			Command = "commands";
			Key = ResponseParserKeys.Command;
		}
	}
}
