using System.Globalization;
using System.Reflection.Metadata;
using poo01.Entidades;
using poo01.Enumeradores;

List<Pessoa> pessoas = new();
bool executando = true; 

#region Metodos


void exibirInformacoes(Pessoa pessoa)
{
    Console.WriteLine("====================================");
    Console.WriteLine("Informações da Pessoa:");
    Console.WriteLine($"Nome: {pessoa.NomeCompleto}");
    Console.WriteLine($"Data de Nascimento: {pessoa.Nascimento.ToString("dd/MM/yyyy")}");
    Console.WriteLine($"CPF: {pessoa.Cpf}");
    Console.WriteLine($"Idade: {pessoa.Idade} anos");
    Console.WriteLine($"Estado Civil: {pessoa.EstadoCivil}");

    if (pessoa.EstadoCivil == EstadoCivil.Casado)
        Console.WriteLine($"Cônjuge: {pessoa.Conjuge?.NomeCompleto}");

    // Exibe a lista de filhos
    if (pessoa.Filhos.Count > 0)
    {
        Console.WriteLine("Filhos:");

        foreach (var filho in pessoa.Filhos)
        {
            Console.WriteLine($"- {filho.NomeCompleto} - {filho} (Nascimento: {filho.Nascimento.ToString("dd/MM/yyyy")})");
        }
    }
    Console.WriteLine("====================================");
}

void CriarPessoa()
{
    Console.Clear();
    Console.WriteLine("Criar nova pessoa");

    Console.Write("Nome: ");   
    var nome = Console.ReadLine();

    Console.Write("Sobrenome: ");   
    var sobrenome = Console.ReadLine();


    Console.Write("Data nascimento (dd/mm/yyyy): ");   
    var nascimento = Console.ReadLine();

    Console.Write("CPF: ");   
    var cpf = Console.ReadLine();

    try
    {
        var data = DateTime.ParseExact(nascimento!, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        var pessoa = new Pessoa(nome!, sobrenome!, data, cpf!);
        pessoas.Add(pessoa);
        Console.WriteLine("Pessoa criada com sucesso!");
        
    } catch (Exception ex)
    {
        Console.WriteLine($"Erro ao criar pessoa: {ex.Message}");
    }

    Pausar();
}

void ListarPessoasDetalhado()
{
    Console.Clear();
    Console.WriteLine("-----Pessoas-----");

    if (!pessoas.Any())
    {
        Console.WriteLine("Nenhuma pessoa cadastrada.");
    }
    else
    {
        for  (int i = 0; i < pessoas.Count; i++)
        {
            var p = pessoas[i];
            Console.WriteLine($"[{i}] - {p.NomeCompleto} - {p.Idade} - {p.EstadoCivil}");
        }
    }
    
    Pausar();
}

void VisualizarPessoa()
{
    Console.Clear();
    Console.WriteLine("-----Visualizar Pessoa-----");

    if (!pessoas.Any())
    {
        Console.WriteLine("Nenhuma pessoa cadastrada.");
        Pausar();
        return;
    }
    ListarPessoasSimples();
    Console.Write("\nEscolha o índice da pessoa ");

    if (int.TryParse(Console.ReadLine(), out int indice))
    {
      if (indice >= 0 && indice < pessoas.Count)
        {
            var pessoa = pessoas[indice];
            exibirInformacoes(pessoa);
        }
        else
        {
            Console.WriteLine("Índice inválido. Retornando ao menu.");
        }
    }
    else
    {
        Console.WriteLine("Entrada inválida. Retornando ao menu.");
    }
    
    Pausar();
}

void ListarPessoasSimples()
{
   for (int i = 0; i < pessoas.Count; i++)
    {
        Console.WriteLine($"[{i}] - {pessoas[i].NomeCompleto} {pessoas[i].Idade} - {pessoas[i].EstadoCivil}");
    }
}

void Pausar()
{
    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();
}

void CasarPessoas()
{
    Console.Clear();
    Console.WriteLine("-----Casamento-----");
    ListarPessoasSimples();

    Console.Write("\nEscolha o índice da primeira pessoa: ");
    int i1 = int.Parse(Console.ReadLine()!);
    
    Console.Write("\nEscolha o índice da segunda pessoa: ");
    int i2 = int.Parse(Console.ReadLine()!);

    Console.Write("\nDeseja adicionar o sobrenome do cônjuge? (s/n): ");
    bool adicionarSobrenome = Console.ReadLine()?.ToLower() == "s";

    try
    {
        pessoas[i1].Casar(pessoas[i2], adicionarSobrenome);
        Console.WriteLine("Pessoas casadas com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao casar pessoas: {ex.Message}");
    }
    Pausar();
}

void DivorciarPessoas()
{
    Console.Clear();
    Console.WriteLine("-----Divórcio-----");
    ListarPessoasSimples();

    Console.Write("\nEscolha o índice da pessoa: ");
    int indice = int.Parse(Console.ReadLine()!);

    try
    {
        pessoas[indice].Divorciar();
        Console.WriteLine("Pessoa divorciada com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao divorciar pessoa: {ex.Message}");
    }
    Pausar();
}

void GerarFilho()
{
    Console.Clear();
    Console.WriteLine("-----Gerar Filho-----");
    ListarPessoasSimples();

    Console.Write("\nEscolha o índice do pai/mãe: ");
    int indice = int.Parse(Console.ReadLine()!);

    Console.Write("\nNome do filho: ");
    var nome = Console.ReadLine()!;

    Console.Write("\nSobrenome do filho: ");
    var sobrenome = Console.ReadLine()!;

    Console.Write("\nData de nascimento do filho (dd/MM/yyyy): ");
    var dataTexto = Console.ReadLine()!;

    Console.Write("\nCPF do filho: ");
    var cpf = Console.ReadLine()!;

    try
    {
        var data = DateTime.ParseExact(dataTexto!, "dd/MM/yyyy", CultureInfo.InvariantCulture); 

        var filho = pessoas[indice].GerarFilho(nome!, sobrenome!, data, cpf!);
        pessoas.Add(filho);
        Console.WriteLine("Filho gerado com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao gerar filho: {ex.Message}");
    }
    Pausar();
}






#endregion

while (executando)
{
    Console.Clear();
    Console.WriteLine("-----Menu-----");
    Console.WriteLine("1 - Criar pessoa");
    Console.WriteLine("2 - Listar pessoas ");
    Console.WriteLine("3 - Visualizar pessoa");
    Console.WriteLine("4 - Casar pessoas");
    Console.WriteLine("5 - Divorciar pessoas");
    Console.WriteLine("6 - Gerar filho");
    Console.WriteLine("0 - Sair");
    Console.Write("\nEscolha : ");
    
    var opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            CriarPessoa();
            break;
        case "2":
            ListarPessoasDetalhado();
            break;
        case "3":
            VisualizarPessoa();
            break;
        case "4":
            CasarPessoas();
            break;
        case "5":
            DivorciarPessoas();
            break;
        case "6":
            GerarFilho();
            break;
        case "0":
            executando = false;
            break;
        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            Pausar();
            break;
    }

}