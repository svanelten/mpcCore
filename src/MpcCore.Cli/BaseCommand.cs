using McMaster.Extensions.CommandLineUtils;
using MpcCore.Contracts;
using System;
using System.Threading.Tasks;

namespace MpcCore.Cli
{
	public class BaseCommand : IDisposable
	{
		[Option("-v|--verbose", CommandOptionType.NoValue, LongName = "verbose", Description = "Sets the output to reeeeeally chatty mode")]
		public bool Verbose { get; }

		private MpcCoreClient _client;

		public async Task<MpcCoreClient> GetClient()
		{
			if (_client == null)
			{
				_client = new MpcCoreClient(new MpcCoreConnection("10.76.0.5", "6600"));
				await _client.ConnectAsync();
			}

			return _client;
		}

		public virtual async Task<bool> HandleError(IMpcCoreResponseStatus status)
		{
			if (status.HasError || status.HasMpdError)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("error: ");
				Console.ForegroundColor = ConsoleColor.White;

				if (status.HasMpdError)
				{
					Console.WriteLine("mpd error");

					if (status.HasMpdError && status.MpdError != null)
					{
						Console.Write("error code ");
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.Write(status.MpdError.Code);
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write(" at line ");

						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.Write(status.MpdError.Line);
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write(": ");

						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.WriteLine(status.MpdError.Message);
						Console.ForegroundColor = ConsoleColor.White;
					}
					else
					{
						Console.WriteLine(status.ErrorMessage);
					}
				}
				else
				{
					Console.WriteLine("mpc error");
					Console.WriteLine(status.ErrorMessage);
				}

				return true;
			}

			return false;
		}

		public virtual async Task ShowCurrentTrack()
		{
			var item = await _client.SendAsync(new MpcCore.Commands.Status.GetCurrentSong());
			Console.WriteLine($"Artist: {item.Result.Artist},  Name: {item.Result.Name} Title: {item.Result.Title}");
		}

		public void Dispose()
		{
			_client.DisconnectAsync().Wait();
		}
	}
}
