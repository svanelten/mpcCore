using McMaster.Extensions.CommandLineUtils;
using MpcCore.Extensions;
using System;
using System.Threading.Tasks;

namespace MpcCore.Cli.Commands.Status
{
	[Command(
		Name = "status",
		FullName = "status",
		Description = "Gets mpd status")]
	public class StatusCommand : BaseCommand
	{
		public async Task OnExecuteAsync(IConsole console)
		{
			var client = await GetClient();
			var response = await client.SendAsync(new MpcCore.Commands.Status.GetStatus());

			await HandleError(response.Status);

			if (response.Result != null)
			{
				console.WriteLine($"mpd status:");
				console.WriteLine($" Consume: {response.Result.Consume.GetDisplayString()}");
				console.WriteLine($" Random: {response.Result.Random.GetDisplayString()}");
				console.WriteLine($" Repeat: {response.Result.Repeat.GetDisplayString()}");
				console.WriteLine($" Single: {response.Result.Single.GetDisplayString()}");
				console.WriteLine($" Volume: {response.Result.Volume} 🔊");
				console.WriteLine($" State: {response.Result.State}");
				console.WriteLine(await ShowDuration(TimeSpan.FromSeconds(response.Result.Elapsed), " Elapsed: "));
				console.WriteLine($" Song ID: {response.Result.Song}");
			}
		}
	}
}
