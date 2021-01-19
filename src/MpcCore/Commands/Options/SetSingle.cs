using MpcCore.Commands.Base;

namespace MpcCore.Commands.Options
{
	/// <summary>
	/// Sets single playback state to true or false.
	/// When single is activated, playback is stopped after the current item, or the item is repeated if the ‘repeat’ mode is enabled.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#playback-options"/>
	/// </summary>
	public class SetSingle : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="state">consume state</param>
		public SetSingle(bool state)
		{
			Command = $"single {(state ? "1" : "0")}";
		}
	}
}
