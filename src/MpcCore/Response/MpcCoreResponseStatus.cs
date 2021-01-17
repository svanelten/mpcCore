using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;

namespace MpcCore.Response
{
	public class MpcCoreResponseStatus : IMpcCoreResponseStatus
	{
		public bool HasError { get; set; }
		public bool HasMpdError => MpdError != null;
		public string ErrorMessage { get; set; }
		public IMpdError MpdError { get; set; }
	}
}
