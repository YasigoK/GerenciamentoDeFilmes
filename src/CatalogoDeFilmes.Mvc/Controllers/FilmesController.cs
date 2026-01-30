using CatalogoDeFilmes.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoDeFilmes.Mvc.Controllers;

public class FilmesController : Controller
{
    private readonly IFilmesService _filmesService;

    public FilmesController(IFilmesService filmesService)
    {
        _filmesService = filmesService;
    }

    //[HttpGet]
    public async Task<IActionResult> Index()
    {
        var listagem = await _filmesService.ListarTodos();
        return View(listagem);
    }

    [HttpGet]
    public async Task<IActionResult> CadastrarFilme()
    {
        return View();
    }
}
