using MpcCore.Contracts.Mpd;

namespace MpcCore.Mpd
{
	public class BinaryChunk : IBinaryChunk
	{
		public byte[] Binary { get; set; }

		public long FullLength { get; set; }

		public int Offset { get; set; }

		public string MimeType { get; set; }

		public int ChunkLength { get; set; }
	}
}
