using MpcCore.Commands.Base;

namespace MpcCore.Commands.Options
{
	/// <summary>
	/// Sets consume playback option.
	/// When consume is activated, each item played is removed from queue.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#playback-options"/>
	/// </summary>
	public class SetConsume : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="state">consume state</param>
		public SetConsume(bool state)
		{
			Command = $"consume {(state ? "1" : "0")}";
		}
	}
}
