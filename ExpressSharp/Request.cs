using System;
using System.Net;
namespace ExpressSharp
{
	public class Request
	{
		private readonly HttpListenerRequest _request;
		public Request(HttpListenerRequest request)
		{
			_request = request;
		}

		public string Header(string header) => _request.Headers.Get(header);

		public string Param(string param)
		{
			//TODO
			return "";
		}
	}
}
