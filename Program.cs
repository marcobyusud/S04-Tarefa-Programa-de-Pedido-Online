using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

Random random = new Random();
Cliente clienteEua = CriarClienteAleatorio(random, true);
Cliente clienteInternacional = CriarClienteAleatorio(random, false);

Pedido pedidoEua = CriarPedidoAleatorio(random, clienteEua);
Pedido pedidoInternacional = CriarPedidoAleatorio(random, clienteInternacional);

Console.WriteLine("Pedido nos EUA");
MostrarPedido(pedidoEua);
Console.WriteLine("Pedido internacional");
MostrarPedido(pedidoInternacional);

static Cliente CriarClienteAleatorio(Random random, bool estaNosEua)
{
    string[] primeirosNomes = { "Alex", "Beatriz", "Carlos", "Diana", "Eduardo", "Fernanda", "Gabriel", "Helena", "Isabela", "Lucas", "Marina", "Rafael" };
    string[] sobrenomes = { "Almeida", "Costa", "Ferreira", "Gomes", "Lima", "Martins", "Oliveira", "Pereira", "Rocha", "Santos", "Silva", "Souza" };
    string[] ruas = { "Oak Street", "Maple Avenue", "Pine Road", "Cedar Lane", "Lakeview Drive", "Hillcrest Road", "Park Avenue", "River Street" };
    string nome = $"{primeirosNomes[random.Next(primeirosNomes.Length)]} {sobrenomes[random.Next(sobrenomes.Length)]}";
    string rua = $"{random.Next(10, 9999)} {ruas[random.Next(ruas.Length)]}";

    if (estaNosEua)
    {
        (string Cidade, string Estado)[] cidadesEua =
        {
            ("Seattle", "Washington"), ("Austin", "Texas"), ("Denver", "Colorado"),
            ("Miami", "Florida"), ("Boston", "Massachusetts"), ("Portland", "Oregon")
        };
        (string Cidade, string Estado) cidade = cidadesEua[random.Next(cidadesEua.Length)];
        Endereco enderecoEua = new Endereco(rua, cidade.Cidade, cidade.Estado, "USA");
        return new Cliente(nome, enderecoEua);
    }

    (string Cidade, string Estado, string Pais)[] locaisInternacionais =
    {
        ("Lisboa", "Lisboa", "Portugal"), ("Porto", "Porto", "Portugal"),
        ("Toronto", "Ontario", "Canada"), ("Vancouver", "British Columbia", "Canada"),
        ("Sydney", "New South Wales", "Australia"), ("Paris", "Ile-de-France", "France"),
        ("Tokyo", "Tokyo", "Japan"), ("Sao Paulo", "Sao Paulo", "Brazil")
    };
    (string Cidade, string Estado, string Pais) local = locaisInternacionais[random.Next(locaisInternacionais.Length)];
    Endereco enderecoInternacional = new Endereco(rua, local.Cidade, local.Estado, local.Pais);
    return new Cliente(nome, enderecoInternacional);
}

static Pedido CriarPedidoAleatorio(Random random, Cliente cliente)
{
    List<string> produtosDisponiveis = new List<string>
    {
        "Caderno", "Caneta", "Mochila", "Garrafa", "Estojo", "Fone de ouvido",
        "Teclado", "Mouse", "Carregador", "Camiseta", "Livro", "Luminaria",
        "Carteira", "Oculos de sol", "Caixa de som"
    };

    Pedido pedido = new Pedido(cliente);
    int quantidadeDeProdutos = random.Next(2, 4);

    for (int indice = 0; indice < quantidadeDeProdutos; indice++)
    {
        int indiceProduto = random.Next(produtosDisponiveis.Count);
        string nomeProduto = produtosDisponiveis[indiceProduto];
        produtosDisponiveis.RemoveAt(indiceProduto);

        string identificacao = $"{nomeProduto.Substring(0, 3).ToUpperInvariant()}-{random.Next(1000, 10000)}";
        decimal preco = random.Next(199, 30000) / 100m;
        int quantidade = random.Next(1, 8);
        pedido.AdicionarProduto(new Produto(nomeProduto, identificacao, preco, quantidade));
    }

    return pedido;
}

