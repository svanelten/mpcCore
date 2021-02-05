using MpcCore.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace MpcCore.Response
{
	public class MpcCoreResponse<T> : IMpcCoreResponse<T>
	{
		private IMpdResponse _response;

		public IMpcCoreCommand<T> Command { get; internal set; }

		public IMpcCoreResponseStatus Status { get; internal set; } = new MpcCoreResponseStatus();

		public T Result { get; internal set; }

		public MpcCoreResponse(IMpcCoreCommand<T> command, IMpdResponse response)
		{
			_response = response;
			Command = command;
		}

		public MpcCoreResponse(IMpcCoreCommand<T> command, IMpcCoreResponseStatus status)
		{
			Command = command;
			Status = status;
		}

		#pragma warning disable CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
		public async Task<IMpcCoreResponse<T>> CreateResult()
		{
			if (_response == null || _response.IsNullOrEmpty)
			{
				Status.ErrorMessage = "response is empty";
				Status.HasError = true;

				return this;
			}

			if (_response.IsErrorResponse)
			{
				Status = new MpcCoreResponseStatus
				{
					HasError = true,
					ErrorMessage = "mpd returned an error",
					MpdError = ResponseParser.ParseMpdError(Command.Command, _response.RawResponse.Last())
				};
			}

			Result = Command.HandleResponse(_response);

			return this;
		}
		#pragma warning restore CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
	}
}
