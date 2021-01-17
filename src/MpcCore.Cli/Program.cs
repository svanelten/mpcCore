using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

	[Command(Name = "sample-cli",
		FullName = "Sample CLI",
		Description = "A sample CLI tool.")]
	[VersionOptionFromMember(MemberName = "GetVersion")]
	[Subcommand(typeof(Commands.Player.PlayCommand))]
	[Subcommand(typeof(Commands.Player.StopCommand))]
	[Subcommand(typeof(Commands.Player.PauseCommand))]
	[Subcommand(typeof(Commands.Player.PrevCommand))]
	[Subcommand(typeof(Commands.Player.NextCommand))]
	[Subcommand(typeof(Commands.Status.StatusCommand))]
	[Subcommand(typeof(Commands.Queue.QueueCommand))]
	[HelpOption]
	public class MainCommand
	{
		public void OnExecute(CommandLineApplication app)
		{
			Console.WriteLine($"mpcCore v{GetVersion()}");
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