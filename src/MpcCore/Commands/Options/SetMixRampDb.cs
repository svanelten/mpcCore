using MpcCore.Commands.Base;

namespace MpcCore.Commands.Options
{
	/// <summary>
	/// Sets the threshold at which items will be overlapped. 
	/// Like crossfading but doesn’t fade the track volume, just overlaps. 
	/// The items need to have MixRamp tags added by an external tool. 
	/// 0dB is the normalized maximum volume so use negative values, MPD documentation recommends -17dB. 
	/// In the absence of mixramp tags crossfading will be used.
	/// <seealso cref="http://sourceforge.net/projects/mixramp"/>
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#playback-options"/>
	/// </summary>
	public class SetMixRampDb : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="state">decibel value</param>
		public SetMixRampDb(int decibels)
		{
			Command = $"mixrampdb {decibels}";
		}
	}
}
