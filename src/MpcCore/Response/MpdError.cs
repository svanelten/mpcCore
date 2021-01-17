using MpcCore.Contracts.Mpd;

namespace MpcCore.Response
{
	public class MpdError : IMpdError
	{
		public string Code { get; set; }
		public string Line { get; set; }
		public string Message { get; set; }
		public string Command { get; set; }
		public string RawError { get; set; }
	}
}
