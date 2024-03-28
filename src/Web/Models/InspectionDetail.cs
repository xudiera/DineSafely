using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models;

public class InspectionDetail
{
    /// <summary>
    /// Enforcement activity based on the infractions noted during a food safety inspection.
    /// </summary>
    [StringLength(50)]
    public string? Action { get; set; }

    /// <summary>
    /// Fine determined in a court outcome.
    /// </summary>
    public decimal? AmountFined { get; set; }

    /// <summary>
    /// The registered court decision resulting from the issuance of a ticket or summons for outstanding infractions
    /// to the Health Protection and Promotion Act.
    /// </summary>
    [StringLength(50)]
    public string? CourtOutcome { get; set; }

    /// <summary>
    /// Unique ID for an inspection detail.
    /// </summary>
    [StringLength(50)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public required string Id { get; set; }

    /// <summary>
    /// Description of the Infraction.
    /// </summary>
    [StringLength(250)]
    public string? InfractionDetails { get; set; }

    /// <summary>
    /// Unique identifier for an inspection.
    /// </summary>
    public int InspectionId { get; set; }

    /// <summary>
    /// Level of the infraction, i.e. S - Significant, M - Minor, C - Crucial.
    /// </summary>
    [StringLength(50)]
    public string? Severity { get; set; }

    /// <summary>
    /// Pass, Conditional Pass, Closed.
    /// </summary>
    [StringLength(50)]
    public required string Status { get; set; }
}
