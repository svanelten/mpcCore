using MpcCore.Commands.Base;

namespace MpcCore.Commands.Partition
{
	/// <summary>
	/// Delete a partition. The partition must be empty (no connected clients and no outputs).
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#partition-commands"/>
	/// </summary>
	public class DeletePartition : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="partition">new partition name</param>
		public DeletePartition(string partition)
		{
			Command = $"delpartition {partition}";
		}
	}
}
