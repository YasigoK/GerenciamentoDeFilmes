using CatalogoDeFilmes.Data.Contexts;
using CatalogoDeFilmes.Data.Repositories.Interfaces;
using CatalogoDeFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogoDeFilmes.Data.Repositories;

public class FilmesRepository : IFilmesRepository
{
    private readonly CatalogoConext _db;

    public FilmesRepository(CatalogoConext db)
    {
        _db = db;
    }

    public async Task<List<FilmesEntity>> Listar()
    {
        return await _db.Filmes.Include(f => f.Diretor).AsNoTracking().ToListAsync();
    }
}
