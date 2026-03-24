using System.Runtime.CompilerServices;
using poo01.Enumeradores;


namespace poo01.Entidades;

public class Pessoa
{
    // As regions são organizadores para agrupar partes relacionadas
    #region Propriedades Privadas
    private string _nome;
    private string _sobrenome;
    private DateTime _nascimento;
    private string _cpf;
    private EstadoCivil _estadoCivil;
    private Pessoa? _conjuge;

    private readonly List<Pessoa> _filhos = new();

    #endregion

    #region Construtores
    public Pessoa(string nome, string sobrenome, DateTime nascimento, string cpf)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("Nome inválido");
        }

        if (string.IsNullOrWhiteSpace(sobrenome))
        {
            throw new ArgumentException("Sobrenome inválido");
        }

        if (nascimento > DateTime.Now)
        {
            throw new ArgumentException("Data de nascimento inválida");
        }
        if (string.IsNullOrWhiteSpace(cpf))
        {
            throw new ArgumentException("CPF inválido");
        }

        _nome = nome;
        _sobrenome = sobrenome;
        _nascimento = nascimento;
        _cpf = cpf;
        _estadoCivil = EstadoCivil.Solteiro; // Valor padrão
    }

    #endregion
   
    #region Propriedades Públicas
    public string Nome
    {
        get {return _nome;}
    }

    public string Sobrenome
    {
        get {return _sobrenome;}
    }

    public string NomeCompleto
    {
        get { return $"{_nome} {_sobrenome}"; }
    }

    public DateTime Nascimento
    {
        get {return _nascimento;}
    }
    public string Cpf
    {
        get {return _cpf;}
    }

    public EstadoCivil EstadoCivil
    {
        get { return _estadoCivil; }
    }

    public Pessoa? Conjuge
    {
        get { return _conjuge; }
    }

    public IReadOnlyList<Pessoa> Filhos
    {
        get { return _filhos.AsReadOnly(); }
    }
    public int Idade
    {
        get {
            var hoje = DateTime.Today;
            var idade = hoje.Year - _nascimento.Year;

            // Verifica se o aniversário já ocorreu este ano; se não, subtrai um ano da idade
            if (_nascimento.Date > hoje.AddYears(-idade)) idade--;

            return idade;
        }
     
    }
    #endregion

    #region Métodos
    public void Casar(Pessoa conjuge, bool adicionarSorbrenome)
    {
        // Verifica se o conjugê é nulo
        if (conjuge == null)
            throw new ArgumentNullException("Cônjuge inválido");

        // Verifica se o conjugê é a mesma pessoa
        if (ReferenceEquals(this, conjuge))
            throw new ArgumentException("Não é possível casar consigo mesmo");

        // Verifica se a pessoa já é casada
        if (_estadoCivil  == EstadoCivil.Casado)
            throw new InvalidOperationException($"{_nome} já está casada");

        // Verifica se o conjugê já é casado
        if (conjuge._estadoCivil == EstadoCivil.Casado)
            throw new InvalidOperationException($"{conjuge._nome} já está casada");

        // Define o estado civil como casado
        _estadoCivil = EstadoCivil.Casado;
        // Associa o atributo _conjuge ao objeto passado por parâmetro
        _conjuge = conjuge;

        // Define o estado civil do conjugêcomo casado
        conjuge._estadoCivil = EstadoCivil.Casado;
        // Associa o atributo _conjuge ao objeto passado por parâmetro
        conjuge._conjuge = this;

        // Verifica se deve adicionar o sobrenome do conjugê
        if (adicionarSorbrenome)
           conjuge._sobrenome = $"{conjuge._sobrenome} {_sobrenome}";

    }


    public void Divorciar()
    {
        // Verifica se a pessoa é casada
        if (_estadoCivil != EstadoCivil.Casado)
            throw new InvalidOperationException($"{_nome} não está casada");

        // Verifica se o conjugê é nulo
        if (_conjuge == null)
            throw new InvalidOperationException("Cônjuge inválido");

        // Define o estado civil como divorciado
        _estadoCivil = EstadoCivil.Divorciado;
        // Define o estado civil do conjugê como divorciado
        _conjuge._estadoCivil = EstadoCivil.Divorciado;

        // Desassocia o atributo conjugê ao objeto passado por parâmetro
        _conjuge._conjuge = null;
        _conjuge = null;

    }

    public Pessoa GerarFilho(string nome, string sobrenome, DateTime nascimento, string cpf)
    {
        // Verifica se a pessoa é casada
        if (_estadoCivil != EstadoCivil.Casado || _conjuge == null )
            throw new InvalidOperationException("Só é possível gerar um filho estando casado.");

        // Cria um novo objeto do tipo Pessoa representando o filho
        var filho = new Pessoa(nome, sobrenome, nascimento, cpf);

        // Adiciona o filho à lista de filhos da pessoa e do conjugê
        _filhos.Add(filho);
        _conjuge._filhos.Add(filho);

        return filho;
    }

    
    #endregion
}