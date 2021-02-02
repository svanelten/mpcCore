using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace MpcCore.Test.Mock
{
	[ExcludeFromCodeCoverage]
	public class DefaultTestsBase
	{
		protected MpcCoreClient Client;
		protected Mock<IMpcCoreConnection> Connection;

		public DefaultTestsBase()
		{
			Connection = new Mock<IMpcCoreConnection>();
			Connection
				.Setup(p => p.ConnectAsync())
				.Returns(Task.CompletedTask);
			Connection
				.Setup(p => p.DisconnectAsync())
				.Returns(Task.CompletedTask);
			Connection
				.Setup(p => p.IsConnected)
				.Returns(true);

			Client = new MpcCoreClient(Connection.Object);
		}
	}
}
