using MpcCore.Contracts;
using MpcCore.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MpcCore
{
	/// <summary>
	/// The connection encapsulates the actual TCP connection to the MPD server
	/// and does all the sending and reading.
	/// Should not be used directly.
	/// </summary>
	public class MpcCoreConnection : IMpcCoreConnection
	{
		private readonly IPEndPoint _endPoint;
		private TcpClient _tcpClient;
		private NetworkStream _networkStream;
		private StreamReader _reader;
		private StreamWriter _writer;
		private string _version = "n/a";

		/// <summary>
		/// The MPD server version.
		/// Value is available after connecting
		/// </summary>
		public string Version => _version;

		/// <summary>
		/// Creates a new connection to the given endpoint
		/// </summary>
		/// <param name="iPEndPoint">ipEndpoint for the MPD server</param>
		public MpcCoreConnection(IPEndPoint iPEndPoint)
		{
			if (IsConnected)
			{
				return;
			}

			_disconnect();
			_endPoint = iPEndPoint;
		}

		/// <summary>
		/// Creates a new connection to an MPD server with the given info
		/// Default is a local MPD server at 127.0.0.1:6600
		/// </summary>
		/// <param name="ip">ip address</param>
		/// <param name="port">port</param>
		public MpcCoreConnection(string ip = "127.0.0.1", string port = "6600")
		{
			if (IsConnected)
			{
				return;
			}

			_disconnect();
			_endPoint = new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(port));
		}

		/// <summary>
		/// Current connection state
		/// </summary>
		public bool IsConnected => _tcpClient?.Connected ?? false;

		/// <summary>
		/// Tries to connect to the MPD server.
		/// </summary>
		/// <returns>Task</returns>
		public async Task ConnectAsync()
		{
			if (_endPoint == null)
			{
				throw new InvalidOperationException("Server endpoint is not set.");
			}

			if (IsConnected)
			{
				return;
			}

			_tcpClient = new TcpClient();
			await _tcpClient.ConnectAsync(_endPoint.Address, _endPoint.Port);

			_networkStream = _tcpClient.GetStream();
			_reader = new StreamReader(_networkStream);
			_writer = new StreamWriter(_networkStream) { NewLine = "\n" };

			var firstLine = _reader.ReadLine();

			if (!firstLine.StartsWith(Constants.FirstLinePrefix))
			{
				await DisconnectAsync();
				throw new InvalidDataException($"Unexpected response from MPD, does not start with '{Constants.FirstLinePrefix}.");
			}

			_version = firstLine.Substring(Constants.FirstLinePrefix.Length);
		}

		/// <summary>
		/// Closes the current connection to the MPD server
		/// </summary>
		/// <returns>Task</returns>
		public Task DisconnectAsync()
		{
			if (IsConnected)
			{
				_disconnect();
			}

			return Task.CompletedTask;
		}

		/// <summary>
		/// Sends the given raw string command to the MPD server and returns the response as string list.
		/// There is no error handling on this, use carefully.
		/// </summary>
		/// <param name="command">mpd command</param>
		/// <returns>Task<List<string>> response</returns>
		public async Task<List<string>> SendCommandAsync(string command)
		{
			var response = new List<string>();

			if (string.IsNullOrEmpty(command))
			{
				return response;
			}

			await CheckConnectionAsync();

			_writer.WriteLine(command.Trim());
			_writer.Flush();

			return await ReadResponseAsync();
		}

		/// <summary>
		/// Send a predefined command to the MPD server.
		/// Returns a 
		/// </summary>
		/// <typeparam name="T">result type T depending on the command</typeparam>
		/// <param name="command">MpcCoreCommand</param>
		/// <returns>Task with result<T></returns>
		public async Task<IMpcCoreResponse<T>> SendAsync<T>(IMpcCoreCommand<T> command)
		{
			if (command == null)
			{
				return new MpcCoreResponse<T>(command, new MpcCoreResponseStatus
				{
					HasError = true,
					ErrorMessage = "Command is null or empty"
				});
			}

			var connected = await CheckConnectionAsync();
			var response = new List<string>();

			try
			{
				_writer.WriteLine(command.Command);
				_writer.Flush();

				response = await ReadResponseAsync();
			}
			catch (Exception exception)
			{
				try
				{
					await DisconnectAsync();
				}
				catch (Exception)
				{
					// TODO handle exception correctly	
				}

				return new MpcCoreResponse<T>(command, new MpcCoreResponseStatus
				{
					HasError = true,
					ErrorMessage = $"An exception occured: {exception.Message} in {exception.Source}"
				});
			}

			return await new MpcCoreResponse<T>(command, response).CreateResult();
		}

		/// <summary>
		/// Checks if there is a connection to MPD, will try to reconnect if not
		/// </summary>
		/// <returns>Task<bool> connected</returns>
		private async Task<bool> CheckConnectionAsync()
		{
			if (!IsConnected)
			{
				await ConnectAsync();
			}

			return IsConnected;
		}

		/// <summary>
		/// Reads the response from the MPD server and creates a string list from it
		/// </summary>
		/// <returns>Task<List<string>></returns>
		private async Task<List<string>> ReadResponseAsync()
		{
			var response = new List<string>();

			// Read response untli reach end token (OK or ACK)
			string responseLine;
			do
			{
				responseLine = await _reader.ReadLineAsync() ?? string.Empty;
				response.Add(responseLine);
			}
			while (!(responseLine.Equals(Constants.Ok) || responseLine.StartsWith(Constants.Ack)));

			return response.Where(s => !string.IsNullOrEmpty(s)).ToList();
		}

		/// <summary>
		/// Disconnects everything
		/// </summary>
		private void _disconnect()
		{
			_writer?.Dispose();
			_reader?.Dispose();
			_networkStream?.Dispose();
			_tcpClient?.Dispose();
		}
	}
}
