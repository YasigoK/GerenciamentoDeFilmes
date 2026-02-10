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
    private readonly IDiretorService _diretorService;
    private readonly IWebHostEnvironment _enviroment;

    public FilmesService(IFilmesRepository filmesRepository, IWebHostEnvironment enviroment, IDiretorService diretorService)
    {
        _filmesRepository = filmesRepository;
        _enviroment = enviroment;
        _diretorService = diretorService;
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
        var validar = await _ValidarFomulario(filme, foto);
        if (validar.Any()) { 
            filme.errorMsg.AddRange(validar);
            return false;
        }
            

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



private async Task<List<string>> _ValidarFomulario(FilmesModel filme, IFormFile foto)
    {
        var erros = new List<string>();

        if (filme.NomeFilme.IsNullOrEmpty())
        {
            erros.Add("Nome invalido, insira um valor que nÃo seja vazio");
            filme.OperacaoValida = false;
        }

        if (filme.Genero.IsNullOrEmpty())
        {
            erros.Add("Genero invalido, valor vazio");
            filme.OperacaoValida = false;
        }

        if(filme.DataLancamento > DateTime.Today )
        {
            erros.Add("Data invalida, data no futuro");
            filme.OperacaoValida = false;
        }

        if (filme.DataLancamento< new DateTime(1895, 12, 28))
        {
            erros.Add("Data invalida, data anterior ao primeiro filme registrado");
            filme.OperacaoValida = false;
        }

        if ( foto == null || foto.Length==0)
        {
            erros.Add("Nenhum arquivo foi enviado");
            filme.OperacaoValida = false;
        }

        if (filme.DiretorId_Fk != 0)
        {
            var diretor = await _diretorService.GetById(filme.DiretorId_Fk);
            if (diretor != null && filme.DataLancamento < diretor.DataDeNascimento) {
                    erros.Add("Data invalida, o filme foi lançado antes do diretor nascer");
                    filme.OperacaoValida=false;
            }
        }

        if(filme.Duracao <= 0)
        {
            erros.Add("Nota invalida, valor entre 1 e 10");
            filme.OperacaoValida = false;
        }

        if(filme.Nota>10|| filme.Nota < 1)
        {
            erros.Add("Nota invalida, valor entre 1 e 10");
            filme.OperacaoValida = false;
        }

        return erros;

    }

}
