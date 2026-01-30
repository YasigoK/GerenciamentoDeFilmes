namespace CatalogoDeFilmes.Domain.Entities;

public class FilmesEntity : EntityBase
{
    public string NomeFilme { get; protected set; }
    public string Genero { get; protected set; }
    public int DiretorId_Fk { get; protected set; }
    public DateTime DataLancamento { get; protected set; }
    public int Duracao { get; protected set; }
    public string Imagem { get; protected set;  }
    public decimal Nota { get; protected set; }
    public virtual DiretoresEntity Diretor { get; set; } // vai criar um objetodo tipo diretorEntity e vai permitir q o filme saiba quem é pai
    public FilmesEntity()
    {

    }
    public FilmesEntity( string nomeFilme, string genero, int diretor, DateTime datal, int dur,string imagem, decimal nota)
    {
        NomeFilme = nomeFilme;
        Genero = genero;
        DiretorId_Fk = diretor;
        DataLancamento = datal;
        Duracao = dur;
        Imagem = imagem;
        Nota = nota;
    }
    public void AtulizarFilme( string nomeFilme, string genero, int diretor, DateTime datal, int dur,string imagem, decimal nota)
    {
        NomeFilme = nomeFilme;
        Genero = genero;
        DiretorId_Fk = diretor;
        DataLancamento = datal;
        Duracao = dur;
        Imagem = imagem;
        Nota = nota;
    }
}
