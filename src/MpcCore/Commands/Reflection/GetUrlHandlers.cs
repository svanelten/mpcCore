using MpcCore.Commands.Base;
using MpcCore.Response;

namespace MpcCore.Commands.Reflection
{
	/// <summary>
	/// Gets a list of available URL handlers.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#reflection"/>
	/// </summary>
	public class GetUrlHandlers : ValueListCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public GetUrlHandlers()
		{
			Command = "urlhandlers";
			Key = ResponseParserKeys.Handler;
		}
	}
}
