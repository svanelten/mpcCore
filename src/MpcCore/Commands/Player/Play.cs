using MpcCore.Commands.Base;
using MpcCore.Extensions;

namespace MpcCore.Commands.Player
{
	public class Play : SimpleCommandBase
	{
		public Play(int? position = null)
		{
			Command = $"play {position.GetParamString()}";
		}
	}
}
