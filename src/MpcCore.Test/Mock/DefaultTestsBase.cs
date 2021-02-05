using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace MpcCore.Test.Mock
{
	[ExcludeFromCodeCoverage]
	public class DefaultTestsBase
	{
		public Mock<IMpcCoreConnection> GetConnectionMock()
		{
			var connection = new Mock<IMpcCoreConnection>(MockBehavior.Strict);
			connection
				.Setup(p => p.ConnectAsync())
				.Returns(Task.CompletedTask);
			connection
				.Setup(p => p.DisconnectAsync())
				.Returns(Task.CompletedTask);
			connection
				.Setup(p => p.IsConnected)
				.Returns(true);

			return connection;
		}
	}
}
