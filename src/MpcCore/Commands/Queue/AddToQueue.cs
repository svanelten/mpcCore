using MpcCore.Commands.Base;
using System;

namespace MpcCore.Commands.Queue
{
	public class AddToQueue : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="uri">Uri of file or directory</param>
		public AddToQueue(string uri)
		{
			Command = $"add {uri}";
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="uri">Uri of file or directory</param>
		public AddToQueue(Uri uri)
		{
			Command = $"add {uri}";
		}
	}
}
