namespace CatalogoDeFilmes.Domain.Entities;

public class DiretoresEntity:EntityBase
{
    public string PrimeiroNome { get; protected set; }
    public string Sobrenome { get; protected set; }
    public DateTime DataDeNascimento { get; protected set; }
    public string Nacionalidade { get; protected set; }
    public char Sexo { get; protected set; }

    public DiretoresEntity()
    {
        
    }
    public DiretoresEntity( string nome1,string nome2, DateTime nascimento, string nacionalidade, char sexo) 
    {
        PrimeiroNome = nome1;
        Sobrenome = nome2;
        DataDeNascimento = nascimento; 
        Nacionalidade = nacionalidade;
        Sexo = sexo;
    }
}
