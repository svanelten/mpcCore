using MpcCore.Commands.Base;
using MpcCore.Response;

namespace MpcCore.Commands.Connection
{
	/// <summary>
	/// Shows a list of available tag types. It is an intersection of the metadata_to_use setting and this client’s tag mask.
	/// About the tag mask: each client can decide to disable any number of tag types, which will be omitted from responses to this client.
	/// That is a good idea, because it makes responses smaller. Other tagtypes sub commands configure this list.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#connection-settings"/>
	/// </summary>
	public class GetAvailableTags : ValueListCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public GetAvailableTags()
		{
			Command = "tagtypes";
			Key = ResponseParserKeys.TagType;
		}
	}
}
