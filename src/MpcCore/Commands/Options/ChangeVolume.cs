using MpcCore.Commands.Base;
using System;

namespace MpcCore.Commands.Options
{
	/// <summary>
	/// Changes volume by the given amount. Deprecated, use <see cref="SetVolume"/> instead.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#playback-options"/>
	/// </summary>
	[Obsolete("Deprecated command. Please use 'SetVolume'", false)]
	public class ChangeVolume : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="change">volume change value</param>
		public ChangeVolume(int change)
		{
			Command = $"volume {change}";
		}
	}
}
