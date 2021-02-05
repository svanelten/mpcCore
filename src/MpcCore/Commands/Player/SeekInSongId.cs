using MpcCore.Commands.Base;

namespace MpcCore.Commands.Player
{
	public class SeekInSongId : SimpleCommandBase
	{
		public SeekInSongId(string itemId, double seconds)
		{
			Command = $"seekid {itemId} {seconds.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture)}";
		}
	}
}
