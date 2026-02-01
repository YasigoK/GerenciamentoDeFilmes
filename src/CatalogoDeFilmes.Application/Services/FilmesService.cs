using CatalogoDeFilmes.Application.Models;
using CatalogoDeFilmes.Application.Services.Interfaces;
using CatalogoDeFilmes.Data.Repositories.Interfaces;
using CatalogoDeFilmes.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace CatalogoDeFilmes.Application.Services;

public class FilmesService :IFilmesService
{
    private readonly IFilmesRepository _filmesRepository;
    private readonly IWebHostEnvironment _enviroment;

    public FilmesService(IFilmesRepository filmesRepository, IWebHostEnvironment enviroment)
    {
        _filmesRepository = filmesRepository;
        _enviroment = enviroment;
    }


    public async Task<List<FilmesModel>> ListarTodos()
    {
        var lista = await _filmesRepository.Listar();
        return lista.Select(x =>
        {
            return FilmesModel.Map(x);
        }).ToList();
    }
    public async Task<bool> CadastrarFilme(FilmesModel filme, IFormFile foto)
    {
        if (foto != null)
        {
            string nomeFinalImagem = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(foto.FileName);
            filme.Imagem = nomeFinalImagem;

            string caminho = Path.Combine(_enviroment.WebRootPath, "imgs", "cartazes", nomeFinalImagem);

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }
        }

        var novoFilme = new FilmesEntity(
            filme.NomeFilme, 
            filme.Genero, 
            filme.DiretorId_Fk, 
            filme.DataLancamento, 
            filme.Duracao, 
            filme.Imagem, 
            filme.Nota
         );


        await _filmesRepository.CadastrarFilme(novoFilme);
        await _filmesRepository.Salvar();

        return true;
    }

    public async Task<FilmesModel> GetById(int id)
    {
        var entity = await _filmesRepository.GetId(id);
        return FilmesModel.Map(entity);
    }

    public async Task<bool> EditarFilme(FilmesModel filme, IFormFile foto)
    {

        if (foto != null)
        {
            string nomeFinalImagem = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(foto.FileName);
            filme.Imagem = nomeFinalImagem;
            string caminho = Path.Combine(_enviroment.WebRootPath, "imgs", "cartazes", nomeFinalImagem);

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }
        }
        var modelo = await _filmesRepository.GetId(filme.Id);

        if(modelo != null)
        {
            modelo.AtualizarFilme
                (
                        filme.NomeFilme, 
                        filme.Genero,
                        filme.DiretorId_Fk,
                        filme.DataLancamento,
                        filme.Duracao,
                        filme.Imagem,
                        filme.Nota
                
                );
            await _filmesRepository.Salvar();
            return true;
        }

        return false;
    }
}
