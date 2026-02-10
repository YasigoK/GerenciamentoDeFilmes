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
    private readonly IDiretoresRepository _diretorRepository;

    public FilmesService(IFilmesRepository filmesRepository,IDiretoresRepository diretoresRepository, IWebHostEnvironment enviroment)
    {
        _filmesRepository = filmesRepository;
        _enviroment = enviroment;
        _diretorRepository = diretoresRepository;
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
        var validar = await _ValidarFomularioFilmes(filme, foto);
        if (validar.Any()) 
        {
            filme.OperacaoValida = false;
            filme.errorMsg.AddRange(validar);
            return false;
        }

        await _SalvarImagemAsync(filme, foto);

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

    public async Task<FilmesModel> BuscarId(int id)
    {
        var entity = await _filmesRepository.BuscarId(id);
        return FilmesModel.Map(entity);
    }

    public async Task<bool> EditarFilme(FilmesModel filme, IFormFile foto)
    {
        var validar = await _ValidarFomularioFilmes(filme, foto);
        if (validar.Any())
        {
            filme.OperacaoValida = false;
            filme.errorMsg.AddRange(validar);
            return false;
        }

        var modelo = await _filmesRepository.BuscarId(filme.Id);
        if(modelo == null)
        {
            filme.OperacaoValida = false;
            filme.errorMsg.AddRange("Formulario invalido");
            return false;
        }

        if (foto != null)
        {
            await _DeletarSeExistir(filme);
            await _SalvarImagemAsync(filme, foto);
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
        var entity = await _filmesRepository.BuscarId(filme.Id);

        if (entity == null)
        {
            filme.OperacaoValida = false;
            filme.errorMsg.Add("Formulario invalido");
            return false;
        }

        await _DeletarSeExistir(filme);

        _filmesRepository.Delete(entity);
        await _filmesRepository.Salvar();

        return true;
    }

    private async Task _SalvarImagemAsync(FilmesModel filme, IFormFile foto)
    {
        var nomeFinalImagem = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(foto.FileName);

        filme.Imagem = nomeFinalImagem;

        var caminho = Path.Combine(_enviroment.WebRootPath, "imgs", "cartazes", nomeFinalImagem);

        using var stream = new FileStream(caminho, FileMode.Create);
        await foto.CopyToAsync(stream);
    }

    private async Task _DeletarSeExistir(FilmesModel filme)
    {
        if (!string.IsNullOrEmpty(filme.Imagem))
        {
            string caminhoAntigo = Path.Combine(_enviroment.WebRootPath, "imgs", "cartazes", filme.Imagem);
            if (System.IO.File.Exists(caminhoAntigo))
            {
                System.IO.File.Delete(caminhoAntigo);
            }
        }
    }

    private async Task<List<string>> _ValidarFomularioFilmes(FilmesModel filme, IFormFile foto)
    {
        var erros = new List<string>();

        if (filme.NomeFilme.IsNullOrEmpty())
            erros.Add("Nome invalido, insira um valor que não seja vazio");


        if (filme.Genero.IsNullOrEmpty())
            erros.Add("Genero invalido, valor vazio");
        

        if(filme.DataLancamento > DateTime.Today )
            erros.Add("Data invalida, data no futuro");


        if (filme.DataLancamento< new DateTime(1895, 12, 28))
            erros.Add("Data invalida, data anterior ao primeiro filme registrado");


        if (filme.Imagem == null && (foto == null || foto.Length == 0))
            erros.Add("Nenhum arquivo foi enviado");

        if(foto!= null && foto.Length>0)
        {
            var tiposPermitidos = new[] { "image/jpeg", "image/png", "image/webp" };

            if (!tiposPermitidos.Contains(foto.ContentType))
            {
                erros.Add("Extensão de imagem invalida envie um Png, Jpeg ou Webp");
            }

        }
        
        var diretor = await _diretorRepository.BuscarId(filme.DiretorId_Fk);
        if (diretor != null && filme.DataLancamento < diretor.DataDeNascimento)
            erros.Add("Data invalida, o filme foi lançado antes do diretor nascer");


        if (filme.Duracao <= 0)
            erros.Add("Duração invalida, valor entre 1 e 10");


        if(filme.Nota>10|| filme.Nota < 1)
            erros.Add("Nota invalida, valor entre 1 e 10");


        return erros;

    }

}
