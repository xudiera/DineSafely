using Web.Data;
using Web.Data.DTOs;
using Web.Models;

namespace Web.ViewModels;

public class InspectionDetailViewModel
{
    public InspectionDetailViewModel(ICollection<SeverityGroup> severityGroups)
    {
        if (severityGroups is null || severityGroups.Count == 0)
        {
            throw new ArgumentNullException(nameof(severityGroups), "Severity groups must not be null or empty.");
        }

        var totalSeverities = severityGroups.Sum(g => g.Count);

        foreach (var severityGroup in severityGroups)
        {
            var percentage = CalculatePercentage(severityGroup.Count, totalSeverities);

            switch (severityGroup.Severity)
            {
                case Severity.Minor:
                    PercentageMinor = percentage;
                    break;
                case Severity.Significant:
                    PercentageSignificant = percentage;
                    break;
                case Severity.Crucial:
                    PercentageCrucial = percentage;
                    break;
                case Severity.NotApplicable:
                case null:
                default:
                    PercentageRemaining += percentage;
                    break;
            }
        }

        var remainingPercentage = 100 - PercentageCrucial - PercentageMinor - PercentageRemaining - PercentageSignificant;

        // Assigning always the remainder to the "best" severity
        if (remainingPercentage > 0)
        {
            if (PercentageRemaining > 0)
            {
                PercentageRemaining += remainingPercentage;
            }
            else if (PercentageMinor > 0)
            {
                PercentageMinor += remainingPercentage;
            }
            else if (PercentageSignificant > 0)
            {
                PercentageSignificant += remainingPercentage;
            }
            else
            {
                PercentageCrucial += remainingPercentage;
            }
        }
    }

    public required int Id { get; init; }

    public required PaginatedList<InspectionDetail> InspectionDetails { get; init; }

    public string MapUrl
    {
        get
        {
            var establishment = InspectionDetails.First().Inspection.Establishment;
            return $"https://www.google.com/maps/search/?api=1&query={establishment.Name}%2C{establishment.Address}%2CToronto%2CON";
        }
    }

    public decimal PercentageCrucial { get; }
    public decimal PercentageMinor { get; }
    public decimal PercentageRemaining { get; }
    public decimal PercentageSignificant { get; }

    public string RowCssClass(Severity? severity)
        => severity switch
        {
            Severity.NotApplicable => "table-success",
            Severity.Minor => "table-secondary",
            Severity.Significant => "table-warning",
            Severity.Crucial => "table-danger",
            null => "table-success",
            _ => "table-success"
        };

    private static decimal CalculatePercentage(decimal count, int total)
        => total == 0
            ? 0
            : Math.Round(decimal.Divide(count * 100, total), 2);
}
