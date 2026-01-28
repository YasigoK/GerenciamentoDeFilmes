using CatalogoDeFilmes.Domain.Entities;
namespace CatalogoDeFilmes.Data.Repositories.Interfaces;

public interface IDiretoresRepository
{
    Task<List<DiretoresEntity>> Listar();
}
