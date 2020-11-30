using System;
using System.Collections.Generic;
using System.Diagnostics;
using ExpressSharp;
namespace ExampleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Express server = new Express();

			server.Use((Request req, Response res, Action next) =>
			{
				Console.WriteLine("middleware");
				next();
			});

			server.GET("/helloworld", (req, res) =>
			{
				res.send("Hello world!");
			});

			server.Listen();
		}
	}
}
