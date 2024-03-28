using Microsoft.AspNetCore.Mvc;
using Web.Data.Repositories;
using Web.ViewModels;

namespace Web.Controllers;

public class EstablishmentController(IEstablishmentRepository establishmentRepository) : Controller
{
    public async Task<IActionResult> SearchAsync(SearchViewModel searchViewModel)
    {
        if (searchViewModel is null || !ModelState.IsValid)
        {
            return View();
        }

        searchViewModel.Establishments = await establishmentRepository
            .GetAsync(searchViewModel.Name, searchViewModel.Address, searchViewModel.PageIndex ?? 1);

        return View(searchViewModel);
    }
}
