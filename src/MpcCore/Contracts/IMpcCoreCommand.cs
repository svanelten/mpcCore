namespace MpcCore.Contracts
{
	public interface IMpcCoreCommand<out T>
	{
		string Command { get; }
		T HandleResponse(IMpdResponse response);
	}
}
