### Whats new in ASP.Net Core 6

---

### Minimal Api

---

https://docs.microsoft.com/en-us/aspnet/core/migration/50-to-60-samples?view=aspnetcore-6.0

```C#
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
```



### Razor compiler updated to use source generators

---

- The Razor compiler is now based on [C# source generators](https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview)
- Source generators:  Run during compilation and inspect what is being compiled to produce additional files that are compiled along with the rest of the project
- Using source generators simplifies the Razor compiler and significantly speeds up build times.



### IAsyncDisposable

---

- IAsyncDisposable is now available for controllers, Razor Pages, and View Components.
- `IAsyncDisposable` is beneficial when working with:
  - Asynchronous enumerators, for example, in asynchronous streams.
  - Unmanaged resources that have resource-intensive I/O operations to release.
- 

```C#
public class HomeController : Controller, IAsyncDisposable
{
    private Utf8JsonWriter? _jsonWriter;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _jsonWriter = new Utf8JsonWriter(new MemoryStream());
    }
    public async ValueTask DisposeAsync()
    {
    if (_jsonWriter is not null)
    {
        await _jsonWriter.DisposeAsync();
    }

    _jsonWriter = null;
}
```

### HTTP logging middleware

---

- HTTP logging is a new built-in middleware that logs information about HTTP requests and HTTP responses including the headers and entire body:

  

```C#
//appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware": "Information"
    }
  }
}

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.UseHttpLogging();

app.MapGet("/", () => "Hello World!");

app.Run();


//output

info: Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware[1]
      Request:
      Protocol: HTTP/2
      Method: GET
      Scheme: https
      PathBase: 
      Path: /
      Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9
      Accept-Encoding: gzip, deflate, br
      Accept-Language: en-US,en;q=0.9
      Cache-Control: max-age=0
      Connection: close
      Cookie: [Redacted]
      Host: localhost:44372
      User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36 Edg/95.0.1020.30
      sec-ch-ua: [Redacted]
      sec-ch-ua-mobile: [Redacted]
      sec-ch-ua-platform: [Redacted]
      upgrade-insecure-requests: [Redacted]
      sec-fetch-site: [Redacted]
      sec-fetch-mode: [Redacted]
      sec-fetch-user: [Redacted]
      sec-fetch-dest: [Redacted]
info: Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware[2]
      Response:
      StatusCode: 200
      Content-Type: text/plain; charset=utf-8

```



###  Async streaming

---

- ASP.NET Core now supports asynchronous streaming from controller actions and responses from the JSON formatter
-  Return an `IAsyncEnumerable` from an action method

### Developer exception page Middleware added automatically

---

- In the development environment the DeveloperExceptionPageMiddleware  is added by default

  

```
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
}
```



### Quick get and set for HTTP headers

---

New APIs were added to expose all common headers available on [Microsoft.Net.Http.Headers.HeaderNames](https://docs.microsoft.com/en-us/dotnet/api/microsoft.net.http.headers.headernames) as properties on the [IHeaderDictionary](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iheaderdictionary) resulting in an easier to use API

```C#
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Use(async (context, next) =>
{
    var hostHeader = context.Request.Headers.Host;
    app.Logger.LogInformation("Host header: {host}", hostHeader);
    context.Response.Headers.XPoweredBy = "ASP.NET Core 6.0";
    await next.Invoke(context);
    var dateHeader = context.Response.Headers.Date;
    app.Logger.LogInformation("Response date: {date}", dateHeader);
});

app.Run();
```



### View Components Tag Helpers

---

```
class MyViewComponent
{
    IViewComponentResult Invoke(bool showSomething = false) { ... }
}

With ASP.NET Core 6, the tag helper can be invoked without having to specify a value for the showSomething parameter:

<vc:my />
```



###  Random generated ports for Kestrel

---

- Random ports help minimize a port conflict when multiple projects are run on the same machine.
- When a project is created, a random HTTP port between 5000-5300 and a random HTTPS port between 7000-7300 is specified in the generated `Properties/launchSettings.json` file. 
- If no port is specified, Kestrel defaults to the HTTP 5000 and HTTPS 5001 ports

### Nullable Reference Type Annotations

---

- Projects that have opted in to using nullable annotations may see new build-time warnings from ASP.NET Core APIs.

- By utilizing the new Nullable feature, ASP.NET Core can provide additional compile-time safety in the handling of reference types

  

```C#
<PropertyGroup>
    <Nullable>enable</Nullable>
</PropertyGroup>
```



###  Draft HTTP/3 support in .NET 6

---

- https://datatracker.ietf.org/doc/html/draft-ietf-quic-http-34

###  Timestamps and PID support for enhanced Logging

---

- The [ASP.NET Core Module (ANCM) for IIS](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/aspnet-core-module?view=aspnetcore-6.0) (ANCM) enhanced diagnostic logs include timestamps and PID of the process emitting the logs
- Logging timestamps and PID makes it easier to diagnose issues with overlapping process restarts in IIS when multiple IIS worker processes are running.

```reStructuredText
[2021-07-28T19:23:44.076Z, PID: 11020] [aspnetcorev2.dll] Initializing logs for 'C:\<path>\aspnetcorev2.dll'. Process Id: 11020. File Version: 16.0.21209.0. Description: IIS ASP.NET Core Module V2. Commit: 96475a2acdf50d7599ba8e96583fa73efbe27912.
[2021-07-28T19:23:44.079Z, PID: 11020] [aspnetcorev2.dll] Resolving hostfxr parameters for application: '.\InProcessWebSite.exe' arguments: '' path: 'C:\Temp\e86ac4e9ced24bb6bacf1a9415e70753\'
[2021-07-28T19:23:44.080Z, PID: 11020] [aspnetcorev2.dll] Known dotnet.exe location: ''
```

