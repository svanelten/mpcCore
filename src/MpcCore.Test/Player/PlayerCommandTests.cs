using Moq;
using MpcCore.Test.Mock;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace MpcCore.Test.Player
{
	[ExcludeFromCodeCoverage]
	public class PlayerCommandTests : DefaultTestsBase
	{
		[Theory]
		[InlineData(null, true)]
		[InlineData(1, true)]
		[InlineData(10, true)]
		[InlineData(400, false)]
		public async Task Play(int? index, bool isSuccess)
		{
			// play command always returns a positive result even if already playing.
			// Error case index 400 simulates out-of-range situation for queue index
			Connection.Setup(m => m.SendAsync<bool>(It.IsAny<Commands.Player.Play>()))
				.ReturnsAsync((Commands.Player.Play command) =>
				{
					return command.Command.EndsWith(" 400") 
						? MockResponse.BasicErrorResponse 
						: MockResponse.BasicOkResponse;
				});

			var result = await Client.SendAsync(new Commands.Player.Play(index));

			if (isSuccess)
			{
				Assert.False(result.Status.HasError);
				Assert.True(result.Result);
			}
			else
			{
				Assert.True(result.Status.HasError);
				Assert.False(result.Result);
			}
		}

		[Theory]
		[InlineData("", true)]
		[InlineData("1", true)]
		[InlineData("400", false)]
		public async Task PlayId(string id, bool isSuccess)
		{
			// playid command always returns true unless id does not exist. empty value assumes pos 0.
			// Error case id 400 simulates non-existant id.
			Connection.Setup(m => m.SendAsync<bool>(It.IsAny<Commands.Player.PlayId>()))
				.ReturnsAsync((Commands.Player.PlayId command) =>
				{
					return command.Command.EndsWith(" 400") 
						? MockResponse.Builder.New().AddErrorResponse("ACK [50@0] {playid} No such song").Create() 
						: MockResponse.BasicOkResponse;
				});

			var result = await Client.SendAsync(new Commands.Player.PlayId(id));

			if (isSuccess)
			{
				Assert.False(result.Status.HasError);
				Assert.True(result.Result);
			}
			else
			{
				Assert.True(result.Status.HasError);
				Assert.False(result.Result);
			}
		}

		[Fact]
		public async Task Stop()
		{
			// stop command always returns true
			Connection.Setup(m => m.SendAsync<bool>(It.IsAny<Commands.Player.Stop>()))
				.ReturnsAsync(MockResponse.BasicOkResponse);

			var result = await Client.SendAsync(new Commands.Player.Stop());

			Assert.False(result.Status.HasError);
			Assert.True(result.Result);
		}

		[Fact]
		public async Task Next()
		{
			// simulating skipping to next item in a queue with two items.
			// returns ok for the first skip, then the queue end is reached and an error occurs
			Connection.SetupSequence(m => m.SendAsync<bool>(It.IsAny<Commands.Player.Next>()))
				.ReturnsAsync(MockResponse.BasicOkResponse)
				.ReturnsAsync(MockResponse.Builder.New().AddErrorResponse("ACK [55@0] {next} Not playing").Create());

			var result = await Client.SendAsync(new Commands.Player.Next());

			Assert.False(result.Status.HasError);
			Assert.True(result.Result);

			var result2 = await Client.SendAsync(new Commands.Player.Next());

			Assert.True(result2.Status.HasError);
			Assert.False(result2.Result);
		}

		[Fact]
		public async Task Previous()
		{
			// simulating skipping to previous item in a queue with two items.
			// returns ok for the first skip, then the queue end is reached and an error occurs
			Connection.SetupSequence(m => m.SendAsync<bool>(It.IsAny<Commands.Player.Previous>()))
				.ReturnsAsync(MockResponse.BasicOkResponse)
				.ReturnsAsync(MockResponse.Builder.New().AddErrorResponse("ACK [55@0] {previous} Not playing").Create());

			var result = await Client.SendAsync(new Commands.Player.Previous());

			Assert.False(result.Status.HasError);
			Assert.True(result.Result);

			var result2 = await Client.SendAsync(new Commands.Player.Previous());

			Assert.True(result2.Status.HasError);
			Assert.False(result2.Result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData(true)]
		[InlineData(false)]
		public async Task Pause(bool? state)
		{
			// pause command should always return true
			Connection.Setup(m => m.SendAsync<bool>(It.IsAny<Commands.Player.Pause>()))
				.ReturnsAsync(MockResponse.BasicOkResponse);

			var result = await Client.SendAsync(new Commands.Player.Pause(state));

			Assert.False(result.Status.HasError);
			Assert.True(result.Result);
		}

		[Theory]
		[InlineData(10)]
		[InlineData(-10)]
		[InlineData(100000)]
		public async Task SeekInCurrent(int seconds)
		{
			// simulating seeking x seconds ahead or back in a playing song.
			// always returns true unless the player is not playing.
			Connection.Setup(m => m.SendAsync<bool>(It.IsAny<Commands.Player.Seek>()))
				.ReturnsAsync(MockResponse.BasicOkResponse);

			var result = await Client.SendAsync(new Commands.Player.Seek(seconds));

			Assert.False(result.Status.HasError);
			Assert.True(result.Result);
		}

		[Fact]
		public async Task SeekInCurrentWhileNotPlaying()
		{
			// simulating seeking x seconds while the player is not playing.
			Connection.Setup(m => m.SendAsync<bool>(It.IsAny<Commands.Player.Seek>()))
				.ReturnsAsync(MockResponse.Builder.New().AddErrorResponse("ACK [55@0] {seekcur} Not playing").Create());

			var result = await Client.SendAsync(new Commands.Player.Seek(99));

			Assert.True(result.Status.HasError);
			Assert.False(result.Result);
		}

		[Theory]
		[InlineData("1", 10d, true)]
		[InlineData("1", -10d, false)]
		[InlineData("400", 10d, false)]
		[InlineData("", 10d, false)]
		[InlineData("1", null, false)]
		public async Task SeekInId(string id, double? seconds, bool isSuccess)
		{
			// simulates seeking in a song identified by id which exists, 
			// negative seek values which are not allowed
			// an id (400) that does not exist
			// and missing parameter id as well as seconds
			Connection.Setup(m => m.SendAsync<bool>(It.IsAny<Commands.Player.SeekInSongId>()))
				.ReturnsAsync((Commands.Player.SeekInSongId command) =>
				{
					if (!seconds.HasValue || string.IsNullOrEmpty(id))
					{
						return MockResponse.Builder.New().AddErrorResponse("ACK [2@0] {seekid} wrong number of arguments for \"seekid\"").Create();
					}

					if (id == "400")
					{ 
						return MockResponse.Builder.New().AddErrorResponse("ACK [50@0] {seekid} No such song").Create();
					}

					if (seconds.HasValue && seconds.Value < 0)
					{
						return MockResponse.Builder.New().AddErrorResponse($"ACK [2@0] {{seekid}} Negative value not allowed: {seconds.Value}").Create();
					}

					return MockResponse.BasicOkResponse;
				});

			var result = await Client.SendAsync(new Commands.Player.SeekInSongId(id, seconds.GetValueOrDefault()));

			if (isSuccess)
			{
				Assert.False(result.Status.HasError);
				Assert.True(result.Result);
			}
			else
			{
				Assert.True(result.Status.HasError);
				Assert.False(result.Result);
			}
		}

		[Theory]
		[InlineData(1, 10d, true)]
		[InlineData(1, -10d, false)]
		[InlineData(400, 10d, false)]
		[InlineData(null, 10d, false)]
		[InlineData(1, null, false)]
		public async Task SeekInPosition(int? position, double? seconds, bool isSuccess)
		{
			// simulates seeking in a song with a position which exists, 
			// negative seek values which are not allowed
			// a position (400) that does not exist
			// and missing parameter position as well as seconds
			Connection.Setup(m => m.SendAsync<bool>(It.IsAny<Commands.Player.SeekInSongOnPosition>()))
				.ReturnsAsync((Commands.Player.SeekInSongOnPosition command) =>
				{
					if (!seconds.HasValue || !position.HasValue)
					{
						return MockResponse.Builder.New().AddErrorResponse("ACK [2@0] {seek} wrong number of arguments for \"seek\"").Create();
					}
				
					if (position.HasValue && position.Value == 400)
					{
						return MockResponse.Builder.New().AddErrorResponse("ACK [2@0] {seek} Bad song index").Create();
					}

					if (seconds.HasValue && seconds.Value < 0)
					{
						return MockResponse.Builder.New().AddErrorResponse($"ACK [2@0] {{seek}} Negative value not allowed: {seconds.Value}").Create();
					}

					return MockResponse.BasicOkResponse;
				});

			var result = await Client.SendAsync(new Commands.Player.SeekInSongOnPosition(position.GetValueOrDefault(), seconds.GetValueOrDefault()));

			if (isSuccess)
			{
				Assert.False(result.Status.HasError);
				Assert.True(result.Result);
			}
			else
			{
				Assert.True(result.Status.HasError);
				Assert.False(result.Result);
			}
		}
	}
}
