using CatalogoDeFilmes.Domain.Entities;

namespace CatalogoDeFilmes.Data.Repositories.Interfaces;

public interface IFilmesRepository
{
    Task<List<FilmesEntity>> Listar();
    Task<FilmesEntity> GetId(int id);
    Task Salvar();
    void CadastrarFilme(FilmesEntity filmes);
    void Delete(FilmesEntity filme);

}
