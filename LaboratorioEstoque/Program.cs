using System;
using System.Security.Cryptography.X509Certificates;

public static class Security
{

  public static double ValidateDoubleInput()
  {
    double num;
    string aux = "";
    bool valid = false;

    do
    {
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
  public int code;
  public string name;
  public double price;
  public int qtd;

  //CONSTRUCTOR

  public void editarProduto() { }

  public void exibirDetalhes()
  {
    Console.Write($"CÓDIGO: {this.code} | ");
    Console.Write($"NOME: {this.name} | ");
    Console.Write($"PREÇO: {this.price} | ");
    Console.WriteLine($"QUANTIDADE: {this.qtd}");
  }

  public void criarProduto()
  {
    string nome;
    double preco;
    int qtd;

    Console.WriteLine("CADASTRAR PRODUTO\n");
    Console.Write("NOME: ");
    nome = Console.ReadLine().ToLower();
    Console.Write("PREÇO: ");
    preco = Security.ValidateDoubleInput();
    Console.Write("QUANTIDADE: ");
    qtd = Security.ValidateIntInput();

    this.name = nome;
    this.price = preco;
    this.qtd = qtd;

  }
}

class Estoque
{

  int MAX;
  public int volume;
  Produto[] produtos;

  public Estoque(int volumeMAX)
  {
    MAX = volumeMAX;
    volume = 0;
    produtos = new Produto[MAX];
  }

  public void cadastrarProduto(Produto produto)
  {
    if (volume > MAX)
    {
      Console.WriteLine("Limite de estoque atingido!");
    }
    else
    {
      produto.code = volume;
      produtos[volume] = produto;
      volume++;
    }

  }

  public void listarProdutos(int modo)
  {
    if (modo == 0)
    {
      for (int i = 0; i < volume; i++)
      {
        produtos[i].exibirDetalhes();
      }
    }
    else
    {
      for (int i = 0; i < volume; i++)
      {
        if (produtos[i].qtd < 10) { produtos[i].exibirDetalhes(); }
      }
    }
  }

  public void deletarProduto()
  {
    bool existe = false;

    Console.Write("Digite o código do produto: ");
    int produtoCodigo = Security.ValidateIntInput();

    for (int i = 0; i < volume; i++)
    {

      if (produtos[i].code == produtoCodigo)
      {
        for (int j = i; j < volume; j++)
        {
          if (j == volume - 1)
          {
            produtos[j] = null;
            existe = true;
          };
          produtos[j] = produtos[j + 1];
        }
        volume--;
        break;
      }
    }

    for (int i = 0; i < volume; i++)
    {
      produtos[i].code = i;
    }

    if (!existe)
    {
      Console.WriteLine("Produto não encontrado!");
    }
  }
  public void editarProduto()
  {
    Console.WriteLine("PRODUTOS");
    for (int i = 0; i < volume; i++)
    {
      Console.WriteLine($"{produtos[i].code} - {produtos[i].name}");
    }

    Console.WriteLine("Selecione o produto que deseja editar: ");
    int produtoCode = Security.ValidateIntInput();
    if (produtoCode < 0 || produtoCode > produtos.Length)
    {
      Console.WriteLine("Este produto não existe");
    }
    else
    {
      produtos[produtoCode].criarProduto();
      Console.WriteLine("Produto editado com sucesso!");
    }
  }


  public void pesquisarProduto(int modo)
  {
    bool existe = false;
    if (modo == 0)
    {
      Console.Write("Digite o nome do produto: ");
      string produtoNome = Console.ReadLine();

      for (int i = 0; i < volume; i++)
      {
        if (produtos[i].name == produtoNome)
        {
          existe = true;
          produtos[i].exibirDetalhes();
        }
      }
    }
    else
    {
      if (modo == 1)
      {
        Console.Write("Digite o código do produto: ");
        int produtoCodigo = Security.ValidateIntInput();

        for (int i = 0; i < volume; i++)
        {
          if (produtos[i].code == produtoCodigo)
          {
            existe = true;
            produtos[i].exibirDetalhes();
          }
        }
      }
    }

    if (!existe)
    {
      Console.WriteLine("Produto não encontrado!");
    }
  }

  public void record()
  {
    int linhas = volume;
    StreamWriter arqW = new StreamWriter("estoque.txt");
    arqW.WriteLine(linhas);

    for (int i = 0; i < linhas; i++)
    {
      arqW.WriteLine(produtos[i].code + ";" + produtos[i].name + ";" + produtos[i].price + ";" + produtos[i].qtd);
    }
    arqW.Close();
  }

  public void read()
  {
    StreamReader arqR = new StreamReader("estoque.txt");
    volume = int.Parse(arqR.ReadLine());
    for (int i = 0; i < volume; i++)
    {
      string linha = arqR.ReadLine();
      string[] dados = linha.Split(";");
      Produto produto = new Produto();
      produto.code = int.Parse(dados[0]);
      produto.name = dados[1];
      produto.price = double.Parse(dados[2]);
      produto.qtd = int.Parse(dados[3]);
      produtos[i] = produto;
    }

    arqR.Close();
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
    estoque.read();

    while (keep)
    {
      //COLETANDO DADOS
      Console.WriteLine("---------------------------------------");
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
          Produto produto = new Produto();
          produto.criarProduto();
          estoque.cadastrarProduto(produto);
          break;

        case "2":
          if (estoque.volume == 0)
          {
            Console.WriteLine("Estoque sem nenhuma produto!");
          }
          else
          {
            Console.WriteLine("LISTAR PRODUTOS");
            estoque.listarProdutos(0);
          }

          break;

        case "3":
          if (estoque.volume == 0)
          {
            Console.WriteLine("Estoque sem nenhuma produto!");
          }
          else
          {
            Console.WriteLine("EDITAR PRODUTO");
            estoque.editarProduto();
          }
          break;

        case "4":
          if (estoque.volume == 0)
          {
            Console.WriteLine("Estoque sem nenhuma produto!");
          }
          else
          {
            Console.WriteLine("PESQUISAR PRODUTO - NOME");
            estoque.pesquisarProduto(0);
          }
          break;

        case "5":
          if (estoque.volume == 0)
          {
            Console.WriteLine("Estoque sem nenhuma produto!");
          }
          else
          {
            Console.WriteLine("PESQUISAR PRODUTO - CÓDIGO");
            estoque.pesquisarProduto(1);
          }
          break;

        case "6":
          if (estoque.volume == 0)
          {
            Console.WriteLine("Estoque sem nenhuma produto!");
          }
          else
          {
            Console.WriteLine("PRODUTOS EM ATENÇÃO");
            estoque.listarProdutos(1);
          }
          break;

        case "7":
          if (estoque.volume == 0)
          {
            Console.WriteLine("Estoque sem nenhuma produto!");
          }
          else
          {
            Console.WriteLine("APAGAR PRODUTO");
            estoque.listarProdutos(0);
            estoque.deletarProduto();
          }
          break;

        case "x":
          keep = false;
          Console.WriteLine("Saindo...");
          estoque.record();
          break;

        default:
          break;
      }
    }

  }
}