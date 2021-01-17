using MpcCore.Contracts.Mpd;

namespace MpcCore.Contracts
{
	public interface IMpcCoreResponseStatus
	{
		string ErrorMessage { get; set; }
		bool HasError { get; set; }
		bool HasMpdError { get; }
		IMpdError MpdError { get; set; }
	}
}