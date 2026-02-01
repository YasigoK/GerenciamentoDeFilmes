using CatalogoDeFilmes.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CatalogoDeFilmes.Application.Models;

public class FilmesModel : EntityBase
{
    [DisplayName("Digite o nome")]
    [Required(ErrorMessage = "Campo obrigatório, Nome necessário")]
    public string NomeFilme { get;  set; }

    [DisplayName("Escolha o Genero")]
    [Required(ErrorMessage = "Campo obrigatório, escolha o genero")]
    public string Genero { get;  set; }

    [DisplayName("Digite o diretor")]
    [Required(ErrorMessage = "Campo obrigatório, escolha o diretor")]
    public int DiretorId_Fk { get;  set; }

    [DisplayName("Digite a data de lançamento")]
    [Required(ErrorMessage = "Campo obrigatório, Data de Lançamento requirida")]
    [DataType(DataType.Date)]
    public DateTime DataLancamento { get;  set; }

    [DisplayName("Digite o Tempo de duração")]
    [Required(ErrorMessage = "Campo obrigatório, digite a quantidade de minutos do filme")]
    [DataType(DataType.Currency)]
    public int Duracao { get;  set; }

    [DisplayName("Selecione a imagem")]
    [Required(ErrorMessage = "Campo obrigatório, escolha a capa do filme")]
    public string Imagem { get;  set; }

    [DisplayName("Digite o nome")]
    [Required(ErrorMessage = "Campo obrigatório, digite uma nota entre 1 e 10")]
    [Range(1,10)]
    public decimal Nota { get;  set; }

    public string DiretorNome { get; set; }



    public static FilmesModel Map(FilmesEntity filme)
    {
        if (filme == null)
            return null;

       return new FilmesModel
        {
            Id = filme.Id,
            NomeFilme = filme.NomeFilme,
            Genero = filme.Genero,
            DiretorId_Fk = filme.DiretorId_Fk,
            DataLancamento = filme.DataLancamento,
            Duracao = filme.Duracao,
            Imagem = filme.Imagem,
            Nota = filme.Nota,
            DiretorNome = filme.Diretor?.PrimeiroNome ?? "Não informado" //caso seja passada uma entidade que não tenha um nome de diretor atrelada, evitar o erro Diretor == null 
       };
    }


}
