using MpcCore.Commands.Base;

namespace MpcCore.Commands.Partition
{
	/// <summary>
	/// Create a new partition.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#partition-commands"/>
	/// </summary>
	public class AddPartition : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="partition">new partition name</param>
		public AddPartition(string partition)
		{
			Command = $"newpartition {partition}";
		}
	}
}
