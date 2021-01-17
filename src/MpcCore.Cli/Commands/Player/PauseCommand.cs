using McMaster.Extensions.CommandLineUtils;
using System.Threading.Tasks;

namespace MpcCore.Cli.Commands.Player
{
	[Command(
		Name = "pause",
		FullName = "pause",
		Description = "pauses playback")]
	[HelpOption]
	public class PauseCommand : BaseCommand
	{
		[Argument(0, "State", "Set pause state explicitly")]
		public bool? State { get; set; }

		public async Task OnExecuteAsync(IConsole console)
		{
			var client = await GetClient();
			var re = await client.SendAsync(new MpcCore.Commands.Player.Pause(State));

			if (!await HandleError(re.Status))
			{
				console.WriteLine($"paused");
			}
		}
	}
}
