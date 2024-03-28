
using System.ComponentModel.DataAnnotations;
using Web.Models;

namespace Web.ViewModels;

public class SearchViewModel
{
    [StringLength(50)]
    public string? Address { get; set; }

    public IEnumerable<Establishment> Establishments { get; set; } = [];

    [StringLength(100)]
    [Required(ErrorMessage = "The establishment name field is required.")]
    public required string Name { get; set; }
}
