using MpcCore.Commands.Base;
using MpcCore.Extensions;

namespace MpcCore.Commands.Player
{
	/// <summary>
	/// Pause playback.
	/// Will toggle playback pausing unless state is set explicitly
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#playback-options"/>
	/// </summary>
	public class Pause : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="state">optional pause state</param>
		public Pause(bool? state = null)
		{
			Command = $"pause {state.GetParamString()}";
		}
	}
}
