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

    public async Task Salvar()
    {
        await _db.SaveChangesAsync();
    }

    public async Task<FilmesEntity> GetId(int id)
    {
        return await _db.Filmes.Include(d=>d.Diretor).FirstOrDefaultAsync(i  => i.Id == id);
    }
    public void CadastrarFilme(FilmesEntity filmes)
    {
        _db.Filmes.Add(filmes);
    }

    public void Delete(FilmesEntity filme)
    {
        _db.Filmes.Remove(filme);
    }
}
