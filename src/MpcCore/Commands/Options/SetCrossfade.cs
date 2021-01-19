using MpcCore.Commands.Base;

namespace MpcCore.Commands.Options
{
	/// <summary>
	/// Set the crossfading between queue items.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#playback-options"/>
	/// </summary>
	public class SetCrossfade : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="state">crossfade time in seconds</param>
		public SetCrossfade(int seconds)
		{
			Command = $"crossfade {seconds}";
		}
	}
}
