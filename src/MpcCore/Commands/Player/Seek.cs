using MpcCore.Commands.Base;

namespace MpcCore.Commands.Player
{
	public class Seek : SimpleCommandBase
	{
		public Seek(double seconds)
		{
			Command = $"seekcur {seconds.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture)}";
		}
	}
}
