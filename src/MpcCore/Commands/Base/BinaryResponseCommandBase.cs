using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;

namespace MpcCore.Commands.Base
{
	/// <summary>
	/// Base command for commands that return binary objects
	/// </summary>
	public abstract class BinaryResponseCommandBase : IMpcCoreCommand<IBinaryChunk>
	{
		/// <summary>
		/// Requested path to an item
		/// </summary>
		public string Path { get; internal set; }

		public string Command { get; internal set; }

		public virtual IBinaryChunk HandleResponse(IMpdResponse response)
		{
			return response.BinaryChunk;
		}
	}
}
