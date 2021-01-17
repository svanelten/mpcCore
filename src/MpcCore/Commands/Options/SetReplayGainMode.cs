using MpcCore.Commands.Base;
using System;
using System.Linq;

namespace MpcCore.Commands.Options
{
	/// <summary>
	/// Sets the replay gain mode. 
	/// Possible values are:
	///		off
	///		track
	///		album
	///		auto
	///	Changing the mode during playback may take several seconds, because the new settings do not affect the buffered data. This command triggers the <see cref="Status.SetIdle"/> idle event.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#playback-options"/>
	/// </summary>
	public class SetReplayGainMode : SimpleCommandBase
	{
		private static readonly string[] _availableOptions = { "off", "track", "album", "auto" };

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="mode">gain mode</param>
		public SetReplayGainMode(string mode)
		{
			if (!_availableOptions.Contains(mode.ToLower()))
			{
				throw new ArgumentOutOfRangeException();
			}

			Command = $"replay_gain_mode {mode}";
		}
	}
}
