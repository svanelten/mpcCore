using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Threading.Tasks;

namespace MpcCore.Cli
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			await Host
				.CreateDefaultBuilder()
				.ConfigureServices((context, services) =>
				{
					// Add DI services here!
				})
				.RunCommandLineApplicationAsync<MainCommand>(args);
		}
	}

	[Command(Name = "mpccorecli",
		FullName = "mpccorecli",
		Description = "A MPD command line client based on MpcCore")]
	[VersionOptionFromMember("-v|--version", MemberName = "GetVersion")]
	[Subcommand(typeof(Commands.Player.PlayCommand))]
	[Subcommand(typeof(Commands.Player.StopCommand))]
	[Subcommand(typeof(Commands.Player.PauseCommand))]
	[Subcommand(typeof(Commands.Player.PrevCommand))]
	[Subcommand(typeof(Commands.Player.NextCommand))]
	[Subcommand(typeof(Commands.Status.StatusCommand))]
	[Subcommand(typeof(Commands.Queue.QueueCommand))]
	[Subcommand(typeof(Commands.Database.ListPathCommand))]
	[HelpOption]
	public class MainCommand
	{
		public int OnExecute(CommandLineApplication app)
		{
			app.ShowHelp();
			return 1;
		}

		private string GetVersion()
		{
			return typeof(MainCommand)
				.Assembly?
				.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
				.InformationalVersion;
		}
	}
}