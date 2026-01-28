using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoDeFilmes.Domain.Entities;

public abstract class EntityBase
{
    public int Id { get; set; }


    protected EntityBase()
    {
        
    }

    [NotMapped]
    public string errorMsg { get; set; }

    [NotMapped]
    public bool OperacaoValida { get; set; }
}
