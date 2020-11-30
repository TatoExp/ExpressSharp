using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressSharp.Middleware
{
	internal class MiddlewareHandler
	{

		private readonly List<Action<Request, Response, Action>> _actions;
		private readonly Request _req;
		private readonly Response _res;
		private readonly Action<Request, Response> _callback;


		private int middlewarePos = 0;


		public MiddlewareHandler(Express express,
			Request req,
			Response res,
			Action<Request, Response> callback,  
			List<Action<Request, Response, Action>> actions)
		{
			_callback = callback;
			_req = req;
			_res = res;
			_actions = actions;
			if (_actions.Count >= 1)
				actions[0].Invoke(req, res, next);
			else
				callback(req, res);
		}

		private void next()
		{
			middlewarePos++;
			if (middlewarePos == _actions.Count)
			{
				_callback(_req, _res);
				return;
			}
			_actions[middlewarePos](_req, _res, next);
		}
	}
}
