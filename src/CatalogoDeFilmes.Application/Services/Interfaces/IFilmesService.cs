using CatalogoDeFilmes.Application.Models;

namespace CatalogoDeFilmes.Application.Services.Interfaces;

public interface IFilmesService
{
    Task<List<FilmesModel>> ListarTodos();
}
