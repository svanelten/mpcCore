using MpcCore.Contracts;
using MpcCore.Mpd;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Base
{
	/// <summary>
	/// Base functionality for commands that return a job response
	/// </summary>
	public abstract class DirectoryCommandBase : IMpcCoreCommand<IDirectory>
	{
		public string Command { get; internal set; }

		public string Path { get; internal set; }

		public virtual IDirectory HandleResponse(IEnumerable<string> response)
		{
			var parser = new ResponseParser(response);

			if (parser.ResponseHasMpdError)
			{
				return null;
			}

			return parser.GetDirectoryListing(Path);
		}
	}
}
