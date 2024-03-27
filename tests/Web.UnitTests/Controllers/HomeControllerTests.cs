using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Web.Controllers;
using Web.ViewModels;

namespace Web.UnitTests.Controllers;

public class HomeControllerTests
{
    [Theory]
    [InlineData(null, "There was an unexpected problem serving the requested page.")]
    [InlineData(404, "We can't seem to find the page you're looking for.")]
    [InlineData(500, "There was an unexpected problem serving the requested page.")]
    public void Error_ReturnsValidStatusCodeAndErrorMessage(int? statusCode, string errorMessage)
    {
        // Arrange
        var traceIdentifier = "trace identifier";
        var logger = Substitute.For<ILogger<HomeController>>();
        using var controller = new HomeController(logger);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
        controller.ControllerContext.HttpContext.TraceIdentifier = "trace identifier";

        // Act
        var result = controller.Error(statusCode);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<ErrorViewModel>(viewResult.ViewData.Model);
        Assert.Equal(errorMessage, model.ErrorMessage);
        Assert.Equal(traceIdentifier, model.RequestId);
    }
}
