namespace CatalogoDeFilmes.Domain.Entities;

public class FilmesEntity : EntityBase
{
    public string NomeFilme { get; protected set; }
    public string Genero { get; protected set; }
    public int DiretorId_fk { get; protected set; }
    public DateTime DataLancamento { get; protected set; }
    public int Duracao { get; protected set; }
    public string Imagem { get; protected set;  }
    public decimal Nota { get; protected set; }

    public FilmesEntity( string nomeFilme, string genero, int diretor, DateTime datal, int dur,string imagem, decimal nota)
    {
        NomeFilme = nomeFilme;
        Genero = genero;
        DiretorId_fk = diretor;
        DataLancamento = datal;
        Duracao = dur;
        Imagem = imagem;
        Nota = nota;

    }
}
