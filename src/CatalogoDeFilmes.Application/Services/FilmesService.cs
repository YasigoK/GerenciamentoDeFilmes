using CatalogoDeFilmes.Application.Models;
using CatalogoDeFilmes.Application.Services.Interfaces;
using CatalogoDeFilmes.Data.Repositories.Interfaces;
using CatalogoDeFilmes.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;


namespace CatalogoDeFilmes.Application.Services;

public class FilmesService :IFilmesService
{
    private readonly IFilmesRepository _filmesRepository;
    private readonly IWebHostEnvironment _enviroment;

    public FilmesService(IFilmesRepository filmesRepository, IWebHostEnvironment enviroment)
    {
        _filmesRepository = filmesRepository;
        _enviroment = enviroment;
    }


    public async Task<List<FilmesModel>> ListarTodos()
    {
        var lista = await _filmesRepository.Listar();
        return lista.Select(x =>
        {
            return FilmesModel.Map(x);
        }).ToList();
    }
    public async Task<bool> CadastrarFilme(FilmesModel filme, IFormFile foto)
    {
        if (foto != null)
        {
            string nomeFinalImagem = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(foto.FileName);
            filme.Imagem = nomeFinalImagem;

            string caminho = Path.Combine(_enviroment.WebRootPath, "imgs", "cartazes", nomeFinalImagem);

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }
        }

        var novoFilme = new FilmesEntity(
            filme.NomeFilme, 
            filme.Genero, 
            filme.DiretorId_Fk, 
            filme.DataLancamento, 
            filme.Duracao, 
            filme.Imagem, 
            filme.Nota
         );


        _filmesRepository.CadastrarFilme(novoFilme);
        await _filmesRepository.Salvar();

        return true;
    }

    public async Task<FilmesModel> GetById(int id)
    {
        var entity = await _filmesRepository.GetId(id);
        return FilmesModel.Map(entity);
    }

    public async Task<bool> EditarFilme(FilmesModel filme, IFormFile foto)
    {
        var modelo = await _filmesRepository.GetId(filme.Id);
        if(modelo == null)
        {
            filme.OperacaoValida = false;
            filme.errorMsg.Add("Formulario invalido");
            return false;
        }

        if (foto != null)
        {

            if (!string.IsNullOrEmpty(modelo.Imagem))
            {
                string caminhoAntigo = Path.Combine(_enviroment.WebRootPath, "imgs", "cartazes", modelo.Imagem);
                if (System.IO.File.Exists(caminhoAntigo))
                {
                    System.IO.File.Delete(caminhoAntigo);
                }
            }


            string nomeFinalImagem = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(foto.FileName);
            filme.Imagem = nomeFinalImagem;
            string caminho = Path.Combine(_enviroment.WebRootPath, "imgs", "cartazes", nomeFinalImagem);

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }
        }


        modelo.AtualizarFilme
            (
                    filme.NomeFilme, 
                    filme.Genero,
                    filme.DiretorId_Fk,
                    filme.DataLancamento,
                    filme.Duracao,
                    filme.Imagem,
                    filme.Nota
                
            );
        await _filmesRepository.Salvar();
        return true;

    }

    public async Task<bool> DeletarFilme(FilmesModel filme)
    {
        var entity = await _filmesRepository.GetId(filme.Id);

        if (entity == null)
        {
            filme.OperacaoValida = false;
            filme.errorMsg.Add("Formulario invalido");
            return false;
        }

        string caminho = Path.Combine(_enviroment.WebRootPath, "imgs", "cartazes", filme.Imagem);

        if(System.IO.File.Exists(caminho))  // se o arquivo existir
            System.IO.File.Delete(caminho);
        

        _filmesRepository.Delete(entity);
        await _filmesRepository.Salvar();

        return true;
    }



    public async Task<bool> ValidarFomulario(FilmesModel filme, IFormFile foto)
    {
        //Nome
        if (filme.NomeFilme.IsNullOrEmpty())
        {
            filme.errorMsg.Add("Nome vazio");
            filme.OperacaoValida = false;
        }

        //Genero
        if (filme.Genero.IsNullOrEmpty())
        {
            filme.errorMsg.Add("Genero vazio");
            filme.OperacaoValida = false;
        }

        //Data
        if(filme.DataLancamento > DateTime.Today ||filme.DataLancamento == DateTime.MinValue)
        {
            filme.errorMsg.Add("Data invalida, data no futuro");
            filme.OperacaoValida = false;
        }
        //Data antes do diretor
        //if(filme.DataLancamento<filme.)


        //Imagem
        if ( foto == null)
        {
            filme.errorMsg.Add("Nenhum arquivo foi enviado");
            filme.OperacaoValida = false;
        }

        //Diretor
        if (filme.DiretorId_Fk == 0 )
        {
            filme.errorMsg.Add("É necessário a seleção e um diretor para o filme");
            filme.OperacaoValida = false;
        }

        //Duração
        if(filme.Duracao < 1)
        {
            filme.errorMsg.Add("Nota invalida, valor entre 1 e 10");
            filme.OperacaoValida = false;
        }

        //Nota
        if(filme.Nota>10|| filme.Nota < 1)
        {
            filme.errorMsg.Add("Nota invalida, valor entre 1 e 10");
            filme.OperacaoValida = false;
        }



        if (filme.OperacaoValida == false)
        {
            return false;
        }
        return true;
    }
}
