using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Base
{
	/// <summary>
	/// Base functionality for commands that return a job response
	/// </summary>
	public abstract class JobCommandBase : IMpcCoreCommand<IJob>
	{
		public string Command { get; internal set; }

		public virtual IJob HandleResponse(IEnumerable<string> response)
		{
			var parser = new ResponseParser(response);

			if (parser.ResponseHasMpdError)
			{
				return null;
			}

			return parser.GetJobInformation();
		}
	}
}
