using MpcCore.Commands.Base;

namespace MpcCore.Commands.Options
{
	/// <summary>
	/// Sets repeat playback option.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#playback-options"/>
	/// </summary>
	public class SetRepeat : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="state">repeat state</param>
		public SetRepeat(bool state)
		{
			Command = $"repeat {(state ? "1" : "0")}";
		}
	}
}
