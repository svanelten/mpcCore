using MpcCore.Commands.Base;

namespace MpcCore.Commands.Partition
{
	/// <summary>
	/// Move an output to the current partition.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#partition-commands"/>
	/// </summary>
	public class MoveOutputToPartition : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="outputName">The output to move to the current partition</param>
		public MoveOutputToPartition(string outputName)
		{
			Command = $"moveoutput {outputName}";
		}
	}
}
