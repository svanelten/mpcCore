using MpcCore.Contracts;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Test.Mock
{
	internal class MockResponse
	{
		public static IMpdResponse BasicOkResponse => Builder.New().AddBasicOkResponse().Create();

		public static IMpdResponse BasicErrorResponse => Builder.New().AddBasicErrorResponse().Create();

		public static ResponseBuilder Builder = new ResponseBuilder();
	}

	internal class ResponseBuilder
	{
		private MpdResponse _response = new MpdResponse();
		private string _result = Constants.Ok;

		public ResponseBuilder New()
		{
			_response = new MpdResponse();
			_result = Constants.Ok;
			return this;
		}

		public ResponseBuilder AddBasicErrorResponse()
		{
			_result = Constants.Ack;
			return this;
		}

		public ResponseBuilder AddBasicOkResponse()
		{
			_result = Constants.Ok;
			return this;
		}

		public ResponseBuilder AddErrorResponse(string errorLine)
		{
			_result = errorLine;
			return this;
		}

		public ResponseBuilder AddErrorResponse(string code, string line, string command, string message = "errormessage")
		{
			_result = $"ACK[{code}@{line}] {{{command}}} {message}";
			return this;
		}

		public ResponseBuilder Add(List<string> data)
		{
			_response.RawResponse.AddRange(data);
			return this;
		}

		public IMpdResponse Create()
		{
			_response.RawResponse.Add(_result);

			return _response;
		}
	}
}
