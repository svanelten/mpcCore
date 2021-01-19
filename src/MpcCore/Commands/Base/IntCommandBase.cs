using MpcCore.Contracts;
using MpcCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MpcCore.Commands.Base
{
	/// <summary>
	/// Base functionality for commands that return an int
	/// </summary>
	public class IntCommandBase : IMpcCoreCommand<int>
	{
		public string Command { get; internal set; }

		public virtual int HandleResponse(IEnumerable<string> response)
		{
			if (response.ToList().IsErrorResponse())
			{
				return 0;
			}

			return Convert.ToInt32(response.First());
		}
	}
}
