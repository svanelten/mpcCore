using McMaster.Extensions.CommandLineUtils;
using System.Threading.Tasks;

namespace MpcCore.Cli.Commands.Player
{
	[Command(
		Name = "next",
		FullName = "next",
		Description = "Skips ahead to the queue item")]
	[HelpOption]
	public class NextCommand : BaseCommand
	{
		public async Task OnExecuteAsync(IConsole console)
		{
			var client = await GetClient();
			var re = await client.SendAsync(new MpcCore.Commands.Player.Next());

			if (!await HandleError(re.Status))
			{
				await ShowCurrentTrack();
			}
		}
	}
}
