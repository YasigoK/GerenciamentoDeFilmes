using CatalogoDeFilmes.Application.Models;
using CatalogoDeFilmes.Application.Services.Interfaces;
using CatalogoDeFilmes.Data.Repositories;
using CatalogoDeFilmes.Data.Repositories.Interfaces;

namespace CatalogoDeFilmes.Application.Services;

public class FilmesService :IFilmesService
{
    private readonly IFilmesRepository _filmesRepository;

    public FilmesService(IFilmesRepository filmesRepository)
    {
        _filmesRepository = filmesRepository;
    }

    public async Task<List<FilmesModel>> ListarTodos()
    {
        var lista = await _filmesRepository.Listar();
        return lista.Select(x =>
        {
            return FilmesModel.Map(x);
        }).ToList();
    }

}
