using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers;

public partial class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index() => View();

    [Route("/Error/{statusCode:int?}")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? statusCode)
    {
        var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        LogRequestIdOnError(statusCode, requestId);
        return View(new ErrorViewModel
        {
            ErrorMessage = statusCode == 404
                ? "We can't seem to find the page you're looking for."
                : "There was an unexpected problem serving the requested page.",
            RequestId = requestId
        });
    }

    [LoggerMessage(0, LogLevel.Information, "Error {statusCode} on request with id: {requestId}")]
    partial void LogRequestIdOnError(int? statusCode, string requestId);
}
