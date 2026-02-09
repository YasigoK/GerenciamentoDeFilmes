using CatalogoDeFilmes.Domain.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CatalogoDeFilmes.Application.Models;

public class FilmesModel : EntityBase
{
    [DisplayName("Digite o nome")]
    [MaxLength(150, ErrorMessage = "Entre com um nome de até 150 caracteres")]
    [Required(ErrorMessage = "Campo obrigatório, digite o nome do Filme")]
    public string NomeFilme { get; set; } 

    [DisplayName("Escolha o Genero")]
    [MaxLength(50, ErrorMessage = "Entre com um nome de até 50 caracteres")]
    [Required(ErrorMessage = "Campo obrigatório, escreva o genero do filme")]
    public string Genero { get;  set; }

    [DisplayName("Digite o diretor")]
    [Required(ErrorMessage = "Campo obrigatório, escolha o diretor")]
    public int DiretorId_Fk { get;  set; }

    [DisplayName("Digite a data de lançamento")]
    [Required(ErrorMessage = "Campo obrigatório, data de Lançamento requirida")]
    [DataType(DataType.Date)]
    public DateTime DataLancamento { get;  set; }

    [DisplayName("Digite o Tempo de duração")]
    [Required(ErrorMessage = "Campo obrigatório, digite a quantidade de minutos do filme")]
    [Range(0, int.MaxValue, ErrorMessage = "Insira um número inteiro válido.")]
    public int Duracao { get;  set; }

    [DisplayName("Selecione a imagem")]
    [Required(ErrorMessage = "Campo obrigatório, envie a imagem da capa do filme")]
    public string Imagem { get;  set; }

    [DisplayName("Digite o nome")]
    [Required(ErrorMessage = "Campo obrigatório, selecione a nota do filme")]
    [Range(1.0,10.0 ,ErrorMessage = "Digite uma nota entre 1 e 10")]
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
            DiretorNome = filme.Diretor?.PrimeiroNome + " " + filme.Diretor?.Sobrenome ?? "Não informado" //caso seja passada uma entidade que não tenha um nome de diretor atrelada, evitar o erro Diretor == null 
       };
    }


}



