using McMaster.Extensions.CommandLineUtils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MpcCore.Cli.Commands
{
	[Command(
		Name = "raw",
		FullName = "raw",
		Description = "Send raw command")]
	public class RawCommand : BaseCommand
	{
		[Argument(0, "Command", "raw command")]
		public string Command { get; set; }

		public async Task OnExecuteAsync(IConsole console)
		{
			var client = await GetClient();

			try
			{
				var response = await client.SendRawCommandAsync(Command);


				console.ForegroundColor = ConsoleColor.Gray;
				console.Write($"❯ {Command}" );
				console.ForegroundColor = ConsoleColor.White;

				if (response == null || !response.Any())
				{

					console.ForegroundColor = ConsoleColor.Yellow;
					console.WriteLine(" - no response content");
					console.ForegroundColor = ConsoleColor.White;
				}
				else
				{
					if (response.Last().StartsWith("OK"))
					{
						console.ForegroundColor = ConsoleColor.Green;
						console.WriteLine(" OK");
						console.ForegroundColor = ConsoleColor.White;
					}
					else
					{
						console.ForegroundColor = ConsoleColor.Red;
						console.WriteLine(" ERROR");
						console.WriteLine(response.Last());
						console.ForegroundColor = ConsoleColor.White;
					}

					foreach(var line in response.Take(response.Count() - 1).ToList())
					{
						console.WriteLine(line);
					}
				}
			}
			catch (Exception ex)
			{
				console.ForegroundColor = ConsoleColor.Red;
				console.WriteLine("❌ Exception");
				console.ForegroundColor = ConsoleColor.White;
				console.WriteLine($"{ex.GetType()}: {ex.Message}");
				console.Write($"{ex.StackTrace}");

				if (ex.InnerException != null)
				{
					console.WriteLine($"Inner: {ex.InnerException.GetType()}: {ex.InnerException.Message}");
					console.Write($"{ex.InnerException.StackTrace}");
				}
			}
		}
	}
}
