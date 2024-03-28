using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models;

public class Establishment
{
    /// <summary>
    /// Municipal address of the establishment.
    /// </summary>
    [StringLength(50)]
    public required string Address { get; set; }

    /// <summary>
    /// Unique identifier for an establishment.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    /// <summary>
    /// Latitude of establishment.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Longitude of establishment
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Every eating and drinking establishment receives a minimum of 1, 2, or 3 inspections each year depending on the specific
    /// type of establishment, the food preparation processes, volume and type of food served and other related criteria.
    /// Low risk premises that offer for sale only pre-packaged non-hazardous food shall be inspected once every two years.
    /// The inspection frequency for these low risk premises is shown as "O" (Other) on the report and in the data set.
    /// </summary>
    [StringLength(1)]
    public required string MinimumInspectionsPerYear { get; set; }

    /// <summary>
    /// Business name of the establishment.
    /// </summary>
    [StringLength(100)]
    public required string Name { get; set; }

    /// <summary>
    /// Establishment type ie restaurant, mobile cart.
    /// </summary>
    [StringLength(50)]
    public required string Type { get; set; }

    /// <summary>
    /// Collection of inspections related to the establishment.
    /// </summary>
    public ICollection<Inspection> Inspections { get; } = [];
}
