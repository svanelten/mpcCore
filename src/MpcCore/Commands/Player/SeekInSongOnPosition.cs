using MpcCore.Commands.Base;

namespace MpcCore.Commands.Player
{
	public class SeekInSongOnPosition : SimpleCommandBase
	{
		public SeekInSongOnPosition(int position, double seconds)
		{
			Command = $"seek {position} {seconds.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture)}";
		}
	}
}
