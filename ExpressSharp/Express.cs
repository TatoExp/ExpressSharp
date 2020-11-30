using System;
using System.Net;
using ExpressSharp.Exceptions;
using System.Collections.Generic;
namespace ExpressSharp
{
	public class Express
	{
		private readonly HttpListener _server;
		private readonly List<Action<Request, Response, Action>> _actions; //Middleware that is specified via use
		private readonly Dictionary<string, Action<Request, Response>> _bindings;

		private string _baseUrl; //Base URL is required, this will be a wildcard so that we can dynamically set port when Listen is called.
		private ushort _port = 80; //UInt16 as that is the largest a port can be
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

		public void Listen(ushort? port = null)
		{
			if (port != null)
			{
				if (port == 0)
					throw new InvalidPortException();
				_port = port.GetValueOrDefault();
			}
			_server.Start();
			HttpListenerContext context = _server.GetContext();
		}

		public void Bind(string method, string path, Action<Request, Response> callback) => _bindings.Add($"{method} {path}", callback);
		public void GET(string path, Action<Request, Response> callback) => this.Bind("GET", path, callback);
		public void POST(string path, Action<Request, Response> callback) => this.Bind("POST", path, callback);
		public void PUT(string path, Action<Request, Response> callback) => this.Bind("PUT", path, callback);
		public void DELETE(string path, Action<Request, Response> callback) => this.Bind("DELETE", path, callback);
		public void PATCH(string path, Action<Request, Response> callback) => this.Bind("PATCH", path, callback);
	}
}
