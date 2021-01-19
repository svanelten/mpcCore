using MpcCore.Commands.Base;

namespace MpcCore.Commands.Options
{
	/// <summary>
	/// Sets random playback option.
	/// When random is activated, queue items are played in random order.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#playback-options"/>
	/// </summary>
	public class SetRandom : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="state">random state</param>
		public SetRandom(bool state)
		{
			Command = $"random {(state ? "1" : "0")}";
		}
	}
}
