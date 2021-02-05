namespace MpcCore.Contracts.Mpd
{
	public interface IMpdError
	{
		string Code { get; set; }
		string Command { get; set; }
		string Line { get; set; }
		string Message { get; set; }
		public string RawError { get; set; }
	}
}