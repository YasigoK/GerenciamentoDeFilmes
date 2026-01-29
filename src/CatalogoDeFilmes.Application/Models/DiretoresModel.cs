using CatalogoDeFilmes.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CatalogoDeFilmes.Application.Models;

public class DiretoresModel : EntityBase
{

    [DisplayName("Digite o nome")]
    [MaxLength(70)]
    [Required(ErrorMessage ="Campo obrigatório, entre com um nome de até 70 caracteres")]
    public string PrimeiroNome { get;  set; }

    [DisplayName("Digite o Sobrenome")]
    [MaxLength(70)]
    [Required(ErrorMessage = "Campo obrigatório, entre com um sobrenome de até 70 caracteres")]
    public string Sobrenome { get;  set; }

    [DisplayName("Digite a data de nascimento ")]
    [Required(ErrorMessage = "Campo obigatórios, Data de nascimento obrigatória")]
    [DataType(DataType.Date)]
    public DateTime DataDeNascimento { get;  set; }

    [DisplayName("Digite a nacionalidade ")]
    [MaxLength(100)]
    [Required(ErrorMessage = "Campo obigatórios, digite o pais de origem de até 100 caractéres.")]
    public string Nacionalidade { get;  set; }

    [DisplayName("Digite o sexo")]
    [Required(ErrorMessage = "Campo obigatórios, digite o sexo")]
    public char Sexo { get;  set; }

    public static DiretoresModel Map(DiretoresEntity entity)
    {
        if (entity == null)
            return null;

        return new DiretoresModel

        {
            Id = entity.Id,
            PrimeiroNome = entity.PrimeiroNome,
            Sobrenome = entity.Sobrenome,
            DataDeNascimento = entity.DataDeNascimento,
            Nacionalidade = entity.Nacionalidade,
            Sexo = entity.Sexo
        };
    }
}
