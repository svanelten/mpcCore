using MpcCore.Commands.Base;

namespace MpcCore.Commands.Partition
{
	/// <summary>
	/// Switch the client to the given partition.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#partition-commands"/>
	/// </summary>
	public class SwitchToPartition : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="partition">The partition to switch to</param>
		public SwitchToPartition(string partition)
		{
			Command = $"partition {partition}";
		}
	}
}
