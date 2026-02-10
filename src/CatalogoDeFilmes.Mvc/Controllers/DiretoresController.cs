using CatalogoDeFilmes.Application.Models;
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

    [HttpGet]
    public IActionResult CadastrarDiretor()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarDiretor(DiretoresModel diretor)
    {
        await _dirService.CadastrarDiretor(diretor);
        if (diretor.OperacaoValida == false)
        {
            return View(diretor);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> ExibirDetalhes(int id)
    {
        var diretor = await _dirService.BuscarId(id);
        return View(diretor);
    }


    [HttpGet]
    public async Task<IActionResult> EditarDiretor(int Id)
    {
        var diretor = await _dirService.BuscarId(Id);
        return View(diretor);
    }

    [HttpPost]
    public async Task<IActionResult> EditarDiretor(DiretoresModel diretor)
    {
        await _dirService.EditarDiretor(diretor);

        return RedirectToAction("Index");
    }


    [HttpGet]
    public async Task<IActionResult> ExcluirDiretor(int id)
    {
        var diretor = await _dirService.BuscarId(id); 
        return View(diretor);
    }

    [HttpPost]
    public async Task<IActionResult> ExcluirDiretor(DiretoresModel diretor)
    {
        var sucesso = await _dirService.DeletarDiretor(diretor);

        if(sucesso != null)
        {
            return RedirectToAction("Index");   
        }
            return View(diretor);
    }
}
