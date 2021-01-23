using MpcCore.Contracts.Mpd;
using System.Collections.Generic;

namespace MpcCore.Contracts
{
	public interface IMpdResponse
	{
		IBinaryChunk BinaryChunk { get; set; }

		bool IsNullOrEmpty { get; }

		bool IsOkResponse { get; }

		bool HasContent { get; }

		public bool IsErrorResponse { get; }
		
		bool HasBinaryChunk { get; }
		
		List<string> RawResponse { get; set; }

		List<string> GetContent();
	}
}