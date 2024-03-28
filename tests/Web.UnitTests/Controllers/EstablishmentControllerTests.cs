using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Web.Controllers;
using Web.Data.Repositories;
using Web.ViewModels;

namespace Web.UnitTests.Controllers;

public class EstablishmentControllerTests
{
    [Theory]
    [InlineData("Restaurant", null)]
    [InlineData("Restaurant", "")]
    [InlineData("Restaurant", "Address")]
    public async void Search_ReturnsValidViewModel(string name, string? address)
    {
        // Arrange
        var establishmentRepository = Substitute.For<IEstablishmentRepository>();
        establishmentRepository.GetEstablishmentsAsync(name, address).Returns([]);
        using var controller = new EstablishmentController(establishmentRepository);

        // Act
        var result = await controller.SearchAsync(new SearchViewModel
        {
            Name = name,
            Address = address
        });

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<SearchViewModel>(viewResult.ViewData.Model);
        Assert.Equal(name, model.Name);
        Assert.Equal(address, model.Address);
        Assert.NotNull(model.Establishments);
    }
}
