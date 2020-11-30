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

			server.Use((req, res, next) =>
			{
				Console.WriteLine("Im middleware");
				next();
			});

			server.Use((req, res, next) =>
			{
				Console.WriteLine("Middleware 2");
				next();
			});

			server.GET("/helloworld", (req, res) =>
			{
				res.Send("Hello world!");
			});

			server.POST("/post", (req, res) =>
			{
				res.Send("oof");
			});

			server.GET("/", (req, res) =>
			{
				res.Send("oof");
			});

			server.Listen();
		}
	}
}
