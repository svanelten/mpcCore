using MpcCore.Commands.Base;

namespace MpcCore.Commands.Outputs
{
	/// <summary>
	/// Turns an output off.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#audio-output-devices"/>
	/// </summary>
	public class DisableOutput : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="id">output id</param>
		public DisableOutput(int id)
		{
			Command = $"disableoutput {id}";
		}
	}
}
