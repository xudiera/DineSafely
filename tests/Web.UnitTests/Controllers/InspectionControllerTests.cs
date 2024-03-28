using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Web.Controllers;
using Web.Data;
using Web.Data.Repositories;
using Web.Models;
using Web.ViewModels;

namespace Web.UnitTests.Controllers;

public class InspectionControllerTests
{
    [Fact]
    public async Task EstablishmentFound_ReturnsDetails()
    {
        // Arrange
        var establishmentId = 1;
        var InspectionDetail = new InspectionDetail { Id = "1", Status = "" };
        var inspectionDetails = new PaginatedList<InspectionDetail>([InspectionDetail], 0, 1, 1);
        var inspectionDetailRepository = Substitute.For<IInspectionDetailRepository>();
        inspectionDetailRepository.GetByEstablishmentIdAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(inspectionDetails);
        using var inspectionController = new InspectionController(inspectionDetailRepository);

        // Act
        var result = await inspectionController.DetailAsync(establishmentId, 1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<InspectionDetailViewModel>(viewResult.ViewData.Model);
        Assert.NotEmpty(model.InspectionDetails);
        Assert.Equal(establishmentId, model.Id);
    }

    [Fact]
    public async Task EstablishmentNotFound_ReturnsNotFound()
    {
        // Arrange
        var inspectionDetails = new PaginatedList<InspectionDetail>([], 0, 1, 1);
        var inspectionDetailRepository = Substitute.For<IInspectionDetailRepository>();
        inspectionDetailRepository.GetByEstablishmentIdAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(inspectionDetails);
        using var inspectionController = new InspectionController(inspectionDetailRepository);

        // Act
        var result = await inspectionController.DetailAsync(1, 1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
