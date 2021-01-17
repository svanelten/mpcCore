using System.Threading.Tasks;

namespace MpcCore.Contracts
{
	public interface IMpcCoreResponse<T>
	{ 
		IMpcCoreCommand<T> Command { get; }
		IMpcCoreResponseStatus Status { get; }
		T Result { get; }

		Task<IMpcCoreResponse<T>> CreateResult();
	}
}
