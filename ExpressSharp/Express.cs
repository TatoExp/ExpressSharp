using System;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using ExpressSharp.Middleware;
using ExpressSharp.Exceptions;

namespace ExpressSharp
{
	public class Express
	{
		private readonly HttpListener _server;
		private readonly List<Action<Request, Response, Action>> _actions; //Middleware that is specified via use
		private readonly Dictionary<string, Action<Request, Response>> _bindings;

		private string _baseUrl = "http://*:{0}/"; //Base URL is required, this will be a wildcard so that we can dynamically set port when Listen is called.
		private string _baseHttpsUrl = "https://*:{0}/"; //Same as base URL but for https
		private ushort _port = 3000; //UInt16 as that is the largest a port can be
		private bool _listening = false; //Set to false, HttpListener uses GetContext so we will have to loop and create new threads per request.

		public Express()
		{
			if (!HttpListener.IsSupported)
				throw new UnsupportedOperatingSystemException();
			_server = new HttpListener();
			_actions = new List<Action<Request, Response, Action>>();
			_bindings = new Dictionary<string, Action<Request, Response>>();
		}
		public void Use(Action<Request, Response, Action> method) => _actions.Add(method);

		private void AcceptRequest(HttpListenerContext context)
		{
			HttpListenerResponse res = context.Response;
			HttpListenerRequest req = context.Request;
			Action<Request, Response> callback;

			if (!_bindings.TryGetValue($"{req.HttpMethod} {req.RawUrl}", out callback)) //Callback doesnt exist
			{
				res.StatusCode = 404;
				byte[] buffer = Encoding.UTF8.GetBytes($"Cannot {req.HttpMethod} {req.RawUrl}");
				res.ContentLength64 = buffer.Length;
				res.OutputStream.Write(buffer);
				return;
			}

			Request actualReq = new Request(req);
			Response actualResponse = new Response(res);

			MiddlewareHandler middleware = new MiddlewareHandler(this, actualReq, actualResponse, callback, _actions);
		}

		public void Listen(ushort? port = null)
		{
			if (port != null)
			{
				if (port == 0)
					throw new InvalidPortException();
				_port = port.GetValueOrDefault();
			}

			_server.Prefixes.Clear();
			_server.Prefixes.Add(string.Format(_baseUrl, _port));

			_server.Start();
			_listening = true;
			while (_listening)
			{
				HttpListenerContext context = _server.GetContext();
				new Thread(() =>
				{
					AcceptRequest(context);
				}).Start();
			}
		}

		public void Bind(string method, string path, Action<Request, Response> callback) => _bindings.Add($"{method} {path}", callback);
		public void GET(string path, Action<Request, Response> callback) => this.Bind("GET", path, callback);
		public void POST(string path, Action<Request, Response> callback) => this.Bind("POST", path, callback);
		public void PUT(string path, Action<Request, Response> callback) => this.Bind("PUT", path, callback);
		public void DELETE(string path, Action<Request, Response> callback) => this.Bind("DELETE", path, callback);
		public void PATCH(string path, Action<Request, Response> callback) => this.Bind("PATCH", path, callback);
	}
}
