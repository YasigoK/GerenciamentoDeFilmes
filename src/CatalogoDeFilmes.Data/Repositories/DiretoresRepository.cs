using CatalogoDeFilmes.Data.Contexts;
using CatalogoDeFilmes.Data.Repositories.Interfaces;
using CatalogoDeFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogoDeFilmes.Data.Repositories;

public class DiretoresRepository : IDiretoresRepository
{
    private readonly CatalogoConext _db;

    public DiretoresRepository(CatalogoConext db)
    {
        _db = db;
    }

    public async Task<List<DiretoresEntity>> Listar()
    {
        return await _db.Diretores.AsNoTracking().ToListAsync();
    }
}
