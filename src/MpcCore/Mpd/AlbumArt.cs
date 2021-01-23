using MpcCore.Contracts.Mpd;
using System.IO;
using System.Threading.Tasks;

namespace MpcCore.Mpd
{
	public class AlbumArt : IAlbumArt
	{
		/// <summary>
		/// Checks if any bytes were read.
		/// No image found if not.
		/// </summary>
		public bool HasContent => Bytes?.Length > 0;

		/// <summary>
		/// Path of the mpd item
		/// </summary>
		public string ItemPath { get; internal set; }

		/// <summary>
		/// Bytearray of the binary response
		/// </summary>
		public byte[] Bytes { get; internal set; }

		/// <summary>
		/// Mimetype of the image. Not always set.
		/// </summary>
		public string MimeType { get; internal set; }

		/// <summary>
		/// Tries to save the file to the given path
		/// </summary>
		/// <param name="path">full path plus filename</param>
		/// <returns>Task</returns>
		public async Task SaveAsFile(string path)
		{
			await File.WriteAllBytesAsync(path, Bytes);
		}
	}
}
