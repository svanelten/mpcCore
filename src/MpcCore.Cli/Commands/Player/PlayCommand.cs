using McMaster.Extensions.CommandLineUtils;
using System.Threading.Tasks;

namespace MpcCore.Cli.Commands.Player
{
	[Command(Name = "play",
				FullName = "play",
				Description = "Starts playback")]
	[HelpOption]
	public class PlayCommand : BaseCommand
	{
		[Argument(0, "Position", "Song position to play")]
		public int? Position { get; set; }

		public async Task OnExecuteAsync(IConsole console)
		{
			var client = await GetClient();
			var re = await client.SendAsync(new MpcCore.Commands.Player.Play(Position));

			if (!await HandleError(re.Status))
			{
				await ShowCurrentTrack();
			}
		}
	}
}
