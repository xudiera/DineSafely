using Web.Models;

namespace Web.Data.DTOs;

public class SeverityGroupDTO
{
    public required int Count { get; set; }

    public Severity? Severity { get; set; }
}
