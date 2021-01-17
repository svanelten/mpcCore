using MpcCore.Commands.Base;

namespace MpcCore.Commands.Player
{
	public class PlayId : SimpleCommandBase
	{

		public PlayId(string songId)
		{
			Command = $"playid {songId}";
		}
	}
}
