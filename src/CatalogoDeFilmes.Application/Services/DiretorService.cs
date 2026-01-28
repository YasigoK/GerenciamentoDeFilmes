using CatalogoDeFilmes.Application.Models;
using CatalogoDeFilmes.Application.Services.Interfaces;
using CatalogoDeFilmes.Data.Repositories.Interfaces;

namespace CatalogoDeFilmes.Application.Services;

public class DiretorService : IDiretorService
{
    private readonly IDiretoresRepository _diretoresRepository;

    public DiretorService(IDiretoresRepository diretorrepository)
    {
        _diretoresRepository = diretorrepository;
    }




    public async Task<List<DiretoresModel>> ListarTodos()
    {
        var lista = await _diretoresRepository.Listar();

        return lista.Select(x =>
        {
            return DiretoresModel.Map(x);
        }).ToList();
    }
}
