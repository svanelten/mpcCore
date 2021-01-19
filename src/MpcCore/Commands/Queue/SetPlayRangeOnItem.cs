using MpcCore.Commands.Base;
using MpcCore.Extensions;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// <![CDATA[MPD version >= 0.19]]>
	/// Specifies a portion of an item that shall be played. 
	/// rangeStart and rangeEnd are offsets in seconds (fractional seconds allowed); both are optional. 
	/// Omitting both (i.e. sending just “:”) means “remove the range, play everything”. 
	/// An item that is currently playing cannot be manipulated this way.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class SetPlayRangeOnItem : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="id">item id</param>
		/// <param name="rangeStart">range start</param>
		/// <param name="rangeEnd">range end</param>
		public SetPlayRangeOnItem(int id, int? rangeStart = null, int? rangeEnd = null)
		{
			Command = $"rangeid {id} {rangeStart.GetParamString()}:{rangeEnd.GetParamString()}";
		}
	}
}