static void MostrarPedido(Pedido pedido)
{
    Console.WriteLine(pedido.ObterEtiquetaDeEmbalagem());
    Console.WriteLine(pedido.ObterEtiquetaDeEnvio());
    Console.WriteLine($"Total do pedido: {pedido.ObterCustoTotal().ToString("C", CultureInfo.GetCultureInfo("en-US"))}");
    Console.WriteLine();
}

public class Produto
{
    private string _nome;
    private string _identificacao;
    private decimal _preco;
    private int _quantidade;

    public Produto(string nome, string identificacao, decimal preco, int quantidade)
    {
        _nome = nome;
        _identificacao = identificacao;
        _preco = preco;
        _quantidade = quantidade;
    }

    public string ObterNome() => _nome;
    public void DefinirNome(string nome) => _nome = nome;

    public string ObterIdentificacao() => _identificacao;
    public void DefinirIdentificacao(string identificacao) => _identificacao = identificacao;

    public decimal ObterPreco() => _preco;
    public void DefinirPreco(decimal preco) => _preco = preco;

    public int ObterQuantidade() => _quantidade;
    public void DefinirQuantidade(int quantidade) => _quantidade = quantidade;

    public decimal ObterCustoTotal()
    {
        return _preco * _quantidade;
    }
}

public class Cliente
{
    private string _nome;
    private Endereco _endereco;

    public Cliente(string nome, Endereco endereco)
    {
        _nome = nome;
        _endereco = endereco;
    }

    public string ObterNome() => _nome;
    public void DefinirNome(string nome) => _nome = nome;

    public Endereco ObterEndereco() => _endereco;
    public void DefinirEndereco(Endereco endereco) => _endereco = endereco;

    public bool EstaNosEua()
    {
        return _endereco.EstaNosEua();
    }
}

public class Endereco
{
    private string _rua;
    private string _cidade;
    private string _estado;
    private string _pais;

    public Endereco(string rua, string cidade, string estado, string pais)
    {
        _rua = rua;
        _cidade = cidade;
        _estado = estado;
        _pais = pais;
    }

    public string ObterRua() => _rua;
    public void DefinirRua(string rua) => _rua = rua;

    public string ObterCidade() => _cidade;
    public void DefinirCidade(string cidade) => _cidade = cidade;

    public string ObterEstado() => _estado;
    public void DefinirEstado(string estado) => _estado = estado;

    public string ObterPais() => _pais;
    public void DefinirPais(string pais) => _pais = pais;

    public bool EstaNosEua()
    {
        return _pais.Equals("USA", StringComparison.OrdinalIgnoreCase)
            || _pais.Equals("United States", StringComparison.OrdinalIgnoreCase)
            || _pais.Equals("EUA", StringComparison.OrdinalIgnoreCase);
    }

    public string ObterEnderecoCompleto()
    {
        var endereco = new StringBuilder();
        endereco.AppendLine(_rua);
        endereco.AppendLine($"{_cidade}, {_estado}");
        endereco.Append(_pais);
        return endereco.ToString();
    }
}

public class Pedido
{
    private List<Produto> _produtos;
    private Cliente _cliente;

    public Pedido(Cliente cliente)
    {
        _cliente = cliente;
        _produtos = new List<Produto>();
    }

    public Cliente ObterCliente() => _cliente;
    public void DefinirCliente(Cliente cliente) => _cliente = cliente;

    public void AdicionarProduto(Produto produto)
    {
        _produtos.Add(produto);
    }

    public decimal ObterCustoTotal()
    {
        decimal total = 0;
        foreach (Produto produto in _produtos)
        {
            total += produto.ObterCustoTotal();
        }

        decimal frete = _cliente.EstaNosEua() ? 5 : 35;
        return total + frete;
    }

    public string ObterEtiquetaDeEmbalagem()
    {
        var etiqueta = new StringBuilder("Etiqueta de embalagem\n");
        foreach (Produto produto in _produtos)
        {
            etiqueta.AppendLine($"{produto.ObterNome()} - ID: {produto.ObterIdentificacao()}");
        }

        return etiqueta.ToString();
    }

    public string ObterEtiquetaDeEnvio()
    {
        return $"Etiqueta de envio\n{_cliente.ObterNome()}\n{_cliente.ObterEndereco().ObterEnderecoCompleto()}";
    }
}