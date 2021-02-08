using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Mpd;
using MpcCore.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
		/// Creates a new connection to a MPD server with the given info
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
			_reader = new StreamReader(_networkStream, Encoding.ASCII);
			_writer = new StreamWriter(_networkStream, Encoding.ASCII) { NewLine = "\n" };

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
		/// <returns>Task<IEnumerable<string>> response</returns>
		public async Task<IEnumerable<string>> SendCommandAsync(string command)
		{
			var response = new List<string>();

			if (string.IsNullOrEmpty(command))
			{
				return response;
			}

			await CheckConnectionAsync();

			_writer.WriteLine(command);
			_writer.Flush();

			var result = await ReadResponseAsync();

			return result.RawResponse;
		}

		/// <summary>
		/// Send a predefined command to the MPD server.
		/// Returns a 
		/// </summary>
		/// <typeparam name="T">result type T depending on the command</typeparam>
		/// <param name="command">MpcCoreCommand</param>
		/// <returns>Task with result<T></returns>
		public async Task<IMpdResponse> SendAsync<T>(IMpcCoreCommand<T> command)
		{
			var connected = await CheckConnectionAsync();

			_writer.WriteLine(command.Command);
			_writer.Flush();

			return (command.Command.StartsWith("readpicture") || command.Command.StartsWith("albumart"))
				? await ReadBinaryResponseAsync()
				: await ReadResponseAsync();
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
		private async Task<IMpdResponse> ReadResponseAsync()
		{
			var response = new MpdResponse();

			string responseLine;
			do
			{
				responseLine = await _reader.ReadLineAsync() ?? string.Empty;
				response.RawResponse.Add(responseLine);
			}
			while (!(responseLine.Equals(Constants.Ok) || responseLine.StartsWith(Constants.Ack)));

			return response;
		}

		private async Task<IMpdResponse> ReadBinaryResponseAsync()
		{
			var response = new MpdResponse() { BinaryChunk = new BinaryChunk() };

			try
			{
				int NewLine = 10;
				var encoding = new UTF8Encoding(false, false);

				int value;
				bool endReached = false;
				var stringBuffer = new MemoryStream();
				var stream = _reader.BaseStream;

				while (!endReached && (value = _reader.BaseStream.ReadByte()) != -1)
				{
					// if it's not the newline, keep buffering the string
					if (value != NewLine)
					{
						stringBuffer.WriteByte((byte)value);
						continue;
					}

					// Got a newline. If there's nothing in the stringBuffer, then just move on
					if (stringBuffer.Position == 0L)
					{
						continue;
					}

					// Buffered some string.
					var line = encoding.GetString(stringBuffer.ToArray());
					response.RawResponse.Add(line);

					if (line.StartsWith(Constants.Ack) || line.StartsWith(Constants.Ok))
					{
						endReached = true;
					}

					var split = line.Split(": ");
					if (split.Length > 1)
					{
						switch (split[0])
						{
							case "type":
								response.BinaryChunk.MimeType = split[1];
								break;
							case "offset":
								response.BinaryChunk.Offset = Convert.ToInt32(split[1]);
								break;
							case "size":
								response.BinaryChunk.FullLength = Convert.ToInt64(split[1]);
								break;
							case "binary":
								// data chunk follows immediately. Let's read it
								var length = Convert.ToInt32(split[1]);
								response.BinaryChunk.ChunkLength = length;
								response.BinaryChunk.Binary = new byte[length];
								var chunkOffset = 0;
								var bytesToRead = length; // this is how many bytes we need to read
								while (bytesToRead > 0)
								{
									// Read will not necessarily read as many bytes as we requested. This is why we're doing it in the loop.
									var bytesRead = await _reader.BaseStream.ReadAsync(response.BinaryChunk.Binary, chunkOffset, bytesToRead);
									bytesToRead -= bytesRead;
									chunkOffset += bytesRead;
								}
								break;
						}
					}

					// truncate the stringBuffer stream for the next line
					stringBuffer.SetLength(0L);
				}

				if (stringBuffer.Position > 0L)
				{
					// there's some string left in the buffer
					response.RawResponse.Add(encoding.GetString(stringBuffer.ToArray()));
				}

				stringBuffer.Close();

			}
			catch (Exception e)
			{
				// TODO: communicate the error clearly
				Console.WriteLine(e.Message);
			}

			return response;
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
