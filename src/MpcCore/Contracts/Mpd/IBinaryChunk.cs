namespace MpcCore.Contracts.Mpd
{
	public interface IBinaryChunk
	{
		byte[] Binary { get; set; }
		long FullLength { get; set; }
		int ChunkLength { get; set; }
		string MimeType { get; set; }
		int Offset { get; set; }
	}
}