using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using System.Collections.Generic;
using System.Linq;

namespace MpcCore.Response
{
	public class MpdResponse : IMpdResponse
	{
		/// <summary>
		/// backing field for the raw response
		/// </summary>
		private List<string> _rawResponse;

		/// <summary>
		/// Checks if the response has any content at all
		/// </summary>
		public bool IsNullOrEmpty => RawResponse.Count < 1;

		/// <summary>
		/// Checks if the response was successfull
		/// </summary>
		public bool IsOkResponse => (RawResponse.Count >= 1 && RawResponse.First() == Constants.Ok);

		/// <summary>
		/// Checks if the response has contents other than the status
		/// </summary>
		public bool HasContent => RawResponse.Count > 1;

		/// <summary>
		/// Checks if the response contains an error
		/// </summary>
		public bool IsErrorResponse => RawResponse.Last().StartsWith(Constants.Ack);

		/// <summary>
		/// Checks if the response contains a binary result
		/// </summary>
		public bool HasBinaryChunk => BinaryChunk != null;

		/// <summary>
		/// Binary result, eg for a partial image 
		/// </summary>
		public IBinaryChunk BinaryChunk { get; set; }

		/// <summary>
		/// Returns the content of the response without status or empty lines
		/// </summary>
		/// <returns></returns>
		public List<string> GetContent()
		{
			return RawResponse.Take(RawResponse.Count() - 1).Where(s => !string.IsNullOrEmpty(s)).ToList();
		}

		/// <summary>
		/// The raw response except for binary content
		/// </summary>
		public List<string> RawResponse
		{
			get => _rawResponse ??= new List<string>();
			set => _rawResponse = value;
		}
	}
}
