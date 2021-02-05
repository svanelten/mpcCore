using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace MpcCore.Test.CommandTests
{
	[ExcludeFromCodeCoverage]
	public class PlayerCommandGeneration
	{
		[Theory]
		[InlineData(null, "play ")]
		[InlineData(1, "play 1")]
		[InlineData(123123, "play 123123")]
		public void Play(int? index, string expectedCommand)
		{
			var result = new Commands.Player.Play(index);
			
			Assert.Equal(expectedCommand, result.Command);
		}

		[Theory]
		[InlineData(null, "playid ")]
		[InlineData("1", "playid 1")]
		[InlineData("123123", "playid 123123")]
		public void PlayId(string id, string expectedCommand)
		{ 
			var result = new Commands.Player.PlayId(id);

			Assert.Equal(expectedCommand, result.Command);
		}

		[Fact]
		public void Stop()
		{
			var result = new Commands.Player.Stop();

			Assert.Equal("stop", result.Command);
		}

		[Fact]
		public void Next()
		{
			var result = new Commands.Player.Next();

			Assert.Equal("next", result.Command);
		}

		[Fact]
		public void Previous()
		{
			var result = new Commands.Player.Previous();

			Assert.Equal("previous", result.Command);
		}

		[Theory]
		[InlineData(null, "pause ")]
		[InlineData(true, "pause 1")]
		[InlineData(false, "pause 0")]
		public void Pause(bool? state, string expectedCommand)
		{
			var result = new Commands.Player.Pause(state);

			Assert.Equal(expectedCommand, result.Command);
		}

		[Theory]
		[InlineData(10, "seekcur 10")]
		[InlineData(-10, "seekcur -10")]
		[InlineData(1.2d, "seekcur 1.2")]
		[InlineData(1.45646d, "seekcur 1.46")]
		[InlineData(1.4436d, "seekcur 1.44")]
		[InlineData(null, "seekcur 0")]
		public void SeekInCurrent(double? seconds, string expectedCommand)
		{
			var result = new Commands.Player.Seek(seconds.GetValueOrDefault());

			Assert.Equal(expectedCommand, result.Command);
		}

		[Theory]
		[InlineData("1", 10d, "seekid 1 10")]
		[InlineData("1", 1.30d, "seekid 1 1.3")]
		[InlineData("1", null, "seekid 1 0")]
		public void SeekInId(string id, double? seconds, string expectedCommand)
		{
			var result = new Commands.Player.SeekInSongId(id, seconds.GetValueOrDefault());

			Assert.Equal(expectedCommand, result.Command);
		}

		[Theory]
		[InlineData(1, 10d, "seek 1 10")]
		[InlineData(1, 1.30d, "seek 1 1.3")]
		[InlineData(1, null, "seek 1 0")]
		public void SeekInPosition(int? position, double? seconds, string expectedCommand)
		{
			var result = new Commands.Player.SeekInSongOnPosition(position.GetValueOrDefault(), seconds.GetValueOrDefault());

			Assert.Equal(expectedCommand, result.Command);
		}
	}
}
