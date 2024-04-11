using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Web.Controllers;
using Web.Data;
using Web.Data.DTOs;
using Web.Data.Repositories;
using Web.Models;
using Web.ViewModels;

namespace Web.UnitTests.Controllers;

public class InspectionControllerTests
{
    [Fact]
    public async Task DetailAsync_ReturnsViewResultWithViewModel_WhenInspectionDetailsExist()
    {
        // Arrange
        var id = 1;
        var pageIndex = 1;
        var mockRepository = Substitute.For<IInspectionDetailRepository>();

        // Ensure that the repository returns a non-empty list of inspection details
        var inspectionDetail = new InspectionDetail { Id = "123", Status = Status.Pass };
        var inspectionDetails = new PaginatedList<InspectionDetail>([inspectionDetail], 0, 1, 1);
        mockRepository.GetByEstablishmentIdAsync(id, pageIndex).Returns(inspectionDetails);

        // Ensure that the repository returns some severity groups
        var severityGroups = new List<SeverityGroup> { new(2, Severity.NotApplicable) };
        mockRepository.GetGroupBySeverity(id).Returns(Task.FromResult(severityGroups));

        using var controller = new InspectionController(mockRepository);

        // Act
        var result = await controller.DetailAsync(id, pageIndex);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<InspectionDetailViewModel>(viewResult.ViewData.Model);
        Assert.Equal(id, model.Id);
        Assert.NotEmpty(model.InspectionDetails);
        Assert.Equal(inspectionDetails, model.InspectionDetails);
    }

    [Fact]
    public async Task DetailAsync_WhenNoInspectionDetails_ReturnsNotFoundResult()
    {
        // Arrange
        var id = 1;
        var pageIndex = 1;
        var mockRepository = Substitute.For<IInspectionDetailRepository>();

        // Ensure that the repository returns an empty list of inspection details
        var inspectionDetails = new PaginatedList<InspectionDetail>([], 0, 1, 1);
        mockRepository.GetByEstablishmentIdAsync(id, pageIndex).Returns(inspectionDetails);

        using var controller = new InspectionController(mockRepository);

        // Act
        var result = await controller.DetailAsync(id, pageIndex);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
