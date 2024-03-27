using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public partial class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        LogRequestIdOnError(requestId);
        return View(new ErrorViewModel { RequestId = requestId });
    }

    [LoggerMessage(0, LogLevel.Information, "Error on request with id: {requestId}")]
    partial void LogRequestIdOnError(string requestId);
}
