using CatalogoDeFilmes.Domain.Entities;
namespace CatalogoDeFilmes.Data.Repositories.Interfaces;

public interface IDiretoresRepository
{
    Task<List<DiretoresEntity>> Listar();
    Task Adicionar(DiretoresEntity diretores);
    Task Salvar();
    Task<DiretoresEntity> GetId(int id);
    
}
