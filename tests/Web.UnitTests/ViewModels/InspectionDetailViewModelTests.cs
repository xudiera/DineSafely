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
        var severityGroups = new List<SeverityGroupDTO>
        {
            new() { Severity = Severity.Minor, Count = 3 },
            new() { Severity = Severity.Significant, Count = 2 },
            new() { Severity = Severity.Crucial, Count = 2 },
            new() { Severity = Severity.NotApplicable, Count = 1 },
            new() { Severity = null, Count = 1 }
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
