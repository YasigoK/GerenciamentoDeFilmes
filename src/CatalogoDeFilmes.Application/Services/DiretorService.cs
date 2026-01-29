using CatalogoDeFilmes.Application.Models;
using CatalogoDeFilmes.Application.Services.Interfaces;
using CatalogoDeFilmes.Data.Repositories.Interfaces;
using CatalogoDeFilmes.Domain.Entities;

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

    public async Task<bool> CadastrarDiretor(DiretoresModel diretor)
    {
        var entity = new DiretoresEntity(diretor.PrimeiroNome, diretor.Sobrenome, diretor.DataDeNascimento, diretor.Nacionalidade, diretor.Sexo);
        //validação

        //continua
        await _diretoresRepository.Adicionar(entity);
        await _diretoresRepository.Salvar();

        return true;
    }

    public async Task<DiretoresModel> GetById(int id)
    {
        var diretor = await _diretoresRepository.GetId(id);
        return DiretoresModel.Map(diretor);
    }
    public async Task<bool> EditarDiretor(DiretoresModel diretor)
    {
        var entity = await _diretoresRepository.GetId(diretor.Id);
        if (entity != null)
        {
            entity.Atualizar(diretor.PrimeiroNome, diretor.Sobrenome, diretor.DataDeNascimento, diretor.Nacionalidade, diretor.Sexo);
            await _diretoresRepository.Salvar();
            return true;
        }
        return false;
    }
    
}
