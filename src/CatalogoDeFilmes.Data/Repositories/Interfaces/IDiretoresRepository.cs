using CatalogoDeFilmes.Domain.Entities;
namespace CatalogoDeFilmes.Data.Repositories.Interfaces;

public interface IDiretoresRepository
{
    Task<List<DiretoresEntity>> Listar();
    Task<DiretoresEntity> GetId(int id);
    Task Salvar();
    void CadastrarDiretor(DiretoresEntity diretores);
    void Delete(DiretoresEntity entity);
}
