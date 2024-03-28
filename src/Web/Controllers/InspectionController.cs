using Microsoft.AspNetCore.Mvc;
using Web.Data.Repositories;

namespace Web.Controllers;

public class InspectionController(IEstablishmentRepository establishmentRepository) : Controller
{
    public async Task<IActionResult> DetailAsync(int id)
    {
        var establishment = await establishmentRepository.GetByIdAsync(id);

        return establishment is null
            ? NotFound()
            : View(establishment);
    }
}
