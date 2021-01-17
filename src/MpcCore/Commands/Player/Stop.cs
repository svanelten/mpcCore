using MpcCore.Commands.Base;

namespace MpcCore.Commands.Player
{
	public class Stop : SimpleCommandBase
	{
		public Stop()
		{
			Command = "stop";
		}
	}
}
