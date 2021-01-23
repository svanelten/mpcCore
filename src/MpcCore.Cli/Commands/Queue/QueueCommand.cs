using McMaster.Extensions.CommandLineUtils;
using System.Linq;
using System.Threading.Tasks;

namespace MpcCore.Cli.Commands.Queue
{
	[Command(
		Name = "queue",
		FullName = "queue",
		Description = "lists the current play queue by default")]
	[HelpOption]
	public class QueueCommand : BaseCommand
	{
		[Option("-c|--clear", CommandOptionType.NoValue, LongName = "clear", Description = "Clears the current queue")]
		public bool Clear { get; }

		[Option("-a|--add", CommandOptionType.MultipleValue, LongName = "add", Description = "Adds one or more files to the queue, identified by either the full path to the file or a relative path from the MPD music directory.")]
		public string[] Add { get; }

		[Option("-d|--delete", CommandOptionType.NoValue, LongName = "delete", Description = "Deletes the item at the given position from the queue.")]
		public bool Delete { get; } = false;

		[Option("-p|--position", CommandOptionType.SingleValue, LongName = "position", Description = "When adding or deleting a song, use this parameter to select the position to insert at or remove. First item of the queue by default.")]
		public int Position { get; } = 0;

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

			if (Delete)
			{
				var deleteResponse = await client.SendAsync(new MpcCore.Commands.Queue.DeleteFromQueue(Position));
				await HandleError(deleteResponse.Status);
			}

			// add a list of given paths to the queue
			// does not return but gets the updated list
			if (Add != null && Add.Any())
			{
				foreach (var item in Add)
				{
					var addResponse = await client.SendAsync(new MpcCore.Commands.Queue.AddToQueueAndGetId(item, Position));

					if (Verbose)
					{
						if (!await HandleError(addResponse.Status))
						{
							console.WriteLine($"added '{item}' ({addResponse.Result}) to queue");
						}
						else
						{
							console.WriteLine($"failed to add '{item}' to queue");
						}
					}
				}
			}

			// list queue content
			var response = await client.SendAsync(new MpcCore.Commands.Queue.GetQueue());

			if (!await HandleError(response.Status))
			{
				if (response.Result != null)
				{
					console.WriteLine($"Queue has {response.Result.Count} items");
					response.Result.Items.ToList().ForEach(e => console.WriteLine(string.IsNullOrEmpty(e.Name) ? "n/a" : e.Name));
				}
				else
				{
					console.WriteLine($"Queue is empty");
				}
			}
		}
	}
}
