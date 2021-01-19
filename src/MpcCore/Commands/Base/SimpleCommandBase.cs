using MpcCore.Contracts;
using MpcCore.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace MpcCore.Commands.Base
{
	public class SimpleCommandBase : IMpcCoreCommand<bool>
	{
		public string Command { get; internal set; }

		public virtual bool HandleResponse(IEnumerable<string> response)
		{
			return response.ToList().IsBasicOkResponse();
		}
	}
}
