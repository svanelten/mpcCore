using MpcCore.Commands.Base;
using MpcCore.Extensions;

namespace MpcCore.Commands.Options
{
	/// <summary>
	/// Additional time subtracted from the overlap calculated by mixrampdb. 
	/// Set seconds value to null to disable MixRamp overlapping and fall back to crossfading.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#playback-options"/>
	/// </summary>
	public class SetMixRampDelay : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="seconds">number of seconds, set null to disable</param>
		public SetMixRampDelay(int? seconds)
		{
			Command = $"mixrampdelay {seconds.GetParamString("nan")}";
		}
	}
}
