using MpcCore.Commands.Base;

namespace MpcCore.Commands.Queue
{
	public class AddToQueue : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">path of file or directory</param>
		public AddToQueue(string path)
		{
			Command = $"add \"{path}\"";
		}
	}
}
