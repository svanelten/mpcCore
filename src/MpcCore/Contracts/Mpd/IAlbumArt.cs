namespace MpcCore.Contracts.Mpd
{
	public interface IAlbumArt
	{
		byte[] Bytes { get; set; }
		string MpdItemUri { get; set; }
		int Offset { get; set; }
		long Size { get; set; }
	}
}