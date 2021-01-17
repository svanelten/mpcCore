using MpcCore.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MpcCore
{
	/// <summary>
	/// The MPD client.
	/// Disposable for a clean disconnect.
	/// </summary>
	public class MpcCoreClient : IDisposable
	{
		private IMpcCoreConnection _connection;

		public bool IsConnected => _connection?.IsConnected ?? false;

		public MpcCoreClient(IMpcCoreConnection connection)
		{
			_connection = connection;
		}

		public async Task<List<string>> SendRawCommandAsync(string command)
		{
			return await _connection.SendCommandAsync(command);
		}

		public async Task<IMpcCoreResponse<T>>SendAsync<T>(IMpcCoreCommand<T> command)
		{
			return await _connection.SendAsync(command);
		}

		public async Task<bool> ConnectAsync()
		{
			await _connection.ConnectAsync();
			return _connection.IsConnected;
		}

		public async Task DisconnectAsync()
		{
			await _connection.DisconnectAsync();
		}

		public string Version()
		{
			return _connection.Version;
		}

		public void Dispose()
		{
			DisconnectAsync().Wait();
		}
	}
}
