namespace MpcCore.Contracts.Mpd
{
	public interface ISticker
	{
		string Path { get; set; }
		string Name { get; set; }
		string Type { get; set; }
		string Value { get; set; }
	}
}