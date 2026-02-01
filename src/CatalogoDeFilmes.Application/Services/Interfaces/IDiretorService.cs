using CatalogoDeFilmes.Application.Models;
namespace CatalogoDeFilmes.Application.Services.Interfaces;

public interface IDiretorService
{
    Task<List<DiretoresModel>> ListarTodos();
    Task<List<DiretoresModel>> ListarNomeId();
    Task<bool> CadastrarDiretor(DiretoresModel diretor);
    Task<DiretoresModel> GetById(int id);
    Task<bool> EditarDiretor(DiretoresModel diretor);
    Task<bool> DeletarDiretor(DiretoresModel diretor);
}
