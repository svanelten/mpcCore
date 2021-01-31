using MpcCore.Commands.Base;

namespace MpcCore.Commands.Outputs
{
	/// <summary>
	/// Turns an output on.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#audio-output-devices"/>
	/// </summary>
	public class EnableOutput : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="id">output id</param>
		public EnableOutput(int id)
		{
			Command = $"enableoutput {id}";
		}
	}
}
