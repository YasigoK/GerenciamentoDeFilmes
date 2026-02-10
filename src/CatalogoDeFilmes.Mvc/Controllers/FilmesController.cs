using CatalogoDeFilmes.Application.Models;
using CatalogoDeFilmes.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    [HttpGet]
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

    [HttpPost]
    public async Task<IActionResult> CadastrarFilme(FilmesModel filme, IFormFile foto)
    {
        await _filmesService.CadastrarFilme(filme, foto);
        if (filme.OperacaoValida == false) { 

            var listagem = await _diretorService.ListarNomeId();
            ViewBag.ListaDiretores = new SelectList(listagem, "Id", "PrimeiroNome");
            return View(filme);
        }

        return RedirectToAction("index");
    }

    [HttpGet]
    public async Task<IActionResult> EditarFilme(int id)
    {
        var entity = await _filmesService.BuscarId(id);
        if (entity != null)
        {
            var listagem = await _diretorService.ListarNomeId();

            ViewBag.ListaDiretores = new SelectList(listagem, "Id", "PrimeiroNome");
            return View(entity);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task <IActionResult> EditarFilme(FilmesModel filme, IFormFile foto)
    {

        await _filmesService.EditarFilme(filme, foto);
        if (filme.OperacaoValida == false)
        {
            var listagem = await _diretorService.ListarNomeId();
            ViewBag.ListaDiretores = new SelectList(listagem, "Id", "PrimeiroNome");
            return View(filme);
        }

        return RedirectToAction("index");
    }

    [HttpGet]
    public async Task <IActionResult> ExcluirFilme(int id)
    {
        var entity = await _filmesService.BuscarId(id);
        if (entity != null)
        {
            var listagem = await _diretorService.ListarNomeId();

            ViewBag.ListaDiretores = new SelectList(listagem, "Id", "PrimeiroNome");
            return View(entity);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ExcluirFilme(FilmesModel filme)
    {
        var entity = await _filmesService.DeletarFilme(filme);

        if (entity != null)
        {
            return RedirectToAction("Index");
        }

        
        return View(filme);
    }
}
