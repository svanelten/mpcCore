using McMaster.Extensions.CommandLineUtils;
using System.Threading.Tasks;

namespace MpcCore.Cli.Commands.Player
{
	[Command(
		Name = "prev",
		FullName = "prev",
		Description = "Skips to the previous queue item")]
	[HelpOption]
	public class PrevCommand : BaseCommand
	{
		public async Task OnExecuteAsync(IConsole console)
		{
			var client = await GetClient();
			var re = await client.SendAsync(new MpcCore.Commands.Player.Previous());

			if (!await HandleError(re.Status))
			{
				await ShowCurrentTrack();
			}
		}
	}
}
