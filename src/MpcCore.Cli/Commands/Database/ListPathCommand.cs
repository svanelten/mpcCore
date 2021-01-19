using McMaster.Extensions.CommandLineUtils;
using MpcCore.Mpd;
using System.Linq;
using System.Threading.Tasks;

namespace MpcCore.Cli.Commands.Database
{
	[Command(
		Name = "listpath",
		FullName = "listpath",
		Description = "lists the directory structure for the given path. Empty values uses the root music directory.")]
	[HelpOption]
	public class ListPathCommand : BaseCommand
	{
		private IConsole _console;

		[Argument(0, "Path", "path to list")]
		public string Path { get; set; }

		public async Task OnExecuteAsync(IConsole console)
		{
			_console = console;

			var client = await GetClient();
			var response = await client.SendAsync(new MpcCore.Commands.Database.ListItems(Path));

			if (!await HandleError(response.Status))
			{
				console.WriteLine($"dir listing for '{Path}'");
				ListDir(response.Result);
			}
		}

		public void ListDir(IDirectory dir)
		{
			_console.WriteLine($"`---------------------------------------------");
			_console.WriteLine($"❯ Directory '{dir.Name}' at path '{dir.Path}'");
			if (dir.HasFiles)
			{
				dir.Files.ToList().ForEach(f => _console.WriteLine($"   file '{f.Name}' {f.Path}"));
			}
			if (dir.HasDirectories)
			{
				dir.Directories.ToList().ForEach(d => ListDir(d));
			}
		}
	}
}
