using Web.Data;
using Web.Data.DTOs;
using Web.Models;
using Web.ViewModels;

namespace Web.UnitTests.ViewModels;

public class InspectionDetailViewModelTests
{
    [Fact]
    public void Constructor_ValidSeverityGroups_CalculatesPercentagesCorrectly()
    {
        // Arrange
        var severityGroups = new List<SeverityGroup>
        {
            new(3, Severity.Minor),
            new(2, Severity.Significant),
            new(2, Severity.Crucial),
            new(1, Severity.NotApplicable),
            new(1, null)
        };

        // Act
        var viewModel = new InspectionDetailViewModel(severityGroups)
        {
            Id = 1,
            InspectionDetails = new PaginatedList<InspectionDetail>([], 0, 1, 1)
        };

        // Assert
        Assert.Equal(33.33m, viewModel.PercentageMinor);
        Assert.Equal(22.22m, viewModel.PercentageSignificant);
        Assert.Equal(22.22m, viewModel.PercentageCrucial);
        Assert.Equal(22.23m, viewModel.PercentageRemaining); // Assigning always the remainder to the "best" severity
    }
}
