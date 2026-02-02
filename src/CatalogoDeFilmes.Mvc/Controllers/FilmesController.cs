using CatalogoDeFilmes.Application.Models;
using CatalogoDeFilmes.Application.Services.Interfaces;
using CatalogoDeFilmes.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        ViewBag.ListaDiretores = new SelectList(listagem, "Id", "PrimeiroNome", "DataDeNascimento");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarFilme(FilmesModel filmes, IFormFile foto)
    {
        var validar = await _filmesService.ValidarFomulario(filmes,foto);
        if (validar==false)
        {
            var listagem = await _diretorService.ListarNomeId();
            ViewBag.ListaDiretores = new SelectList(listagem, "Id", "PrimeiroNome");

            return View(filmes);
        }
        var result = await _filmesService.CadastrarFilme(filmes,foto);

        return RedirectToAction("index");
    }



    [HttpGet]
    public async Task<IActionResult> EditarFilme(int id)
    {
        var entity = await _filmesService.GetById(id);
        var listagem = await _diretorService.ListarNomeId();

        ViewBag.ListaDiretores = new SelectList(listagem, "Id", "PrimeiroNome");
        return View(entity);
    }

    [HttpPost]
    public async Task <IActionResult> EditarFilme(FilmesModel filme, IFormFile foto)
    {
        if (ModelState.IsValid ==false)
        {
            var listagem = await _diretorService.ListarNomeId();
            ViewBag.ListaDiretores = new SelectList(listagem, "Id", "PrimeiroNome");

            return View(filme);
        }

        await _filmesService.EditarFilme(filme,foto);
        return RedirectToAction("index");
    }

    [HttpGet]
    public async Task <IActionResult> DeletarFilme(int id)
    {
        var entity = await _filmesService.GetById(id);
        var listagem = await _diretorService.ListarNomeId();

        ViewBag.ListaDiretores = new SelectList(listagem, "Id", "PrimeiroNome");
        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> DeletarFilme(FilmesModel filme)
    {
        var entity = await _filmesService.DeletarFilme(filme);

        if (entity != null)
        {
            return RedirectToAction("Index");
        }

        
        return View(filme);
    }
}
