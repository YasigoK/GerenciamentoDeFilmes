using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoDeFilmes.Domain.Entities;

public abstract class EntityBase
{
    public int Id { get; set; }


    protected EntityBase()
    {
        
    }

    [NotMapped]
    public List<string> errorMsg { get; set; } = new List<string>();

    [NotMapped]
    public bool OperacaoValida { get; set; } = true;
}
