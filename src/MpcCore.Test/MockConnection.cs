using MpcCore.Contracts;
using MpcCore.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MpcCore.Test
{
	public class MockConnection : IMpcCoreConnection
	{
		public Dictionary<string, List<string>> ResultMap;

		public string Version { get; } = "MOCK";

		public bool IsConnected { get; set; }

		public Task ConnectAsync()
		{
			IsConnected = true;

			return Task.CompletedTask;
		}

		public Task DisconnectAsync()
		{
			IsConnected = false;

			return Task.CompletedTask;
		}

		public async Task<IMpcCoreResponse<T>> SendAsync<T>(IMpcCoreCommand<T> command)
		{ 
			var response = GetConfiguredResponse(command.Command);
			
			return await new MpcCoreResponse<T>(command, response).CreateResult();
		}

		public Task<List<string>> SendCommandAsync(string command)
		{
			throw new NotImplementedException();
		}

		public List<string> GetConfiguredResponse(string command)
		{
			return (ResultMap != null && ResultMap.ContainsKey(command)) ? ResultMap[command] : new List<string>();
		}
	}
}
