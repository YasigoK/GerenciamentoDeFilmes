using CatalogoDeFilmes.Application.Models;
using Microsoft.AspNetCore.Http;

namespace CatalogoDeFilmes.Application.Services.Interfaces;

public interface IFilmesService
{
    Task<List<FilmesModel>> ListarTodos();
    Task<bool> CadastrarFilme(FilmesModel filme, IFormFile foto);
}
