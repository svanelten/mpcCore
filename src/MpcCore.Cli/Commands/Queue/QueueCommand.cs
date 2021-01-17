using McMaster.Extensions.CommandLineUtils;
using System.Linq;
using System.Threading.Tasks;

namespace MpcCore.Cli.Commands.Queue
{
	[Command(
		Name = "queue",
		FullName = "queue",
		Description = "lists the current play queue")]
	[HelpOption]
	public class QueueCommand : BaseCommand
	{
		[Option("-c|--clear", CommandOptionType.NoValue, LongName = "clear", Description = "Clears the current queue")]
		public bool Clear { get; }

		[Option("-a|--add", CommandOptionType.MultipleValue, LongName = "add", Description = "Adds the given file to the queue")]
		public string[] Add { get; }

		[Option("-i|--addid", CommandOptionType.SingleValue, LongName = "addid", Description = "Adds the song to the queue and returns the song id. Optional: position param to add the song at the given position")]
		public string AddId { get; }

		[Option("-p|--position", CommandOptionType.SingleValue, LongName = "position", Description = "When adding a song, adds it at the given position")]
		public string Position { get; }

		public async Task OnExecuteAsync(IConsole console)
		{
			var client = await GetClient();

			// clear command is always executed first and, not more happens
			if (Clear)
			{
				var clearResponse = await client.SendAsync(new MpcCore.Commands.Queue.ClearQueue());
				if (!await HandleError(clearResponse.Status))
				{
					console.WriteLine($"queue cleared");
				}

				return;
			}

			// add a list of given paths to the queue
			// does not return but gets the updated list
			if (Add != null && Add.Any())
			{
				foreach (var item in Add)
				{
					var addResponse = await client.SendAsync(new MpcCore.Commands.Queue.AddToQueue(item));

					if (Verbose)
					{
						if (!await HandleError(addResponse.Status))
						{
							console.WriteLine($"added '{item}' to queue");
						}
						else
						{
							console.WriteLine($"failed to add '{item}' to queue");
						}
					}
				}
			}

			// add a single song to the queue and returns the id.
			//if (!string.IsNullOrEmpty(AddId))
			//{
			//	var addResponse = await client.SendAsync(new MpcCore.Commands.Queue.Add(item));

			//	if (Verbose)
			//	{
			//		if (!await HandleError(addResponse.Status))
			//		{
			//			console.WriteLine($"added '{item}' to queue");
			//		}
			//		else
			//		{
			//			console.WriteLine($"failed to add '{item}' to queue");
			//		}
			//	}
			//}

			// list queue content
			var response = await client.SendAsync(new MpcCore.Commands.Queue.GetQueue());

			if (!await HandleError(response.Status))
			{
				console.WriteLine($"Queue has {response.Result.Count} items");

				response.Result.Items.ToList().ForEach(e => console.WriteLine(string.IsNullOrEmpty(e.Name) ? "n/a" : e.Name));
			}
		}
	}
}
