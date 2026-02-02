using CatalogoDeFilmes.Application.Models;
using CatalogoDeFilmes.Application.Services.Interfaces;
using CatalogoDeFilmes.Data.Repositories.Interfaces;
using CatalogoDeFilmes.Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace CatalogoDeFilmes.Application.Services;

public class DiretorService : IDiretorService
{
    private readonly IDiretoresRepository _diretoresRepository;
    private readonly IFilmesService _filmesService;

    public DiretorService(IDiretoresRepository diretorrepository, IFilmesService filmesService)
    {
        _diretoresRepository = diretorrepository;
        _filmesService = filmesService;
    }




    public async Task<List<DiretoresModel>> ListarTodos()
    {
        var lista = await _diretoresRepository.Listar();

        return lista.Select(x =>
        {
            return DiretoresModel.Map(x);
        }).ToList();
    }
    public async Task<List<DiretoresModel>> ListarNomeId()
    {
        var entity = await _diretoresRepository.Listar();
        return entity.Select(x => new DiretoresModel
        {
            Id = x.Id,
            PrimeiroNome = $"{x.PrimeiroNome} {x.Sobrenome}",
            DataDeNascimento = x.DataDeNascimento
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

    public async Task<bool> DeletarDiretor(DiretoresModel diretor)
    {
        var entity = await _diretoresRepository.GetId(diretor.Id);
        if (entity == null)
        {
            return false;
        }

        // se houver algum filme e for diferente de nulo, significa que tem alguma tabela filho
        if (entity.Filmes.Any() && entity.Filmes != null)
        {

            //essa variável serve para criar uma cópia de memoria, pq se eu usar o entity.Filmes no lugar do foreach, assim que ele deletar, não tem mais parametro pro for, e quebra
            //ele até deleta o filho, mas não o pai
            var listaCount = entity.Filmes.ToList();
            foreach (var filme in listaCount)
            {
                var modelParaEntity = FilmesModel.Map(filme);
                await _filmesService.DeletarFilme(modelParaEntity);
            }
        }

        _diretoresRepository.Delete(entity);
        await _diretoresRepository.Salvar();
        return true;
    }
}
