using MpcCore.Contracts;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Base
{
	/// <summary>
	/// Base functionality for commands that return a list of values for one key
	/// </summary>
	public abstract class ValueListCommandBase : IMpcCoreCommand<IEnumerable<string>>
	{
		public string Command { get; internal set; }

		public string Key { get; internal set; }

		public virtual IEnumerable<string> HandleResponse(IMpdResponse response)
		{
			if (response.IsErrorResponse)
			{
				return null;
			}

			var parser = new ResponseParser(response);

			return parser.GetValueList(Key);
		}
	}
}
