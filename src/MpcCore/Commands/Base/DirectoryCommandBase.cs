using MpcCore.Contracts;
using MpcCore.Mpd;
using MpcCore.Response;

namespace MpcCore.Commands.Base
{
	/// <summary>
	/// Base functionality for commands that return a job response
	/// </summary>
	public abstract class DirectoryCommandBase : IMpcCoreCommand<IDirectory>
	{
		public string Command { get; internal set; }

		public string Path { get; internal set; }

		public virtual IDirectory HandleResponse(IMpdResponse response)
		{
			if (response.IsErrorResponse)
			{
				return null;
			}

			var parser = new ResponseParser(response);

			return parser.GetDirectoryListing(Path);
		}
	}
}
