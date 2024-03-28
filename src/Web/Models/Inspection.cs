using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models;

public class Inspection
{
    /// <summary>
    /// Calendar date the inspection was conducted.
    /// </summary>
    public DateOnly? Date { get; set; }

    /// <summary>
    /// Unique identifier for an establishment.
    /// </summary>
    public int EstablishmentId { get; set; }

    /// <summary>
    /// Unique ID for an inspection.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    /// <summary>
    /// Collection of inspection details related to the inspection.
    /// </summary>
    public ICollection<InspectionDetail> InspectionDetails { get; } = [];
}
