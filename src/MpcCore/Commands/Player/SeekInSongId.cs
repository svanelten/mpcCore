using MpcCore.Commands.Base;

namespace MpcCore.Commands.Player
{
	public class SeekInSongId : SimpleCommandBase
	{
		public SeekInSongId(string songId, double seconds)
		{
			Command = $"seekid {songId} {seconds:#.##}";
		}
	}
}
