using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;

namespace MpcCore.Commands.Base
{
	/// <summary>
	/// Base functionality for commands that return a job response
	/// </summary>
	public abstract class JobCommandBase : IMpcCoreCommand<IJob>
	{
		public string Command { get; internal set; }

		public virtual IJob HandleResponse(IMpdResponse response)
		{
			if (response.IsErrorResponse)
			{
				return null;
			}

			var parser = new ResponseParser(response);

			return parser.GetJobInformation();
		}
	}
}
