using MpcCore.Contracts;
using System;
using System.Linq;

namespace MpcCore.Commands.Base
{
	/// <summary>
	/// Base functionality for commands that return an int
	/// </summary>
	public class IntCommandBase : IMpcCoreCommand<int>
	{
		public string Command { get; internal set; }

		public virtual int HandleResponse(IMpdResponse response)
		{
			if (response.IsErrorResponse)
			{
				return 0;
			}

			return Convert.ToInt32(response.GetContent().First() ?? string.Empty);
		}
	}
}
