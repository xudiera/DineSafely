using Microsoft.AspNetCore.Mvc;
using Web.Data.Repositories;
using Web.ViewModels;

namespace Web.Controllers;

public class InspectionController(IInspectionDetailRepository inspectionDetailRepository) : Controller
{
    public async Task<IActionResult> DetailAsync(int id, int? pageIndex)
    {
        var inspectionDetails = await inspectionDetailRepository.GetByEstablishmentIdAsync(id, pageIndex ?? 1);
        if (!inspectionDetails.Any())
        {
            return NotFound();
        }

        var severityGroups = await inspectionDetailRepository.GetGroupBySeverity(id);

        return View(new InspectionDetailViewModel(severityGroups)
        {
            Id = id,
            InspectionDetails = inspectionDetails
        });
    }
}
