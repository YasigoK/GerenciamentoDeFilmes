using CatalogoDeFilmes.Application.Models;
using Microsoft.AspNetCore.Http;

namespace CatalogoDeFilmes.Application.Services.Interfaces;

public interface IFilmesService
{
    Task<List<FilmesModel>> ListarTodos();
    Task<FilmesModel> BuscarId(int id);
    Task<bool> CadastrarFilme(FilmesModel filme, IFormFile foto);
    Task<bool> EditarFilme(FilmesModel filme, IFormFile foto);
    Task<bool> DeletarFilme(FilmesModel filme);
}
