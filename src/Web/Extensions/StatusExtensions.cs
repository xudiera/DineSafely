using Web.Models;

namespace Web.Extensions;

public static class StatusExtensions
{
    public static string ToReadableString(this Status status)
        => status switch
        {
            Status.Pass => nameof(Status.Pass),
            Status.ConditionalPass => "Conditional Pass",
            Status.Closed => nameof(Status.Closed),
            _ => string.Empty,
        };
}
