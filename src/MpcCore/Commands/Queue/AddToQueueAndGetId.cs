using MpcCore.Contracts;
using MpcCore.Extensions;
using MpcCore.Response;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Adds an item to the queue and returns the item id
	/// If a position is given, the item is added at this position in the queue
	/// </summary>
	public class AddToQueueAndGetId : IMpcCoreCommand<int>
	{
		public string Command { get; internal set; }

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">path of item</param>
		/// <param name="position">optional position in the queue</param>
		public AddToQueueAndGetId(string path, int? position = null)
		{
			Command = $"addid \"{path}\" {position.GetParamString()}";
		}


		public int HandleResponse(IMpdResponse response)
		{
			if (response.IsErrorResponse)
			{
				return 0;
			}

			var parser = new ResponseParser(response);

			return parser.GetItemId();
		}
	}
}
