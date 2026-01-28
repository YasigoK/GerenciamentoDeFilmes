using CatalogoDeFilmes.Application.Models;
namespace CatalogoDeFilmes.Application.Services.Interfaces;

public interface IDiretorService
{
    Task<List<DiretoresModel>> ListarTodos();
}
