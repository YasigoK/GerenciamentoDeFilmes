using CatalogoDeFilmes.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoDeFilmes.Mvc.Controllers;

public class DiretoresController : Controller
{
    private readonly IDiretorService _dirService;

    public DiretoresController(IDiretorService dirService)
    {
        _dirService = dirService;
    }

    public async Task<IActionResult> Index()
    {
        var listagem = await _dirService.ListarTodos();
        return View(listagem);
    }
}
