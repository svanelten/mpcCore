using MpcCore.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MpcCore
{
	public interface IMpcCoreConnection
	{
		string Version { get; }
		bool IsConnected { get; }
		Task ConnectAsync();
		Task DisconnectAsync();
		Task<IMpdResponse> SendAsync<T>(IMpcCoreCommand<T> command);
		Task<IEnumerable<string>> SendCommandAsync(string command);
	}
}