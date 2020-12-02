# Development has been moved to [here](https://github.com/EpicTestingTempOrganizationForStuff/ExpressSharp)

# 🌐 ExpressSharp
<!-- ![GitHub Workflow Status](https://img.shields.io/github/workflow/status/EpicTestingTempOrganizationForStuff/dbrepo/.NET%20Core?style=for-the-badge) ![GitHub Workflow Status (branch)](https://img.shields.io/github/workflow/status/EpicTestingTempOrganizationForStuff/dbrepo/.NET%20Core/release?label=Release%20Build&style=for-the-badge) ![GitHub Workflow Status (branch)](https://img.shields.io/github/workflow/status/EpicTestingTempOrganizationForStuff/dbrepo/publish%20to%20nuget/release?color=004880&label=Nuget%20Publish&style=for-the-badge) -->
# 👷 Installation
## Get it from Nuget
[Latest](https://nuget.org)
## Download from releases
[Latest](https://github.com/EpicTestingTempOrganizationForStuff/ExpressSharp/releases)

# 🕴️ Usage

## GET
The code below shows how to initialise an Express server, how to set up a callback to a path and how to send data back to the user.
```cs
using ExpressSharp;
...
Express server = new Express();

server.GET("/helloworld", (req, res) =>
{
  res.Send("Hello, world!");
});

server.Listen(80);
```
If you visit your browser at http://localhost/helloworld it should display "Hello, world!", this means that it is working successfully.

## POST
TODO

## Middleware
Middleware is code that is executed before your callback is executed, for example I will have a middleware which will just write "Im middleware" to the console when the user accesses a page, however, this can be used for other things such as authentication headers, to avoid repeating code within each callback.

```cs
using ExpressSharp;
...
Express server = new Express();

server.Use((req, res, next) =>
{
  Console.WriteLine("Im middleware");
  next();
});

server.GET("/helloworld", (req, res) =>
{
  res.Send("Hello, world!");
});

server.Listen(80);
```
In this example you may notice the usage of `next()`, calling this function calls either the next middleware or the callback depending on whether it is the last middleware to be called, middleware is only called if the path exists as a binding.

# 🥅 Goals
* [ ] Usable

## ✨ Contributors

<table>
  <tr>
    <td align="center"><a href="https://mwareing.xyz/"><img src="https://avatars1.githubusercontent.com/u/29664925?s=460&v=4" width="100px;" alt=""/><br /><sub><b>TatoExp</b></sub></a><br /><a href="https://github.com/TatoExp/ExpressSharp/commits?author=TatoExp" title="Code">💻</a></td>
  </tr>
</table>
