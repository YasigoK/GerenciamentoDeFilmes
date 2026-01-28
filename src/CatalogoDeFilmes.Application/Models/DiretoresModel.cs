using CatalogoDeFilmes.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace CatalogoDeFilmes.Application.Models;

public class DiretoresModel : EntityBase
{

    [Display(Name="Digite o nome")]
    [Required(ErrorMessage ="Campo obrigatório, Nome necessário")]
    public string PrimeiroNome { get;  set; }

    [Display(Name ="Digite o Sobrenome")]
    [Required(ErrorMessage ="Campo obigatórios, Sobrenome necessário")]
    public string Sobrenome { get;  set; }

    [Display(Name = "Digite a data de nascimento ")]
    [Required(ErrorMessage = "Campo obigatórios, Data de nascimento obrigatória")]
    public DateTime DataDeNascimento { get;  set; }

    [Display(Name = "Digite a nacionalidade ")]
    [Required(ErrorMessage = "Campo obigatórios, digite o pais de origem.")]
    public string Nacionalidade { get;  set; }

    [Display(Name = "Digite o sexo")]
    [Required(ErrorMessage = "Campo obigatórios, digite o sexo")]
    public char Sexo { get;  set; }

    public static DiretoresModel Map(DiretoresEntity entity)
    {
        if (entity == null)
            return null;

        return new DiretoresModel
        {
            PrimeiroNome = entity.PrimeiroNome,
            Sobrenome = entity.Sobrenome,
            DataDeNascimento = entity.DataDeNascimento,
            Nacionalidade = entity.Nacionalidade,
            Sexo = entity.Sexo
        };
    }
}
