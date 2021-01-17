using MpcCore.Commands.Base;

namespace MpcCore.Commands.Player
{
	public class Play : SimpleCommandBase
	{
		public Play(int? position = null)
		{
			Command = position.HasValue 
				? $"play {position.Value}"
				: "play";
		}
	}
}
