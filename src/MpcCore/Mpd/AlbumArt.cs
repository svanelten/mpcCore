using MpcCore.Contracts.Mpd;

namespace MpcCore.Mpd
{
	public class AlbumArt : IAlbumArt
	{
		public string MpdItemUri { get; set; }
		public long Size { get; set; }
		public int Offset { get; set; }

		public byte[] Bytes { get; set; }
	}
}
