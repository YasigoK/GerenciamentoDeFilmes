using CatalogoDeFilmes.Domain.Entities;

namespace CatalogoDeFilmes.Data.Repositories.Interfaces;

public interface IFilmesRepository
{
    Task<List<FilmesEntity>> Listar();
}
