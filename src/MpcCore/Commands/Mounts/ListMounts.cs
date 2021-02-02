using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Mounts
{
	/// <summary>
	/// Queries a list of all mounts. By default, this contains just the configured music directory.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#mounts-and-neighbors"/>
	/// </summary>
	public class ListMounts : IMpcCoreCommand<IEnumerable<IMount>>
	{
		public string Command { get; internal set; } = "listmounts";

		public IEnumerable<IMount> HandleResponse(IMpdResponse response)
		{
			var parser = new ResponseParser(response);

			return parser.GetListedMounts();
		}
	}
}
