using CatalogoDeFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogoDeFilmes.Data.Contexts;

public class CatalogoConext : DbContext
{
    public CatalogoConext(DbContextOptions options) : base(options) {}
    public DbSet<CatalogoDeFilmes.Domain.Entities.DiretoresEntity>Diretores { get; set; }
    public DbSet<CatalogoDeFilmes.Domain.Entities.FilmesEntity> Filmes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<FilmesEntity>(entity =>
        {
            entity.ToTable("Filmes"); // passar o nome da tabela
            entity.HasKey(e => e.Id); // vai percorrer e eonctrar o atributo id na tabela Filmes

            entity.HasOne(f=>f.Diretor).WithMany(d => d.Filmes).HasForeignKey(f => f.DiretorId_Fk).HasConstraintName("Fk_Filmes_Diretor");
        });

        builder.Entity<DiretoresEntity>().ToTable("Diretores");
    }


}
