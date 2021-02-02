using MpcCore.Commands.Base;
using MpcCore.Response;

namespace MpcCore.Commands.Partition
{
	/// <summary>
	/// Remove one or more tags from the list of tag types the client is interested in. These will be omitted from responses to this client.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#partition-commands"/>
	/// </summary>
	public class ListPartitions : ValueListCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public ListPartitions()
		{ 
			Command = "listpartitions";
			Key = ResponseParserKeys.Partition;
		}
	}
}
