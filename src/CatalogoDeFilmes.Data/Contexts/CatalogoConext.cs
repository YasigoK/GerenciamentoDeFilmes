using Microsoft.EntityFrameworkCore;

namespace CatalogoDeFilmes.Data.Contexts;

public class CatalogoConext : DbContext
{
    public CatalogoConext(DbContextOptions options) : base(options) {}
    public DbSet<CatalogoDeFilmes.Domain.Entities.DiretoresEntity>Diretores { get; set; }

}
