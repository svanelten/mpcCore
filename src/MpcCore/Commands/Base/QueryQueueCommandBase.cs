using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;

namespace MpcCore.Commands.Base
{
	/// <summary>
	/// Queue commands that return a queue object.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public abstract class QueryQueueCommandBase : IMpcCoreCommand<IQueue>
	{
		public string Command { get; internal set; }

		public virtual IQueue HandleResponse(IMpdResponse response)
		{
			if (!response.HasContent)
			{
				return null;
			}

			var parser = new ResponseParser(response);

			return new Mpd.Queue
			{
				Items = parser.GetListedTracks()
			};

		}
	}
}
