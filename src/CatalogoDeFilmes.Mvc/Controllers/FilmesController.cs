using CatalogoDeFilmes.Application.Services.Interfaces;
using CatalogoDeFilmes.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CatalogoDeFilmes.Mvc.Controllers;

public class FilmesController : Controller
{
    private readonly IFilmesService _filmesService;
    private readonly IDiretorService _diretorService;

    public FilmesController(IFilmesService filmesService, IDiretorService diretorService)
    {
        _filmesService = filmesService;
        _diretorService = diretorService;
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
        var listagem = await _diretorService.ListarNomeId();
        ViewBag.ListaDiretores = new SelectList(listagem, "Id", "PrimeiroNome");
        return View();
    }
}
