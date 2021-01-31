using MpcCore.Contracts;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Partition
{
	/// <summary>
	/// Remove one or more tags from the list of tag types the client is interested in. These will be omitted from responses to this client.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#partition-commands"/>
	/// </summary>
	public class ListPartitions : IMpcCoreCommand<IEnumerable<string>>
	{
		public string Command { get; internal set; } = "listpartitions";

		public IEnumerable<string> HandleResponse(IMpdResponse response)
		{
			var parser = new ResponseParser(response);

			return parser.GetValueList(ResponseParserKeys.Partition);
		}
	}
}
