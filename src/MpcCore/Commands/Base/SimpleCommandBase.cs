using MpcCore.Contracts;

namespace MpcCore.Commands.Base
{
	public class SimpleCommandBase : IMpcCoreCommand<bool>
	{
		public string Command { get; internal set; }

		public virtual bool HandleResponse(IMpdResponse response)
		{
			return response.IsOkResponse;
		}
	}
}
