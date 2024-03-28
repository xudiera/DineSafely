using Web.Models;

namespace Web.Extensions;

public static class SeverityExtensions
{
    public static string ToReadableString(this Severity? severity)
        => severity switch
        {
            Severity.NotApplicable => "Not Applicable",
            Severity.Minor => nameof(Severity.Minor),
            Severity.Significant => nameof(Severity.Significant),
            Severity.Crucial => nameof(Severity.Crucial),
            _ => string.Empty,
        };
}
