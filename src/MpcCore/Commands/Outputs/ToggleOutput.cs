using MpcCore.Commands.Base;

namespace MpcCore.Commands.Outputs
{
	/// <summary>
	/// Turns an output on or off, depending on the current state.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#audio-output-devices"/>
	/// </summary>
	public class ToggleOutput : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="id">output id</param>
		public ToggleOutput(int id)
		{
			Command = $"toggleoutput {id}";
		}
	}
}
