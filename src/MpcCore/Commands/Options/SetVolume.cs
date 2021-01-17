using MpcCore.Commands.Base;
using System;

namespace MpcCore.Commands.Options
{
	/// <summary>
	/// Sets volume, the range of volume is 0-100.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#playback-options"/>
	/// </summary>
	public class SetVolume : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="volume">volume level</param>
		public SetVolume(int volume)
		{
			if (volume < 0 || volume > 100)
			{
				throw new ArgumentOutOfRangeException("Volume can only be set to a value from 0 to 100");
			}

			Command = $"setvol {volume}";
		}
	}
}
