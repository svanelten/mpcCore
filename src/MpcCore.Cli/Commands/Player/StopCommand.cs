using McMaster.Extensions.CommandLineUtils;
using System.Threading.Tasks;

namespace MpcCore.Cli.Commands.Player
{
	[Command(
		Name = "stop",
		FullName = "stop",
		Description = "Stops playback")]
	[HelpOption]
	public class StopCommand : BaseCommand
	{
		public async Task OnExecuteAsync(IConsole console)
		{
			var client = await GetClient();
			var re = await client.SendAsync(new MpcCore.Commands.Player.Stop());

			if (!await HandleError(re.Status))
			{
				console.WriteLine($"stopped");
			}
		}
	}
}
