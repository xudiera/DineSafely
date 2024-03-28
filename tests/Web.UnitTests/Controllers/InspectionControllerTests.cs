using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Web.Controllers;
using Web.Data.Repositories;
using Web.Models;

namespace Web.UnitTests.Controllers;

public class InspectionControllerTests
{
    [Fact]
    public async Task EstablishmentFound_ReturnsDetails()
    {
        // Arrange
        var establishment = new Establishment
        {
            Address = "address",
            MinimumInspectionsPerYear = "1",
            Name = "name",
            Type = "restaurant"
        };
        var establishmentRepository = Substitute.For<IEstablishmentRepository>();
        establishmentRepository.GetByIdAsync(Arg.Any<int>()).Returns(establishment);
        using var inspectionController = new InspectionController(establishmentRepository);

        // Act
        var result = await inspectionController.DetailAsync(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Establishment>(viewResult.ViewData.Model);
    }

    [Fact]
    public async Task EstablishmentNotFound_ReturnsNotFound()
    {
        // Arrange
        var establishmentRepository = Substitute.For<IEstablishmentRepository>();
        establishmentRepository.GetByIdAsync(Arg.Any<int>()).Returns((Establishment?)null);
        using var inspectionController = new InspectionController(establishmentRepository);

        // Act
        var result = await inspectionController.DetailAsync(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
