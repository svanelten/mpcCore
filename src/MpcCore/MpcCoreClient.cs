using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Extensions;
using MpcCore.Mpd;
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

		public async Task<IMpcCoreResponse<T>> SendAsync<T>(IMpcCoreCommand<T> command)
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

		/// <summary>
		/// Loads the embedded album art from the item with the given path.
		/// MPD usually implements this by reading embedded pictures from binary tags (e.g. ID3v2’s APIC tag).
		/// Uses <see cref="Commands.Database.ReadPicture"/> recursively under the hood to get the full image and returns the image content as a byte array.
		/// Returns null if the file can't be found or contains no image.
		/// </summary>
		/// <param name="path">path to an item</param>
		/// <returns>AlbumArt with byte[] image or null</returns>
		public async Task<IAlbumArt> LoadEmbeddedAlbumArt(string path)
		{
			var completed = false;
			var offset = 0;
			var array = new byte[0];
			IAlbumArt art = null;

			while (!completed)
			{
				var result = await SendAsync(new Commands.Database.ReadPicture(path, offset));

				if (result.Result != null && !result.Status.HasError)
				{
					if (result.Result.ChunkLength == 0)
					{
						completed = true;
						art = new AlbumArt
						{
							Bytes = array,
							ItemPath = path,
							MimeType = result.Result.MimeType
						};
					}
					else
					{
						offset += result.Result.ChunkLength;
						array = ByteExtensions.Combine(array, result.Result.Binary);
					}
				}
				else
				{
					completed = true;
				}
			}

			return art.HasContent ? art : null;
		}

		/// <summary>
		/// Loads the album art for the given path.
		/// MPD currently implemented this by searching the directory the file resides in for a file called cover.png, cover.jpg, cover.tiff or cover.bmp.
		/// Uses <see cref="Commands.Database.GetAlbumArt"/> recursively under the hood to get the full image and returns the image content as a byte array.
		/// Returns null if no image exists.
		/// </summary>
		/// <param name="path">path to an item</param>
		/// <returns>AlbumArt with byte[] image or null</returns>
		public async Task<IAlbumArt> LoadAlbumArt(string path)
		{
			var completed = false;
			var offset = 0;
			var array = new byte[0];
			IAlbumArt art = null;

			while (!completed)
			{
				var result = await SendAsync(new Commands.Database.GetAlbumArt(path, offset));

				if (result.Result != null && !result.Status.HasError)
				{
					if (result.Result.ChunkLength == 0)
					{
						completed = true;
						art = new AlbumArt
						{
							Bytes = array,
							ItemPath = path,
							MimeType = result.Result.MimeType
						};
					}
					else
					{
						offset += result.Result.ChunkLength;
						array = ByteExtensions.Combine(array, result.Result.Binary);
					}
				}
				else
				{
					completed = true;
				}
			}

			return art;
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
