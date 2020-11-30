using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace ExpressSharp
{
	public class Response
	{
		private readonly HttpListenerResponse _response;

		public Response(HttpListenerResponse response)
		{
			_response = response;
		}

		public Response Status(int code)
		{
			_response.StatusCode = code;
			return this;
		}

		public Response Send(string data)
		{
			byte[] buffer = Encoding.UTF8.GetBytes(data);
			_response.ContentLength64 = buffer.Length;
			_response.OutputStream.Write(buffer);
			return this;
		}

		public Response Json<T>(T obj)
		{
			byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
			_response.ContentLength64 = buffer.Length;
			_response.OutputStream.Write(buffer);
			return this;
		}

		public Response Set(string key, string value) {
			_response.AddHeader(key, value);
			return this;
		}
	}
}
