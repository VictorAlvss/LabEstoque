using System;

public static class Security
{

  public static double ValidateDoubleInput()
  {
    double num;
    string aux = "";
    bool valid = false;

    do
    {
      Console.Write("Digite um número: ");
      aux = Console.ReadLine();

      if (double.TryParse(aux, out num) && num >= 0)
      {
        valid = true;
      }
      else
      {
        Console.WriteLine("Entrada inválida!");
      }
    } while (!valid);

    return num;
  }

  public static int ValidateIntInput()
  {
    int num;
    string aux = "";
    bool valid = false;

    do
    {
      Console.Write("Digite um número: ");
      aux = Console.ReadLine();

      if (int.TryParse(aux, out num) && num >= 0)
      {
        valid = true;
      }
      else
      {
        Console.WriteLine("Entrada inválida!");
      }
    } while (!valid);

    return num;
  }


  public static char ValidateOptions(char[] validOptions)
  {
    bool valid = false;
    char op;

    do
    {
      Console.Write("Digite sua escolha: ");
      op = char.Parse(Console.ReadLine().ToLower());
      if (validOptions.Contains(op))
      {
        valid = true;
      }
    } while (!valid);

    return op;
  }
}

class Produto
{

  //PROPERTIES
  private int code;
  private string name;
  public double price;
  public int qtd;

  //CONSTRUCTOR
  public Produto(int code, string name)
  {

  }

  public void editarProduto() { }

  public void exibirDetalhes(Produto produto)
  {
    Console.WriteLine($"CÓDIGO: {produto.code}");
    Console.WriteLine($"NOME: {produto.name}");
    Console.WriteLine($"PREÇO: {produto.price}");
    Console.WriteLine($"QUANTIDADE: {produto.qtd}");
  }
}

class Estoque
{

  int MAX;
  int volume;
  Produto[] produtos;

  public Estoque(int volumeMAX)
  {
    MAX = volumeMAX;
    volume = 0;
    produtos = new Produto[MAX];
  }

  public Produto cadastrarProduto()
  {

  }

}

class Program
{

  public static void Main(string[] args)
  {
    //VARIABLES
    char[] menuOptions = ['1', '2', '3', '4', '5', '6', '7', 'x'];
    string op;
    bool keep = true;

    //ESTOQUE
    Estoque estoque = new Estoque(15);

    while (keep)
    {
      //COLETANDO DADOS
      Console.WriteLine("CONTROLE DE ESTOQUE");
      Console.WriteLine("Selecione a opção desejada");
      Console.WriteLine("1 - Cadastrar um novo produto\n2 - Listar todos os produtos cadastrados\n3 - Editar dados de um produto\n4 - Pesquisar um produto por nome\n5 - Pesquisar um produto por código\n6 - Listar todos os produtos com estoque inferior a 10\n7 - Apagar um produto\n");
      Console.WriteLine("x - Sair");
      Console.WriteLine("------------- SELECIONE ---------------");

      //Validando escolha
      op = Security.ValidateOptions(menuOptions).ToString();


      //Escolhendo operação
      switch (op)
      {
        case "1":
          Console.WriteLine("CADASTRAR PRODUTO");


          break;

        case "2":
          Console.WriteLine("LISTANDO PRODUTOS");


          break;

        case "3":
          Console.WriteLine("PESQUISAR PRODUTO - NOME");


          break;

        case "4":
          Console.WriteLine("PESQUISAR PRODUTO - CÓDIGO");

          break;

        case "5":
          Console.WriteLine("CADASTRANDO PRODUTO");

          break;

        case "6":
          Console.WriteLine("CADASTRANDO PRODUTO");
          break;

        case "7":
          Console.WriteLine("APAGAR PRODUTO");

          break;

        case "x":
          keep = false;
          Console.WriteLine("Saindo...");
          break;

        default:
          break;
      }
    }

  }


}