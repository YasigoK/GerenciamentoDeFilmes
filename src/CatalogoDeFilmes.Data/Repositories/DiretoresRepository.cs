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
    
    public async Task Salvar()
    {
        await _db.SaveChangesAsync();
    }
    public async Task<DiretoresEntity> GetId(int id)
    {
        return await _db.Diretores.Include(d=>d.Filmes).FirstOrDefaultAsync(x => x.Id == id);
    }
    public void CadastrarDiretor(DiretoresEntity diretores)
    {
        _db.Diretores.Add(diretores);
    }

    public void Delete(DiretoresEntity filme)
    {
        _db.Diretores.Remove(filme);
    }
}
