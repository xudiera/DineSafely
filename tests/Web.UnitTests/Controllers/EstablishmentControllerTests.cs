using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Web.Controllers;
using Web.Data.Repositories;
using Web.ViewModels;

namespace Web.UnitTests.Controllers;

public class EstablishmentControllerTests
{
    [Fact]
    public async Task SearchAsync_InvalidViewModel_ReturnsInvalidSearchView()
    {
        // Arrange
        var mockRepository = Substitute.For<IEstablishmentRepository>();
        using var controller = new EstablishmentController(mockRepository);
        controller.ModelState.AddModelError("TestKey", "TestError"); // Simulate invalid model state
        var invalidViewModel = new SearchViewModel { Name = string.Empty };

        // Act
        var result = await controller.SearchAsync(invalidViewModel);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewData.Model);
    }

    [Theory]
    [InlineData("TestName", null)]
    [InlineData("TestName", "")]
    [InlineData("TestName", "TestAddress")]
    public async Task SearchAsync_ValidViewModel_ReturnsSearchViewWithEstablishments(string name, string? address)
    {
        // Arrange
        var mockRepository = Substitute.For<IEstablishmentRepository>();
        using var controller = new EstablishmentController(mockRepository);
        var validViewModel = new SearchViewModel
        {
            Name = name,
            Address = address
        };

        // Act
        var result = await controller.SearchAsync(validViewModel);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<SearchViewModel>(viewResult.ViewData.Model);
        Assert.Equal(name, model.Name);
        Assert.Equal(address, model.Address);
    }
}
