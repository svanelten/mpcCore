using MpcCore.Test.Mock;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MpcCore.Test.Client
{
	[ExcludeFromCodeCoverage]
	public class ClientTests : DefaultTestsBase
	{
		[Fact]
		public async Task CreateConnection()
		{
			var connection = GetConnectionMock();
			connection
				.Setup(p => p.IsConnected)
				.Returns(false);

			var client = new MpcCoreClient(connection.Object);

			Assert.False(client.IsConnected);

			connection
				.Setup(p => p.IsConnected)
				.Returns(true);

			await client.ConnectAsync();

			Assert.True(client.IsConnected);
		}

		[Fact]
		public async Task CreateConnectionFailure()
		{
			var connection = GetConnectionMock();
			connection
				.Setup(p => p.IsConnected)
				.Returns(false);

			var client = new MpcCoreClient(connection.Object);

			Assert.False(client.IsConnected);

			await client.ConnectAsync();

			Assert.False(client.IsConnected);
		}

		[Fact]
		public async Task UnexpectedConnectionResponse()
		{
			// todo amend test when connection state object is done
			var connection = GetConnectionMock();
			connection
				.Setup(p => p.ConnectAsync())
				.Throws(new InvalidDataException($"Unexpected response from MPD, does not start with '{Constants.FirstLinePrefix}."));

			var client = new MpcCoreClient(connection.Object);

			await Assert.ThrowsAsync<InvalidDataException>(async ()=> await client.ConnectAsync());
		}

		[Fact]
		public async Task ResponseIsNullHandling()
		{
			var connection = GetConnectionMock();
			connection
				.Setup(p => p.IsConnected)
				.Returns(false);

			var client = new MpcCoreClient(connection.Object);
			var result = await client.SendAsync(new Commands.Player.Play());

			Assert.False(result.Result);
			Assert.True(result.Status.HasError);
			Assert.False(result.Status.HasMpdError);
			Assert.Null(result.Status.MpdError);
			Assert.False(string.IsNullOrEmpty(result.Status.ErrorMessage));
		}

		[Fact]
		public async Task ResponseIsEmptyHandling()
		{
			var connection = GetConnectionMock();
			connection
				.Setup(p => p.IsConnected)
				.Returns(false);

			var client = new MpcCoreClient(connection.Object);
			var result = await client.SendAsync(new Commands.Player.Play());

			Assert.False(result.Result);
			Assert.True(result.Status.HasError);
			Assert.False(result.Status.HasMpdError);
			Assert.Null(result.Status.MpdError);
			Assert.False(string.IsNullOrEmpty(result.Status.ErrorMessage));
		}
	}
}
