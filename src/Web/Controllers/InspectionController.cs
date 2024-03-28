using Microsoft.AspNetCore.Mvc;
using Web.Data.Repositories;
using Web.ViewModels;

namespace Web.Controllers;

public class InspectionController(IInspectionDetailRepository inspectionDetailRepository) : Controller
{
    public async Task<IActionResult> DetailAsync(int id, int? pageIndex)
    {
        var inspectionDetails = await inspectionDetailRepository.GetByEstablishmentIdAsync(id, pageIndex ?? 1);

        return !inspectionDetails.Any()
            ? NotFound()
            : View(new InspectionDetailViewModel { Id = id, InspectionDetails = inspectionDetails });
    }
}
