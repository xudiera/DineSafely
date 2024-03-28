using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Web.Controllers;
using Web.Data.Repositories;
using Web.ViewModels;

namespace Web.UnitTests.Controllers;

public class EstablishmentControllerTests
{
    [Fact]
    public async Task SearchWithInvalidSearchViewModel_ReturnsViewResultWithNullModel()
    {
        // Arrange
        var searchViewModel = new SearchViewModel { Name = string.Empty };
        var establishmentRepository = Substitute.For<IEstablishmentRepository>();
        using var establishmentController = new EstablishmentController(establishmentRepository);
        establishmentController.ViewData.ModelState.AddModelError("key", "Error message");

        // Act
        var result = await establishmentController.SearchAsync(searchViewModel);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewData.Model);
    }

    [Theory]
    [InlineData("Restaurant", null)]
    [InlineData("Restaurant", "")]
    [InlineData("Restaurant", "Address")]
    public async Task SearchWithValidSearchViewModel_ReturnsValidViewModel(string name, string? address)
    {
        // Arrange
        var searchViewModel = new SearchViewModel { Name = name, Address = address };
        var establishmentRepository = Substitute.For<IEstablishmentRepository>();
        using var establishmentController = new EstablishmentController(establishmentRepository);

        // Act
        var result = await establishmentController.SearchAsync(searchViewModel);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<SearchViewModel>(viewResult.ViewData.Model);
        Assert.Equal(name, model.Name);
        Assert.Equal(address, model.Address);
    }
}
