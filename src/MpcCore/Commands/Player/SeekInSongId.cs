using MpcCore.Commands.Base;

namespace MpcCore.Commands.Player
{
	public class SeekInSongId : SimpleCommandBase
	{
		public SeekInSongId(string itemId, double seconds)
		{
			Command = $"seekid {itemId} {seconds:#.##}";
		}
	}
}
