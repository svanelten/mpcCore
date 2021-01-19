using MpcCore.Contracts.Mpd;

namespace MpcCore.Mpd
{
	public class AlbumArt : IAlbumArt
	{
		/// <summary>
		/// Path of the mpd item
		/// </summary>
		public string MpdItemPath { get; internal set; }

		/// <summary>
		/// Total filesize
		/// </summary>
		public long Size { get; internal set; }

		/// <summary>
		/// Offset where reading started
		/// </summary>
		public int Offset { get; internal set; }

		/// <summary>
		/// Bytearray of the binary response
		/// </summary>
		public byte[] Bytes { get; internal set; }

		/// <summary>
		/// Mimetype of the image. Not always set.
		/// </summary>
		public string MimeType { get; internal set; }
	}
}
