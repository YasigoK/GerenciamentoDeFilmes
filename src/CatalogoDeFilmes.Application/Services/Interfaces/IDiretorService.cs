using CatalogoDeFilmes.Application.Models;
namespace CatalogoDeFilmes.Application.Services.Interfaces;

public interface IDiretorService
{
    Task<List<DiretoresModel>> ListarTodos();
    Task<List<DiretoresModel>> ListarNomeId();
    Task<DiretoresModel> BuscarId(int id);
    Task<bool> CadastrarDiretor(DiretoresModel diretor);
    Task<bool> EditarDiretor(DiretoresModel diretor);
    Task<bool> DeletarDiretor(DiretoresModel diretor);
}
