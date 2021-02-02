using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Mounts
{
	/// <summary>
	/// Queries a list of “neighbors” (e.g. accessible file servers on the local net).
	/// Items on that list may be used with the <see cref="AddMount"/> command.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#mounts-and-neighbors"/>
	/// </summary>
	public class ListNeighbors : IMpcCoreCommand<IEnumerable<INeighbor>>
	{
		public string Command { get; internal set; } = "listmounts";

		public IEnumerable<INeighbor> HandleResponse(IMpdResponse response)
		{
			var parser = new ResponseParser(response);

			return parser.GetListedNeighbors();
		}
	}
}
