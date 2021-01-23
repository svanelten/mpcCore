using McMaster.Extensions.CommandLineUtils;
using System;
using System.Threading.Tasks;

namespace MpcCore.Cli.Commands.Status
{
	[Command(
		Name = "stats",
		FullName = "statistics",
		Description = "Gets mpd statistics")]
	public class StatisticsCommand : BaseCommand
	{
		public async Task OnExecuteAsync(IConsole console)
		{
			var client = await GetClient();
			var response = await client.SendAsync(new MpcCore.Commands.Status.GetStatistics());

			await HandleError(response.Status);

			if (response.Result != null)
			{
				console.WriteLine($"mpd statistics:");
				console.WriteLine($" Songs: {response.Result.Songs}");
				console.WriteLine($" Albums: {response.Result.Albums}");
				console.WriteLine($" Artists: {response.Result.Artists}");
				console.WriteLine($" Time played: {await ShowDuration(TimeSpan.FromSeconds(response.Result.Playtime))}");
				console.WriteLine($" MPD uptime: {await ShowDuration(TimeSpan.FromSeconds(response.Result.Uptime))}");
				console.WriteLine($" Database playtime: {await ShowDuration(TimeSpan.FromSeconds(response.Result.DatabasePlaytime))}");
				console.WriteLine($" Database last update: {(response.Result.DatabaseLastUpdate.HasValue ? response.Result.DatabaseLastUpdate.Value.ToString("o") : "n/a")}");

			}
		}
	}
}
