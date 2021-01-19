namespace MpcCore.Contracts.Mpd
{
	public interface IAlbumArt
	{
		byte[] Bytes { get; }
		string MpdItemPath { get; }
		int Offset { get; }
		long Size { get; }
		string MimeType { get; }
	}
}