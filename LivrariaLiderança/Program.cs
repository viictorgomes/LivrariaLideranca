using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Livraria_Liderança
{
    //Estruturas das classes serializáveis

    #region EstruturaBDClientes
    [XmlType("Cliente")]
    public class Pessoa
    {
        [XmlAttribute("ID", DataType = "string")]
        public string ID { get; set; }

        [XmlElement("NOME")]
        public string Nome { get; set; }
        [XmlElement("CPF")]
        public string CPF { get; set; }
        [XmlElement("TEL")]
        public string Telefone { get; set; }
        [XmlElement("CIDADE")]
        public string Cidade { get; set; }
        [XmlElement("EMAIL")]
        public string Email { get; set; }

        public Pessoa() { }

        public Pessoa(string id, string nome, string cpf, string tel, string cidade, string email)
        {
            this.ID = id;
            this.Nome = nome;
            this.CPF = cpf;
            this.Telefone = tel;
            this.Cidade = cidade;
            this.Email = email;
        }
    }

    [XmlRoot("BancoDadosClientes")]
    [XmlInclude(typeof(Pessoa))]
    public class ListaPessoas
    {
        [XmlArray("Clientes")]
        [XmlArrayItem("Cliente")]
        public List<Pessoa> Pessoas = new List<Pessoa>();
        public ListaPessoas() { }

        public void AdicionarCliente(Pessoa pessoa)
        {
            Pessoas.Add(pessoa);
        }
    }
    #endregion

    #region EstruturaBDProdutos
    public class Produto
    {
        [XmlAttribute("ID", DataType = "string")]
        public string ID { get; set; }

        [XmlElement("TIPO")]
        public string Categoria { get; set; }

        [XmlElement("NOME")]
        public string Nome { get; set; }

        [XmlElement("PRECO")]
        public string Preço { get; set; }

        [XmlElement("ESTOQUE")]
        public string Estoque { get; set; }


        public Produto() { }

        public Produto(string id, string categoria, string nome, string preço, string estoque)
        {
            this.ID = id;
            this.Categoria = categoria;
            this.Nome = nome;
            this.Preço = preço;
            this.Estoque = estoque;
        }
    }

    [XmlRoot("BancoDadosProdutos")]
    [XmlInclude(typeof(Produto))]
    public class ListaProdutos
    {
        [XmlArray("Produtos")]
        [XmlArrayItem("Produto")]
        public List<Produto> Produtos = new List<Produto>();
        public ListaProdutos() { }

        public void AdicionarProduto(Produto produto)
        {
            Produtos.Add(produto);
        }
    }
    #endregion

    #region EstruturaDBRelatorio
    public class Relatorio
    {
        [XmlAttribute("CPF", DataType = "string")]
        public string CPF { get; set; }

        [XmlElement("NOME")]
        public string NomeCliente { get; set; }

        [XmlElement("NOMEPRODUTO")]
        public string NomeProduto { get; set; }

        [XmlElement("QUANTIDADE")]
        public string Quantidade { get; set; }

        [XmlElement("PRECO")]
        public string Preço { get; set; }

        [XmlElement("DATA")]
        public string Data { get; set; }

        public Relatorio() { }

        public Relatorio(string cpf, string nomecliente, string nomeproduto, string quantidade, string preço, string data)
        {
            this.CPF = cpf;
            this.NomeCliente = nomecliente;
            this.NomeProduto = nomeproduto;
            this.Quantidade = quantidade;
            this.Preço = preço;
            this.Data = data;
        }
    }

    [XmlRoot("BancoDadosRelatorio")]
    [XmlInclude(typeof(Relatorio))]
    public class ListaRelatorios
    {
        [XmlArray("Relatorios")]
        [XmlArrayItem("Cliente")]
        public List<Relatorio> Relatorios = new List<Relatorio>();
        public ListaRelatorios() { }

        public void RegistrarRelatorio(Relatorio relatorio)
        {
            Relatorios.Add(relatorio);
        }
    }
    #endregion

    //Métodos utilizados na interface gráfica
    #region EstruturaDesenhar

    public class Desenhar
    {
        // Efeito giratorio para carregamentos
        static int contador = 0;
        static string[] efeito = new string[] { "/", "-", "\\", "|" };
        public static void EfeitoGirar()
        {
            contador++;

            //manter o loop dentro do tamanho da string
            if (contador >= efeito.Length)
                contador = 0;

            Console.Write(efeito[contador]);
            //voltar a posição do cursor pra escrever o proximo simbolo em cima do ultimo
            //remover a necessidade de usar Clear e escrever de novo
            Console.SetCursorPosition(Console.CursorLeft - efeito[contador].Length, Console.CursorTop);
        }

        //Métodos de criação de retangulos
        //tamanho, posições, cores e diferentes tipos de retangulos por parametros
        public static void Retangulo(int largura, int altura, int posX = 0, int posY = 0, ConsoleColor? cor = null, int tipo = 0)
        {
            DesenharRetangulo(largura, altura, posX, posY, cor, tipo);
        }

        public static void DesenharRetangulo(int largura, int altura, int posX = 0, int posY = 0, ConsoleColor? cor = null, int tipo = 0)
        {
            if (largura < 1 || altura < 1) return;

            //Salvar posição do cursor antes de desenhar
            int CursorY = Console.CursorTop;
            int CursorX = Console.CursorLeft;

            // Salvar foreground color antes de desenhar
            ConsoleColor salvarcor = Console.ForegroundColor;

            if (cor.HasValue)
            {
                Console.ForegroundColor = cor.Value;
            }

            char cimaesquerda, horizontal, cimadireita, vertical, baixoesquerda, baixodireita;

            switch (tipo)
            {
                case 1:
                    cimaesquerda = '╔'; horizontal = '═'; cimadireita = '╗'; vertical = '║'; baixoesquerda = '╚'; baixodireita = '╝';
                    break;

                case 2:
                    cimaesquerda = '█'; horizontal = '█'; cimadireita = '█'; vertical = '█'; baixoesquerda = '█'; baixodireita = '█';
                    break;

                case 3:
                    cimaesquerda = '■'; horizontal = '■'; cimadireita = '■'; vertical = '■'; baixoesquerda = '■'; baixodireita = '■';
                    break;

                case 4:
                    cimaesquerda = '├'; horizontal = '─'; cimadireita = '┤'; vertical = '│'; baixoesquerda = '├'; baixodireita = '┤';
                    break;

                case 5:
                    cimaesquerda = '┼'; horizontal = '─'; cimadireita = '┐'; vertical = '│'; baixoesquerda = '┼'; baixodireita = '┤';
                    break;

                case 6:
                    cimaesquerda = '┼'; horizontal = '─'; cimadireita = '┤'; vertical = '│'; baixoesquerda = '┼'; baixodireita = '┤';
                    break;

                case 7:
                    cimaesquerda = '┼'; horizontal = '─'; cimadireita = '┤'; vertical = '│'; baixoesquerda = '┼'; baixodireita = '┘';
                    break;
                case 8:
                    cimaesquerda = '┬'; horizontal = '─'; cimadireita = '┬'; vertical = '│'; baixoesquerda = '┴'; baixodireita = '┴';
                    break;
                case 9:
                    cimaesquerda = '┬'; horizontal = '─'; cimadireita = '┐'; vertical = '│'; baixoesquerda = '┴'; baixodireita = '┘';
                    break;
                case 10:
                    cimaesquerda = '┌'; horizontal = '─'; cimadireita = '┐'; vertical = '│'; baixoesquerda = '├'; baixodireita = '┤';
                    break;
                default:
                    cimaesquerda = '┌'; horizontal = '─'; cimadireita = '┐'; vertical = '│'; baixoesquerda = '└'; baixodireita = '┘';
                    break;
            }

            //Desenha o simbolo da esquerda em cima (primeiro)
            Desenha(posX, posY, cimaesquerda);

            //parte horizontal de cima
            for (int x = posX + 1; x < posX + largura; x++)
            {
                Desenha(x, posY, horizontal);
            }

            //simbolo de cima na direita
            Desenha(posX + largura, posY, cimadireita);

            //parte vertical esquerda, calcular largura e desenha parte vertical direita
            for (int y = posY + altura; y > posY; y--)
            {
                Desenha(posX, y, vertical);
                Desenha(posX + largura, y, vertical);
            }

            //simbolo de baixo na esquerda
            Desenha(posX, posY + altura + 1, baixoesquerda);

            //parte horizontal de baixo
            for (int x = posX + 1; x < posX + largura; x++)
            {
                Desenha(x, posY + altura + 1, horizontal);
            }

            //simbolo de baixo na direita
            Desenha(posX + largura, posY + altura + 1, baixodireita);

            // Retornar cursor a posição antes do desenho
            //Console.SetCursorPosition(CursorX+1, CursorY+1);

            // Retornar cor antiga do foreground color caso tenha sido alterado para desenhar
            if (cor.HasValue)
            {
                Console.ForegroundColor = salvarcor;
            }
        }

        //desenhar a partir de determinadas posições utilizando de simbolos de texto
        private static void Desenha(int posX, int posY, char caractere)
        {
            //conferir se os valores não estouram
            if (posX < Console.BufferWidth && posY < Console.BufferHeight)
            {
                Console.SetCursorPosition(posX, posY);
                Console.Write(caractere);
            }
        }

        //mesmo quadrado usado em vários métodos
        public static void QuadradoDefault()
        {
            Desenhar.Retangulo(55, 34, 42, 2, ConsoleColor.Gray, 0);
        }
    }
    #endregion

    class Program
    {
        static Random rand = new Random();
        static ListaPessoas _Pessoas = new ListaPessoas();
        static ListaProdutos _Produtos = new ListaProdutos();
        static ListaRelatorios _Relatorios = new ListaRelatorios();
        static double precoTotal = 0;
        static string admin = "admin", adminsenha = "12345";

        #region GerenciarClientes
        static void CadastrarCliente(Pessoa cliente)
        {
            _Pessoas.AdicionarCliente(cliente);
        }

        static void RemoverCliente(string pParametro)
        {
            foreach (var p in _Pessoas.Pessoas)
            {
                if (p.CPF == pParametro || p.Nome == pParametro)
                {
                    Console.WriteLine($"Cliente {p.Nome} removido!\n");
                    _Pessoas.Pessoas.Remove(p);
                    SalvarClientes();
                    break;
                }
            }
        }

        static int QuantidadeIDs()
        {
            int n = 0;
            foreach (var p in _Pessoas.Pessoas)
            {
                if (p.ID != null)
                {
                    n = Convert.ToInt32(p.ID);
                }
            }

            return n;
        }

        //Gerar arquivo .XML
        //Serializar informações da memória em Elementos e Atributos, e preencher dentro do arquivo
        static void SalvarClientes()
        {
            Type[] Tipo = { typeof(Pessoa) };
            XmlSerializer Serializador = new XmlSerializer(typeof(ListaPessoas), Tipo);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            FileStream fs = new FileStream("Clientes.xml", FileMode.Create);
            Serializador.Serialize(fs, _Pessoas, ns);
            fs.Close();
        }

        //Abre o arquivo, deserializar em Elementos e Atributos
        //Preencher na memoria de _Pessoas as informações salvas no arquivo
        static void CarregarClientes()
        {
            using (FileStream fs = new FileStream("Clientes.xml", FileMode.Open))
            {
                Type[] Tipo = { typeof(Pessoa) };
                XmlSerializer Serializador = new XmlSerializer(typeof(ListaPessoas), Tipo);
                _Pessoas = (ListaPessoas)Serializador.Deserialize(fs);
            }
        }

        static void ListarCadastros()
        {
            foreach (var p in _Pessoas.Pessoas)
            {
                Console.WriteLine($"ID: {p.ID}\nNome: {p.Nome}\nCPF: {p.CPF}\nTelefone: {p.Telefone}\nCidade: {p.Cidade}\nEmail: {p.Email}\n\n");
            }
        }

        //obter nome de algum cliente usando cpf
        static string NomeCliente(string pCpf)
        {
            string nome = "";
            foreach (var p in _Pessoas.Pessoas)
            {
                if (p.CPF == pCpf)
                {
                    nome = p.Nome;
                }
            }
            return nome;
        }

        //parametro de posição apenas por questões visuais
        static void ConsultarCadastro(string cpf, int pos)
        {
            string[] ImprimeCadastro = new string[6];
            int n = pos;
            foreach (var p in _Pessoas.Pessoas)
            {
                if (p.CPF == cpf)
                {
                    ImprimeCadastro[0] = ($"ID: {p.ID}");
                    ImprimeCadastro[1] = ($"Nome: {p.Nome}");
                    ImprimeCadastro[2] = ($"CPF: {p.CPF}");
                    ImprimeCadastro[3] = ($"Telefone: {p.Telefone}");
                    ImprimeCadastro[4] = ($"Cidade: {p.Cidade}");
                    ImprimeCadastro[5] = ($"Email: {p.Email}");

                    for (int i = 0; i < ImprimeCadastro.Length; i++)
                    {
                        Console.SetCursorPosition(44, n);
                        //(descer linha de texto sem alterar a posição horizontal)
                        Console.Write(ImprimeCadastro[i]);
                        n++;
                    }
                }
            }
        }

        static void EditarClienteBD(string pCpf, int atributo, string pParametro)
        {
            foreach (var p in _Pessoas.Pessoas)
            {
                if (p.CPF == pCpf)
                {
                    switch (atributo)
                    {
                        case 1:
                            {
                                p.Nome = pParametro;
                            }
                            break;
                        case 2:
                            {
                                p.Telefone = pParametro;
                            }
                            break;
                        case 3:
                            {
                                p.Cidade = pParametro;
                            }
                            break;
                        case 4:
                            {
                                p.Email = pParametro;
                            }
                            break;
                    }
                }
            }
            SalvarClientes();
        }

        //Conferir se um CPF existe a partir de um CPF ou um Nome
        public static bool ExisteCPF(string pParametro)
        {
            bool possivel = false;

            foreach (var p in _Pessoas.Pessoas)
            {
                if (p.CPF == pParametro || p.Nome == pParametro)
                {
                    possivel = true;
                    break;
                }
            }
            return possivel;
        }
        public static void PessoasCadastradas()
        {
            //Percorrer todos os relatorios da memória
            foreach (var p in _Pessoas.Pessoas)
            {

                //possições verticais alteradas e inclusão de informações

                Console.SetCursorPosition(2, Console.CursorTop + 1);
                Console.Write(p.ID);

                Console.SetCursorPosition(24, Console.CursorTop);

                Console.Write(Convert.ToUInt64(p.CPF).ToString(@"000\.000\.000\-00"));

                Console.SetCursorPosition(42, Console.CursorTop);
                Console.WriteLine(p.Nome);

            }
        }
        #endregion

        #region GerenciarProdutos
        static void CadastrarProduto(Produto produto)
        {
            _Produtos.AdicionarProduto(produto);
        }

        static void RemoverProduto(string ID)
        {
            foreach (var p in _Produtos.Produtos)
            {
                if (p.ID == ID)
                {
                    Console.WriteLine($"Produto {p.Nome} removido!\n");
                    _Produtos.Produtos.Remove(p);
                    break;
                }
            }
        }

        //Gerar arquivo .XML
        //Serializar informações da memória em Elementos e Atributos, e preencher dentro do arquivo
        static void SalvarProdutos()
        {
            Type[] Tipo = { typeof(Produto) };
            XmlSerializer Serializador = new XmlSerializer(typeof(ListaProdutos), Tipo);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            FileStream fs = new FileStream("Produtos.xml", FileMode.Create);
            Serializador.Serialize(fs, _Produtos, ns);
            fs.Close();
        }

        //Abre o arquivo, deserializar em Elementos e Atributos
        //Preencher na memoria de _Produtos as informações salvas no arquivo
        static void CarregarProdutos()
        {
            using (FileStream fs = new FileStream("Produtos.xml", FileMode.Open))
            {
                Type[] Tipo = { typeof(Produto) };
                XmlSerializer Serializador = new XmlSerializer(typeof(ListaProdutos), Tipo);
                _Produtos = (ListaProdutos)Serializador.Deserialize(fs);
            }
        }

        //conferir estoque a partir de ID e quantia de unidades que está tentando ser vendido
        static bool EstoqueValido(string ID, int unidades)
        {
            bool valido = true;
            foreach (var p in _Produtos.Produtos)
            {
                if (p.ID == ID)
                {
                    if (Convert.ToInt32(p.Estoque) < unidades) valido = false;
                }
            }

            return valido;
        }

        //listar produto na tela de venda a partir de seu ID e quantidade inserida
        static void IncluirProduto(string ID, int pQuantidade)
        {
            foreach (var p in _Produtos.Produtos)
            {
                if (p.ID == ID)
                {
                    double total = Convert.ToDouble(p.Preço) * pQuantidade;

                    precoTotal += total;

                    Console.SetCursorPosition(2, Console.CursorTop);
                    Console.Write($"ID: {p.ID}");

                    Console.SetCursorPosition(10, Console.CursorTop);
                    Console.Write(p.Categoria);

                    Console.SetCursorPosition(21, Console.CursorTop);
                    Console.Write(p.Nome);

                    Console.SetCursorPosition(40, Console.CursorTop);
                    Console.Write($"x{pQuantidade}");

                    Console.SetCursorPosition(45, Console.CursorTop);
                    Console.Write(total.ToString("C"));
                }
            }
        }

        //Gerar lista de produtos disponiveis, com suas determinadas informações
        static void ListarProdutos()
        {
            string[] info = new string[6];

            int n = 5;
            int Pos = 0;

            //Alterar posição horizontal dependendo da categoria do produto
            foreach (var p in _Produtos.Produtos)
            {
                switch (p.Categoria)
                {
                    case "Infantis":
                        {
                            Pos = 59;
                        }
                        break;
                    case "Mangá":
                        {
                            Pos = 79;
                        }
                        break;
                    case "Herois":
                        {
                            Pos = 99;
                        }
                        break;
                }

                double preco = Convert.ToDouble(p.Preço);
                info[2] = ($"Código: {p.ID}");
                info[3] = ($"{p.Nome}");
                info[4] = ("Preço: " + preco.ToString("C"));
                info[5] = ($"Estoque: {p.Estoque}");

                //loop parar pulas linhas e manter a mesma posição vertical
                for (int i = 0; i < info.Length; i++)
                {
                    Console.SetCursorPosition(Pos, n);

                    if (Convert.ToInt32(p.Estoque) < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }

                    //colorir o produto de forma diferente caso esteja fora de estoque
                    if (i == info.Length - 1 && Convert.ToInt32(p.Estoque) < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    Console.Write(info[i]);
                    Console.ResetColor();

                    n++;
                }

                //se ultrapassar a ultima linha da primeira categoria de produtos
                //resetar a posição vertical do cursor
                if (n > 29) n = 5;
            }
        }
        static void ConsultarProduto(string pProduto, int pos)
        {
            string[] ImprimeProduto = new string[5];
            int n = pos;

            foreach (var p in _Produtos.Produtos)
            {
                if (p.ID == pProduto || p.Nome.ToLower() == pProduto.ToLower())
                {
                    double preco = Convert.ToDouble(p.Preço);
                    ImprimeProduto[0] = ($"ID: {p.ID}");
                    ImprimeProduto[1] = ($"Categoria: {p.Categoria}");
                    ImprimeProduto[2] = ($"Nome: {p.Nome}");
                    ImprimeProduto[3] = ("Preço: " + preco.ToString("C"));
                    ImprimeProduto[4] = ($"Estoque: {p.Estoque}");

                    //loop parar pulas linhas e manter a mesma posição vertical
                    for (int i = 0; i < ImprimeProduto.Length; i++)
                    {
                        Console.SetCursorPosition(44, n);
                        Console.Write(ImprimeProduto[i]);
                        n++;
                    }
                }
            }
        }

        //editar produto usando código de produto ou nome do produto
        //parametro de atributo para possibilitar a edição de apenas 1 informação
        //do cliente sem precisar mudar todas informações
        static void EditarProdutoBD(string pParametro, int atributo, string pNovo)
        {
            foreach (var p in _Produtos.Produtos)
            {
                if (p.ID == pParametro || p.Nome == pParametro)
                {
                    switch (atributo)
                    {
                        case 1:
                            {
                                p.Nome = pNovo;
                            }
                            break;
                        case 2:
                            {
                                p.Categoria = pNovo;
                            }
                            break;
                        case 3:
                            {
                                p.Estoque = pNovo;
                            }
                            break;
                        case 4:
                            {
                                p.Preço = pNovo;
                            }
                            break;
                    }
                }
            }
            SalvarProdutos();
        }

        //atualizar estoque de um produto ID com parametros de operação e quantidade
        static void AtualizarEstoque(string ID, string operacao, int quantia)
        {
            switch (operacao)
            {
                case "+":
                    foreach (var p in _Produtos.Produtos)
                    {
                        if (p.ID == ID)
                        {
                            int n = Convert.ToInt32(p.Estoque) + quantia;
                            p.Estoque = n.ToString();
                            break;
                        }
                    }
                    break;

                case "-":
                    foreach (var p in _Produtos.Produtos)
                    {
                        if (p.ID == ID)
                        {
                            int n = Convert.ToInt32(p.Estoque) - quantia;
                            p.Estoque = n.ToString();
                            break;
                        }
                    }
                    break;

                default:
                    Console.Write("[AtualizarEstoque] -  Operação inválida.\n\n");
                    break;
            }

            SalvarProdutos();
        }

        //obter nome do produto a partir do código ID
        static string NomeProduto(string pID)
        {
            string nome = "";
            foreach (var p in _Produtos.Produtos)
            {
                if (p.ID == pID)
                {
                    nome = p.Nome;
                }
            }
            return nome;
        }

        //obter preço de um produto a partir de seu ID
        static double PrecoProduto(string pID)
        {
            double preco = 0;
            foreach (var p in _Produtos.Produtos)
            {
                if (p.ID == pID)
                {
                    preco = Convert.ToDouble(p.Preço);
                }
            }
            return preco;
        }
        //Conferir se um Produto existe a partir de um ID ou um Nome
        public static bool ExisteProduto(string pParametro)
        {
            bool possivel = false;

            foreach (var p in _Produtos.Produtos)
            {
                if (p.ID == pParametro || p.Nome == pParametro)
                {
                    possivel = true;
                    break;
                }
            }
            return possivel;
        }
        #endregion

        #region GerenciarRelatorio
        static void RegistrarRelatorio(Relatorio relatorio)
        {
            _Relatorios.RegistrarRelatorio(relatorio);
        }

        //Gerar arquivo .XML
        //Serializar informações da memória em Elementos e Atributos, e preencher dentro do arquivo
        static void SalvarRelatorio()
        {
            Type[] Tipo = { typeof(Relatorio) };
            XmlSerializer Serializador = new XmlSerializer(typeof(ListaRelatorios), Tipo);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            FileStream fs = new FileStream("Relatorio.xml", FileMode.Create);
            Serializador.Serialize(fs, _Relatorios, ns);
            fs.Close();
        }

        //Abre o arquivo, deserializar em Elementos e Atributos
        //Preencher na memoria de _Relatorios as informações salvas no arquivo
        static void CarregarRelatorio()
        {
            using (FileStream fs = new FileStream("Relatorio.xml", FileMode.Open))
            {
                Type[] Tipo = { typeof(Relatorio) };
                XmlSerializer Serializador = new XmlSerializer(typeof(ListaRelatorios), Tipo);
                _Relatorios = (ListaRelatorios)Serializador.Deserialize(fs);
            }
        }
        public static void ListarVendas()
        {
            //Percorrer todos os relatorios da memória
            foreach (var R in _Relatorios.Relatorios)
            {
                //Separar a informação de data/hora para poder utilizar somente a data
                string[] data = R.Data.Split(new[] { ' ' });

                //Conferir se o relatório é do dia atual
                if (data[0] == DateTime.Now.ToShortDateString())
                {
                    //dividir o nome para utilização de apenas o primeiro nome no relatório para poupar espaço
                    string[] nome = R.NomeCliente.Split(new[] { ' ' });

                    //possições verticais alteradas e inclusão de informações

                    Console.SetCursorPosition(2, Console.CursorTop);
                    Console.Write(R.Data);

                    Console.SetCursorPosition(24, Console.CursorTop);

                    Console.Write(Convert.ToUInt64(R.CPF).ToString(@"000\.000\.000\-00"));

                    Console.SetCursorPosition(42, Console.CursorTop);
                    Console.Write(nome[0]);

                    Console.SetCursorPosition(54, Console.CursorTop);
                    Console.Write(R.NomeProduto);

                    Console.SetCursorPosition(77, Console.CursorTop);
                    Console.Write(R.Quantidade);

                    Console.SetCursorPosition(88, Console.CursorTop);
                    double preco = Convert.ToDouble(R.Preço);
                    Console.WriteLine(preco.ToString("C"));
                }
            }
        }
        #endregion

        static void Main(string[] args)
        {
            Console.SetWindowSize(78, 25);
            Console.Title = "Livraria Liderança";
            Program Acesso = new Program();

            Carregando();
            Logar();
        }


        #region Tela de Carregamento
        static void Carregando()
        {
            Desenhar.Retangulo(Console.WindowWidth - 1, Console.WindowHeight - 2, 0, 0, ConsoleColor.Cyan, 0);

            string[] logo = new string[8] { "        ██╗", "██╗     ╚═╝  ██████╗   ██████╗ ██████╗   █████╗  ███╗   ██╗ ██████╗  █████╗ ", "██║     ██╗  ██╔═══██╗ ██╔═══╝ ██╔══██╗ ██╔══██╗ ██╠██╗ ██║ ██╔═══╝ ██╔══██╗", "██║     ██║  ██║   ██║ ████╗   ██████╔╝ ███████║ ██║╚██╗██║ ██║     ███████║", "██╚═══╗ ██║  ██║   ██║ ██╔═╝   ██╔══██╗ ██╔══██║ ██║  ╚███║ ██║     ██╔══██║", "██████║ ██║  ██████╔═╝ ██████╗ ██║  ██║ ██║  ██║ ██║   ╚██║ ██████╗ ██║  ██║", "╚═════╝ ╚═╝  ╚═════╝   ╚═════╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝    ╚═╝ ╚═██╔═╝ ╚═╝  ╚═╝", "                                                              ╚═╝" };

            for (int i = 0; i < logo.Length; i++)
            {
                Console.SetCursorPosition(1, i + 3);
                Console.WriteLine(logo[i]);
            }

            //Carregamento de forma aleatória
            for (int i = 0; i <= 100; i++)
            {
                int valor = (i += rand.Next(11)); // utilização do numero randomico para preenchimento da barra

                Console.SetCursorPosition((Console.WindowWidth / 2 - 7), (Console.WindowHeight / 2 + 6));
                Console.Write("Carregando ");

                Desenhar.EfeitoGirar();

                Console.SetCursorPosition((Console.WindowWidth / 2 - 11), (Console.WindowHeight / 2 + 8));
                if (valor <= 100)
                {
                    BarraProgresso(valor, 100, 20, '█');
                }
                else if (valor >= 95)
                {
                    BarraProgresso(100, 100, 20, '█');
                }
                System.Threading.Thread.Sleep(200);
            }

            TelaLogin();
        }

        //Barra de progresso editavel, é preenchida inicialmente com
        //o valor atual, o valor maximo, tamanho visual e simbolo a ser desenhado
        static void BarraProgresso(int atual, int maximo, int tamanho, char caractere)
        {
            Console.CursorVisible = false;

            int inicio = Console.CursorLeft;
            decimal pocentagem = (decimal)atual / (decimal)maximo;
            int chars = (int)Math.Floor(pocentagem / ((decimal)1 / (decimal)tamanho));

            //strings que criam o fundo e o preenchimento da barra
            string txt1 = string.Empty, txt2 = string.Empty;

            //preenchimento das strings
            for (int i = 0; i < chars; i++) txt1 += caractere;
            for (int i = 0; i < tamanho - chars; i++) txt2 += caractere;

            //desenhando o fundo e a barra
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(txt1);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(txt2);

            int n = Convert.ToInt32(pocentagem * 100);

            //preencher bgcolor do texto com a cor atual da barra
            //para possibilitar de escrever a porcetagem atual no centro da barra
            bool testcor = false, testtxt = false;
            //alterar valor das bool's para mudar a cor caso tenha passado da metade da barra
            if (n > 50) testcor = true;
            if (n > 99) testtxt = true;

            Console.ResetColor();
            Console.BackgroundColor = testcor ? ConsoleColor.Green : ConsoleColor.DarkGray;

            //ajuste de posição vertical caso o valor tenha 1, 2 ou 3 digitos para manter centralizado
            Console.SetCursorPosition(((Console.WindowWidth / 2) - (testtxt ? 4 : 3)), (Console.WindowHeight / 2 + 8));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("{0}", " " + (n).ToString() + "%");
            Console.CursorLeft = inicio;
            Console.ResetColor();
        }

        #endregion

        #region Login
        public static void Logar()
        {
            TelaLogin();
            Program Acesso = new Program();

            int Tentativas = 3; //Definir a quantidade de tentativas para logar

            Console.CursorVisible = true;

            //redigitar a quantidade de tentativas para logar no for
            //pois se for utilizada a variavel Tentativas, o valor dela está sendo alterado
            //cada vez em que o usuario erra a senha, e tornaria o loop falho, possibilitando o login com credenciais incorretas
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(((Console.WindowWidth / 2) - 4), ((Console.WindowHeight / 2) + 1));
                string usuario = Console.ReadLine();

                Console.SetCursorPosition(((Console.WindowWidth / 2) - 4), ((Console.WindowHeight / 2) + 4));

                //substituição de um readline simples, por um método próprio, para que seja possível
                //digitar a senha, alocando na memoriao que for digitado, mas preencher na tela 
                //apenas simbolos mascarando a senha
                var senha = LerSenha();

                //caso credencias estiverem incorretas...
                if (usuario != admin || senha != adminsenha)
                {
                    Tentativas--; //diminuir a quantidade de tentativas
                    //redesenhar tela
                    TelaLogin();
                    Console.SetCursorPosition(((Console.WindowWidth / 2) - 10), ((Console.WindowHeight / 2) + 6));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Usuario ou senha inválido.");
                    Console.ResetColor();
                    Console.SetCursorPosition(((Console.WindowWidth / 2) - 10), ((Console.WindowHeight / 2) + 7));
                    Console.Write("Tentativas restantes: " + Tentativas);
                }

                //sair do loop for caso credenciais estejam corretas
                else
                {
                    break;
                }
            }

            if (Tentativas < 1)
            {
                TelaLogin();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(((Console.WindowWidth / 2) - 10), ((Console.WindowHeight / 2) + 6));
                Console.WriteLine("Tentativas excedidas");
                System.Threading.Thread.Sleep(500);
                Console.SetCursorPosition(((Console.WindowWidth / 2) - 10), ((Console.WindowHeight / 2) + 7));

                //oferecer
                RecuperaSenha();

                Console.SetCursorPosition(((Console.WindowWidth / 2) - 10), ((Console.WindowHeight / 2) + 11));
                Console.Write("Finalizando aplicação");
                System.Threading.Thread.Sleep(4800);
                Environment.Exit(0);
            }
            else
            {
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Green;

                //limpar escritos sem precisar de Console.Clear e redesenhar toda a tela de novo
                Console.SetCursorPosition(((Console.WindowWidth / 2) - 10), ((Console.WindowHeight / 2) + 6));
                Console.Write("\t\t\t\t\t\t");
                Console.SetCursorPosition(((Console.WindowWidth / 2) - 10), ((Console.WindowHeight / 2) + 7));
                Console.Write("\t\t\t\t\t\t");

                Console.SetCursorPosition(((Console.WindowWidth / 2) - 10), ((Console.WindowHeight / 2) + 6));
                Console.WriteLine("Logado com sucesso!");
                System.Threading.Thread.Sleep(1000);
                Menu();
            }
        }

        public static string LerSenha()
        {
            //substituição de um readline simples, por um método próprio, para que seja possível
            //digitar a senha, alocando na memoria o que for digitado, mas preencher na tela 
            //apenas simbolos mascarando a senha

            string senha = "";
            ConsoleKeyInfo cki = Console.ReadKey(true);
            while (cki.Key != ConsoleKey.Enter)
            {
                if (cki.Key != ConsoleKey.Backspace)
                {
                    //preencher string senha adicionando cada letra pressionada
                    senha += cki.KeyChar;
                    //simular digitação da senha preenchendo simbolo
                    Console.Write("*");
                }

                else if (cki.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(senha))
                    {
                        senha = senha.Substring(0, senha.Length - 1);
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                cki = Console.ReadKey(true);
            }
            Console.WriteLine();
            return senha;
        }
        public static string RecuperaSenha()
        {
            string[] CPFs = new string[7];
            CPFs[0] = "10402034694"; //marcos
            CPFs[1] = "12259181627"; //adinan
            CPFs[2] = "11488711607"; //thales
            CPFs[3] = "11547025611"; //rodrigo
            CPFs[4] = "14372368631"; //vinicius
            CPFs[5] = "10517907607"; //victor
            CPFs[6] = "07017417674"; //alexandre
            string cpf;

            Console.ResetColor();
            Console.WriteLine("Digite seu CPF: ");
            Console.SetCursorPosition(((Console.WindowWidth / 2) - 10), ((Console.WindowHeight / 2) + 8));
            cpf = Console.ReadLine();

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (CPFs.Contains(cpf))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(((Console.WindowWidth / 2) - 10), ((Console.WindowHeight / 2) + 9));
                Console.WriteLine("Senha: {0}", adminsenha);
                Console.ResetColor();
                Console.SetCursorPosition(((Console.WindowWidth / 2) - 10), ((Console.WindowHeight / 2) + 10));
                Console.WriteLine("Aperte qualquer tecla para digitar novamente");
                Console.ReadKey();
                Console.Clear();
                TelaLogin();
                Logar();
            }

            Console.SetCursorPosition(((Console.WindowWidth / 2) - 10), ((Console.WindowHeight / 2) + 9));
            Console.WriteLine("CPF digitado não existe");
            Console.SetCursorPosition(((Console.WindowWidth / 2) - 10), ((Console.WindowHeight / 2) + 10));
            Console.WriteLine("ou não tem permissão de acesso ao sistema.");
            return cpf;
        }
        #endregion

        #region ValidarCPF
        public static string DigitaCpf()
        {
            string cpf;
            do
            {
            //Area para ser retornada facilmente sem precisar de Clear(limpar tudo)
            DigitarCPF:

                //Limpar apenas o necessário, mantendo o resto da interface sem precisar redesenhar
                Console.SetCursorPosition(44, 6);
                Console.Write("\t\t\t\t\t\t\t");
                Console.SetCursorPosition(44, 7);
                Console.Write("\t\t\t\t\t\t\t");
                Console.SetCursorPosition(44, 8);
                Console.Write("\t\t\t\t\t\t\t");
                Console.SetCursorPosition(44, 34);
                Console.Write("\t\t\t\t\t\t\t");
                Console.SetCursorPosition(44, 35);
                Console.Write("\t\t\t\t\t\t\t");

                Console.SetCursorPosition(44, 6);
                Console.Write("Digite o CPF: ");
                cpf = Console.ReadLine();


                if (ExisteCPF(cpf))
                {
                    Console.SetCursorPosition(44, 7);
                    Console.Write("CPF já cadastrado.");
                    Console.SetCursorPosition(44, 8);
                    Console.Write("Pressione qualquer tecla para tentar novamente.");
                    Console.ReadKey();
                    goto DigitarCPF; //Retornar a area acima
                }

                Console.SetCursorPosition(75, 6);
                if (ValidaCPF(cpf).Equals(true))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("CPF Válido");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("CPF Inváldo");
                    Console.ResetColor();
                    Console.SetCursorPosition(44, 34);
                    Console.Write("Pressione qualquer tecla para tentar novamente");
                    Console.SetCursorPosition(44, 35);
                    Console.Write("ESC - Voltar ao Menu Principal");
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.Escape:
                            Menu();
                            break;
                    }
                }
                Console.ResetColor();
            } while ((ValidaCPF(cpf).Equals(false)));

            return cpf;
        }
        public static string GuardarCpf(string pCpf)
        {
            return pCpf;
        }
        public static bool ValidaCPF(string pCpf)
        {   // números da fórmula matemática para validação do CPF.
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            //Variavel soma para somar os resultados das multiplicações.
            // Variavel resto para guardar o resto da formula: (soma *10)/11.
            int soma, resto;
            string digito = "", numeroscpf;
            pCpf = pCpf.Trim();
            pCpf = pCpf.Replace(".", "").Replace("-", "");
            //Verifica se os digitos são iguais, caso sejam, retorna falso, nesse caso CPF inválido.
            switch (pCpf)
            {
                case "00000000000":
                    {
                        return false;
                    }
                case "11111111111":
                    {
                        return false;
                    }
                case "22222222222":
                    {
                        return false;
                    }
                case "33333333333":
                    {
                        return false;
                    }
                case "44444444444":
                    {
                        return false;
                    }
                case "55555555555":
                    {
                        return false;
                    }
                case "66666666666":
                    {
                        return false;
                    }
                case "77777777777":
                    {
                        return false;
                    }
                case "88888888888":
                    {
                        return false;
                    }
                case "99999999999":
                    {
                        return false;
                    }
            }
            if (pCpf.Length != 11)
            {
                return false;
            }
            // numeroscpf recebe os 9 primeiro digitos do CPF, ignorando os dois últimos
            numeroscpf = pCpf.Substring(0, 9);
            soma = 0;
            //verificando o penúltimo digito
            for (int i = 0; i < 9; i++)
            {
                // múltiplica os 9 digitos do CPF pela sequencia da expressão matematica para verificar o 1° dos dois ultimo digitos
                soma += Convert.ToInt32(numeroscpf[i].ToString()) * multiplicador1[i];
            }
            resto = (soma * 10) % 11;
            if (resto == 10)
            {
                resto = 0;
                digito = resto.ToString();
                numeroscpf += digito;
            }
            else
            {
                digito = resto.ToString();
                numeroscpf += digito;
            }
            // Verificando o último digito do CPF
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += Convert.ToInt32(numeroscpf[i].ToString()) * multiplicador2[i];
            }
            resto = (soma * 10) % 11;
            if (resto == 10)
            {
                resto = 0;
                digito = resto.ToString();
                numeroscpf += digito;
            }
            else
            {
                digito = digito + resto.ToString();
            }
            return pCpf.EndsWith(digito);
        }
        #endregion

        #region Menu
        public static void Menu()
        {
            Console.Title = "Livraria Liderança -> Menu";
            Console.CursorVisible = false;
            Console.SetCursorPosition(1, 1);
            Console.Clear();

            ImagemMenu(10);//tela com nenhuma opção selecionada, utilização do parametro para alterar cores das opções do menu

            ConsoleKeyInfo cki;
            cki = Console.ReadKey();

            while (cki.Key != ConsoleKey.Escape)
            {
                switch (cki.Key)
                {
                    case ConsoleKey.F1:
                        {
                            Ajuda();
                        }
                        break;
                    case ConsoleKey.F2:
                        {
                            CarregarClientes();
                            ImagemMenuCliente(10);
                            MenuClientes();
                        }
                        break;
                    case ConsoleKey.F3:
                        {
                            CarregarProdutos();
                            ImagemMenuProdutos(10);
                            MenuProdutos();
                        }
                        break;
                    case ConsoleKey.F4:
                        {
                            Console.Clear();
                            MenuVendas();
                        }
                        break;
                    case ConsoleKey.F12:
                        {
                            ImagemMenu(4);
                            System.Threading.Thread.Sleep(375);
                            Environment.Exit(0);
                        }
                        break;

                    default:
                        {
                            Menu();
                        }
                        break;
                }
                Console.ReadKey();
            }
        }
        #endregion

        #region Ajuda
        public static void Ajuda()
        {
            ImagemMenu(0);

            Console.Title = "Livraria Liderança -> Ajuda";
            QuadradoDefault();
            ConsoleKeyInfo cki;

            Console.SetCursorPosition(43, 3);
            //Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t\t\t\t\t ");
            string title = "Ajuda";
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(70 - title.Length / 2, 3);
            Console.Write(title);
            Console.ResetColor();

            string[] perguntasAjuda = new string[8];
            perguntasAjuda[0] = "F4   | Como cadastrar clientes?";
            perguntasAjuda[1] = "F5   | Como consultar clientes?";
            perguntasAjuda[2] = "F6   | Como excluir clientes?";
            perguntasAjuda[3] = "F7   | Como consultar produtos?";
            perguntasAjuda[4] = "F8   | Como editar produtos?";
            perguntasAjuda[5] = "F9   | Como fazer uma venda?";
            perguntasAjuda[6] = "F10  | Como tirar um relatório?";
            perguntasAjuda[7] = "F12  | Voltar ao menu";
            int n = 6;
            for (int i = 0; i < perguntasAjuda.Length; i++)
            {
                Console.SetCursorPosition(44, n);
                Console.Write(perguntasAjuda[i]);
                n++;
            }
            string[] respostasAjuda = new string[7];
            respostasAjuda[0] = "Menu Principal -> F2 (Clientes) -> F8 (Cadastro)";
            respostasAjuda[1] = "Menu Principal -> F2 (Clientes) -> F9 (Consulta)";
            respostasAjuda[2] = "Menu Principal -> F2 (Clientes) -> F10 (Exclusão)";
            respostasAjuda[3] = "Menu Principal -> F3 (Produtos) -> F9 (Consulta)";
            respostasAjuda[4] = "Menu Principal -> F3 (Clientes) -> F10 (Edição)";
            respostasAjuda[5] = "Menu Principal -> F4 (Clientes) -> F8 (Venda)";
            respostasAjuda[6] = "Menu Principal -> F4 (Clientes) -> F9 (Relatório)";

            Console.SetCursorPosition(44, 15);
            cki = Console.ReadKey();

            switch (cki.Key)
            {
                case ConsoleKey.F1:
                    Menu();
                    break;
                case ConsoleKey.F4:
                    {
                        LimparRespostas();
                        Console.Write(respostasAjuda[0]);
                        Ajuda();
                    }
                    break;
                case ConsoleKey.F5:
                    {
                        LimparRespostas();
                        Console.Write(respostasAjuda[1]);
                        Ajuda();
                    }
                    break;
                case ConsoleKey.F6:
                    {
                        LimparRespostas();
                        Console.Write(respostasAjuda[2]);
                        Ajuda();
                    }
                    break;
                case ConsoleKey.F7:
                    {
                        LimparRespostas();
                        Console.Write(respostasAjuda[3]);
                        Ajuda();
                    }
                    break;
                case ConsoleKey.F8:
                    {
                        LimparRespostas();
                        Console.Write(respostasAjuda[4]);
                        Ajuda();
                    }
                    break;
                case ConsoleKey.F9:
                    {
                        LimparRespostas();
                        Console.Write(respostasAjuda[5]);
                        Ajuda();
                    }
                    break;
                case ConsoleKey.F10:
                    {
                        LimparRespostas();
                        Console.Write(respostasAjuda[6]);
                        Ajuda();
                    }
                    break;
                case ConsoleKey.F12:
                    {
                        Menu();
                    }
                    break;
                default:
                    {
                        LimparRespostas();
                        Console.WriteLine("Opção inválida");
                        Ajuda();
                    }
                    break;
            }
        }
        public static void LimparRespostas()
        {
            Console.ResetColor();
            Console.SetCursorPosition(44, 15);
            Console.Write("\t\t\t\t\t\t     ");
            Console.SetCursorPosition(44, 15);
        }
        #endregion

        #region Clientes
        public static void MenuClientes()
        {
            Console.Title = "Livraria Liderança -> Clientes";

            ConsoleKeyInfo cki;
            cki = Console.ReadKey();

            while (cki.Key != ConsoleKey.Escape)
            {
                switch (cki.Key)
                {
                    case ConsoleKey.F2:
                        {
                            Menu();
                        }
                        break;
                    case ConsoleKey.F8:
                        {
                            ImagemMenuCliente(0);
                            CadastroCliente();
                        }
                        break;
                    case ConsoleKey.F9:
                        {
                            ImagemMenuCliente(1);
                            ConsultaCliente(true);
                        }
                        break;
                    case ConsoleKey.F10:
                        {
                            ImagemMenuCliente(2);
                            ExcluirCliente();
                        }
                        break;
                }
                cki = Console.ReadKey();
            }
            Menu();
        }


        public static void CadastroCliente()
        {
            TelaCadastroCliente();

            string[] perguntas = new string[5] { "CPF", "Nome: ", "Cidade: ", "Telefone: ", "Email: " };
            string[] respostas = new string[5] { "", "", "", "", "" };

            respostas[0] = DigitaCpf(); //preenche respostas[0] com retorno do método de validação de cpf

            int n = 8; //posição horizontal
            for (int i = 1; i < perguntas.Length; i++)
            {
                Console.SetCursorPosition(44, n);
                Console.Write(perguntas[i]);
                respostas[i] = Console.ReadLine();
                n += 2;//descer 2 linhas a cada vez que for escrever uma nova pergunta
            }

            //cadastra o cliente com o maior ID já existente +1, para manter a ordem
            //e com as informações recebidas no vetor respostas
            CadastrarCliente(new Pessoa((QuantidadeIDs() + 1).ToString(), respostas[1], respostas[0], respostas[3], respostas[2], respostas[4]));
            SalvarClientes();

            Console.SetCursorPosition(44, 17);
            Console.Write("Cliente {0} cadastrado com sucesso. ID: {1}", respostas[1], QuantidadeIDs());

            Console.SetCursorPosition(44, 34);
            Console.Write("F1 - Cadastrar outro cliente");
            Console.SetCursorPosition(44, 35);
            Console.Write("F2 - Retornar ao Menu Principal");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.F1:
                    {
                        Console.Clear();
                        ImagemMenu(1);
                        ImagemMenuCliente(1);
                        TelaCadastroCliente();
                        CadastroCliente();
                    }
                    break;
                case ConsoleKey.F2:
                    {
                        Menu();
                    }
                    break;
            }
        }

        public static void ConsultaCliente(bool especifico)
        {
            TelaConsultaCliente();
            if (especifico)
            {
                Console.SetCursorPosition(44, 6);
                Console.Write("F1  | Listar todos os clientes");
                Console.SetCursorPosition(44, 7);
                Console.Write("F2  | Consultar cliente específico");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F1:
                        {
                            Console.Clear();
                            ListaTodosClientes();
                        }
                        break;
                    case ConsoleKey.F2:
                        {
                            Console.Clear();
                            ImagemMenu(1);
                            ImagemMenuCliente(1);
                            ConsultaCliente(false);

                        }
                        break;
                }
            }

            Console.SetCursorPosition(44, 6);
            Console.Write("Digite o CPF desejado: ");
            string cpf = Console.ReadLine();
            Console.SetCursorPosition(44, 9);

            if (!ExisteCPF(cpf))
            {
                Console.Write("Não foi possível encontrar o CPF digitado");
                Console.SetCursorPosition(44, 34);
                Console.Write("Pressione qualquer tecla para tentar novamente");
                Console.SetCursorPosition(44, 35);
                Console.Write("ESC - Voltar ao Menu Principal");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        Menu();
                        break;

                    default:
                        {
                            Console.Clear();
                            ImagemMenuCliente(1);
                            ConsultaCliente(true);
                        }
                        break;
                }
            }
            else
            {
                ConsultarCadastro(cpf, 8);

                Console.SetCursorPosition(44, 33);
                Console.Write("F1 - Nova consulta");
                Console.SetCursorPosition(44, 34);
                Console.Write("F2 - Editar cliente");
                Console.SetCursorPosition(44, 35);
                Console.Write("F3 - Retornar ao Menu Principal");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F1:
                        {
                            Console.Clear();
                            ImagemMenu(1);
                            ImagemMenuCliente(1);
                            ConsultaCliente(true);
                        }
                        break;
                    case ConsoleKey.F2:
                        {
                            Console.Clear();
                            ImagemMenu(1);
                            ImagemMenuCliente(1);
                            Console.SetCursorPosition(44, 6);
                            Console.Write("Digite o CPF desejado: {0}", cpf);
                            ConsultarCadastro(cpf, 8);
                            EditarCliente(cpf);
                        }
                        break;

                    case ConsoleKey.F3:
                        {
                            Console.Clear();
                            Menu(); //volta para menu
                        }
                        break;
                }
            }
        }

        public static void EditarCliente(string cpf)
        {
            string[] info = new string[4];

            ConsoleKeyInfo lerTecla;
            do
            {
                TelaEditarCliente();

                string[] opcoesEditarCliente = new string[8];
                opcoesEditarCliente[0] = "F1  |  Editar Nome";
                opcoesEditarCliente[1] = "F2  |  Telefone";
                opcoesEditarCliente[2] = "F3  |  Cidade";
                opcoesEditarCliente[3] = "F4  |  Email";

                int n = 15;//posição vertical
                for (int i = 0; i < opcoesEditarCliente.Length; i++)
                {
                    Console.SetCursorPosition(44, n);
                    Console.Write(opcoesEditarCliente[i]);
                    n++;//escrever pulando de linha sem alterar a posição horizontal
                }

                lerTecla = Console.ReadKey();
                Console.SetCursorPosition(44, 20);
                switch (lerTecla.Key)
                {
                    case ConsoleKey.F1:
                        {
                            Console.Write("Novo Nome: ");
                            info[0] = Console.ReadLine();
                            EditarClienteBD(cpf, 1, info[0]);
                            //editar atributo X do cliente do cpf mencionado no parametro
                            //com a nova informação recebida
                            // 1 = Nome   2 = Telefone   3 = Cidade   4 = Email
                        }
                        break;

                    case ConsoleKey.F2:
                        {
                            Console.Write("Novo Telefone: ");
                            info[1] = Console.ReadLine();
                            EditarClienteBD(cpf, 2, info[1]);
                        }
                        break;
                    case ConsoleKey.F3:
                        {
                            Console.Write("Nova Cidade: ");
                            info[2] = Console.ReadLine();
                            EditarClienteBD(cpf, 3, info[2]);
                        }
                        break;
                    case ConsoleKey.F4:
                        {
                            Console.Write("Novo Email: ");
                            info[3] = Console.ReadLine().ToLower();
                            EditarClienteBD(cpf, 4, info[3]);
                        }
                        break;
                }

                Console.SetCursorPosition(44, 22);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Alteração realizada com sucesso!");
                Console.ResetColor();

                ConsultarCadastro(cpf, 24);//segundo parametro inclui posição vertical, apenas por questões visuais

                Console.SetCursorPosition(44, 33);
                Console.Write("F1 - Editar outra informação");
                Console.SetCursorPosition(44, 34);
                Console.Write("F2 - Editar outro cliente");
                Console.SetCursorPosition(44, 35);
                Console.Write("F3 - Retornar ao Menu Principal");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F1:
                        {
                            //refazer a tela do cliente que acabou de ser consultado
                            Console.Clear();
                            ImagemMenu(1);
                            ImagemMenuCliente(1);
                            Console.SetCursorPosition(44, 6);
                            Console.Write("Digite o CPF desejado: {0}", cpf);
                            ConsultarCadastro(cpf, 8);
                            //oferecer opções para editar novas informações do mesmo cliente
                            EditarCliente(cpf);
                        }
                        break;
                    case ConsoleKey.F2:
                        {
                            Console.Clear();
                            ImagemMenu(1);
                            ImagemMenuCliente(1);
                            ConsultaCliente(true);
                        }
                        break;

                    case ConsoleKey.F3:
                        {
                            Console.Clear();
                            Menu();
                        }
                        break;
                }

            } while ((lerTecla.Key != ConsoleKey.F1) && (lerTecla.Key != ConsoleKey.F2) && (lerTecla.Key != ConsoleKey.F3)
                  && (lerTecla.Key != ConsoleKey.F4) && (lerTecla.Key != ConsoleKey.Escape));

            Console.Clear();
            Menu();
        }

        public static void ExcluirCliente()
        {
            TelaExcluirCliente();
            Console.SetCursorPosition(44, 6);

            Console.Write("Digite o nome ou CPF do cliente: ");
            string pParametro = Console.ReadLine();

            Console.SetCursorPosition(44, 9);
            if (!ExisteCPF(pParametro))
            {
                Console.Write("Não foi possível encontrar o CPF ou nome digitado");
                Console.SetCursorPosition(44, 34);
                Console.Write("Pressione qualquer tecla para tentar novamente");
                Console.SetCursorPosition(44, 35);
                Console.Write("ESC - Voltar ao Menu Principal");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        Menu();
                        break;

                    default:
                        {
                            Console.Clear();
                            ImagemMenuCliente(2);
                            ExcluirCliente();
                        }
                        break;
                }
            }
            else
            {
                RemoverCliente(pParametro);

                Console.SetCursorPosition(44, 34);
                Console.Write("F1 - Excluir outro cliente");
                Console.SetCursorPosition(44, 35);
                Console.Write("F2 - Retornar ao Menu Principal");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F1:
                        {
                            Console.Clear();
                            ImagemMenu(1);
                            ImagemMenuCliente(1);
                            ExcluirCliente();
                        }
                        break;

                    case ConsoleKey.F2:
                        {
                            Console.Clear();
                            Menu();
                        }
                        break;
                }
            }
        }
        public static void ListaTodosClientes()
        {
            Console.Clear();
            Console.Title = "Livraria Liderança -> Clientes -> Lista de Clientes";
            Console.CursorVisible = false;

            Console.SetWindowSize(100, 39);
            Desenhar.Retangulo(Console.WindowWidth - 2, Console.WindowHeight - 2, 0, 0, ConsoleColor.Gray, 0);

            string titulo = "Lista de Clientes";
            Console.SetCursorPosition(1, 1);
            //Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t\t\t\t\t\t\t\t\t\t  ");
            Console.SetCursorPosition((Console.WindowWidth / 2) - (titulo.Length / 2) - 1, 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(titulo);
            Console.ResetColor();

            Console.SetCursorPosition(0, 4);
            Console.Write("ID");
            Console.SetCursorPosition(29, Console.CursorTop);
            Console.Write("CPF");
            Console.SetCursorPosition(43, Console.CursorTop);
            Console.Write("Nome");

            Console.SetCursorPosition(2, 6);
            PessoasCadastradas();

            Console.SetCursorPosition(2, Console.WindowHeight - 4);
            Console.Write("F1 - Voltar a Consulta de Cliente");
            Console.SetCursorPosition(2, Console.WindowHeight - 3);
            Console.Write("ESC - Voltar ao Menu Principal");

            ConsoleKeyInfo opc;
            opc = Console.ReadKey();

            switch (opc.Key)
            {
                case ConsoleKey.F1:
                    {
                        Console.Clear();
                        ImagemMenu(1);
                        ImagemMenuCliente(1);
                        ConsultaCliente(true);
                    }
                    break;
                case ConsoleKey.Escape:
                    {
                        Console.Clear();
                        Menu();
                    }
                    break;
            }
        }
        #endregion

        #region Produtos
        public static void MenuProdutos()
        {
            Console.Title = "Livraria Liderança -> Produtos";

            ConsoleKeyInfo cki;
            cki = Console.ReadKey();

            while (cki.Key != ConsoleKey.Escape)
            {
                switch (cki.Key)
                {
                    case ConsoleKey.F3:
                        {
                            Menu();
                        }
                        break;
                    case ConsoleKey.F8:
                        {
                            ImagemMenuProdutos(0);
                            ConsultaProduto();
                        }
                        break;
                    case ConsoleKey.F9:
                        {
                            ImagemMenuProdutos(1);
                            SolicitarProduto();

                        }
                        break;
                }
                cki = Console.ReadKey();
            }
            Console.Clear();
            Menu();
        }

        public static void ConsultaProduto()
        {
            TelaConsultaProduto();

            Console.SetCursorPosition(44, 6);
            Console.Write("Digite o ID ou nome do produto: ");
            string produto = Console.ReadLine();

            if (!ExisteProduto(produto))
            {
                Console.SetCursorPosition(44, 9);
                Console.Write("Produto não localizado, ID ou nome incorreto.");
                Console.SetCursorPosition(44, 34);
                Console.Write("Pressione qualquer tecla para tentar novamente");
                Console.SetCursorPosition(44, 35);
                Console.Write("ESC - Voltar ao Menu Principal");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        Menu();
                        break;

                    default:
                        {
                            Console.Clear();
                            ImagemMenuProdutos(0);
                            ConsultaProduto();
                        }
                        break;
                }
            }
            else
            {
                ConsultarProduto(produto, 8);

                Console.SetCursorPosition(44, 33);
                Console.Write("F1 - Nova consulta");
                Console.SetCursorPosition(44, 34);
                Console.Write("F2 - Editar Produto");
                Console.SetCursorPosition(44, 35);
                Console.Write("F3 - Retornar ao Menu Principal");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F1:
                        {
                            Console.Clear();
                            ImagemMenu(2);
                            ImagemMenuProdutos(0);
                            ConsultaProduto();
                        }
                        break;
                    case ConsoleKey.F2:
                        {
                            Console.Clear();
                            ImagemMenu(2);
                            ImagemMenuProdutos(1);
                            Console.SetCursorPosition(44, 6);
                            Console.Write("Digite o ID ou nome do produto: " + produto);
                            EditarProduto(produto);
                        }
                        break;
                    case ConsoleKey.F3:
                        {
                            Console.Clear();
                            Menu();
                        }
                        break;
                }
            }
        }

        public static void SolicitarProduto()
        {
            TelaEditarProduto();
            Console.SetCursorPosition(44, 6);
            Console.Write("Digite o ID ou nome do produto: ");
            string produto = Console.ReadLine();

            EditarProduto(produto);
        }

        public static void EditarProduto(string produto)
        {
            TelaEditarProduto();

            string[] info = new string[4];

            ConsoleKeyInfo lerTecla;
            do
            {
                ConsultarProduto(produto, 8);

                string[] opcoesProduto = new string[8];
                opcoesProduto[0] = "F1  |  Nome";
                opcoesProduto[1] = "F2  |  Categoria";
                opcoesProduto[2] = "F3  |  Estoque";
                opcoesProduto[3] = "F4  |  Preço";

                int n = 14; //posição vertical inicial
                for (int i = 0; i < opcoesProduto.Length; i++)
                {
                    Console.SetCursorPosition(44, n);
                    Console.Write(opcoesProduto[i]);
                    n++;//pular de linha sem alterar posição horizontal
                }

                lerTecla = Console.ReadKey();
                Console.SetCursorPosition(44, 19);
                switch (lerTecla.Key)
                {
                    case ConsoleKey.F1:
                        {
                            Console.Write("Digite o novo Nome: ");
                            info[0] = Console.ReadLine();
                            EditarProdutoBD(produto, 1, info[0]);
                            //editar atributo X do cliente do cpf mencionado no parametro
                            //com a nova informação recebida
                            // 1 = Nome   2 = Categoria   3 = Estoque   4 = Preço
                        }
                        break;

                    case ConsoleKey.F2:
                        {
                            Console.Write("Digite a nova Categoria: ");
                            info[1] = Console.ReadLine();
                            EditarProdutoBD(produto, 2, info[1]);
                        }
                        break;
                    case ConsoleKey.F3:
                        {
                            Console.Write("Digite o novo Estoque: ");
                            info[2] = Console.ReadLine();
                            EditarProdutoBD(produto, 3, info[2]);
                        }
                        break;
                    case ConsoleKey.F4:
                        {
                            Console.Write("Digite o novo Preço: ");
                            info[3] = Console.ReadLine();
                            EditarProdutoBD(produto, 4, info[3]);
                        }
                        break;
                }

                Console.SetCursorPosition(44, 21);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Alteração realizada com sucesso!");
                Console.ResetColor();


                ConsultarProduto(produto, 23);//segundo parametro é de posição, apenas por questões visuais

                Console.SetCursorPosition(44, 33);
                Console.Write("F1 - Editar outra informação");
                Console.SetCursorPosition(44, 34);
                Console.Write("F2 - Editar outro produto");
                Console.SetCursorPosition(44, 35);
                Console.Write("F3 - Retornar ao Menu Principal");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F1:
                        {
                            //refazer a tela do produto que acabou de ser consultado
                            Console.Clear();
                            ImagemMenu(1);
                            ImagemMenuProdutos(1);
                            Console.SetCursorPosition(44, 6);
                            Console.Write("Digite o ID ou nome do produto: {0}", produto);
                            ConsultarProduto(produto, 8);
                            //dar opção de editar outra informação sobre esse mesmo produto
                            EditarProduto(produto);
                        }
                        break;
                    case ConsoleKey.F2:
                        {
                            Console.Clear();
                            ImagemMenu(1);
                            ImagemMenuProdutos(1);
                            SolicitarProduto();
                        }
                        break;

                    case ConsoleKey.F3:
                        {
                            Console.Clear();
                            Menu();
                        }
                        break;
                }

            } while ((lerTecla.Key != ConsoleKey.F1) && (lerTecla.Key != ConsoleKey.F2) && (lerTecla.Key != ConsoleKey.F3)
                  && (lerTecla.Key != ConsoleKey.F4) && (lerTecla.Key != ConsoleKey.Escape));

            Console.Clear();
            Menu();

        }
        #endregion

        #region Vendas
        public static void MenuVendas()
        {
            Console.Title = "Livraria Liderança -> Vendas";

            ImagemMenuVendas(10);
            ConsoleKeyInfo cki;
            cki = Console.ReadKey();

            switch (cki.Key)
            {
                case ConsoleKey.F4:
                    Menu();
                    break;
                case ConsoleKey.F8:
                    {
                        ImagemMenuVendas(0);
                        System.Threading.Thread.Sleep(375);
                        Vendas();
                    }
                    break;
                case ConsoleKey.F9:
                    {
                        ImagemMenuVendas(1);
                        System.Threading.Thread.Sleep(375);
                        Relatorio();
                    }
                    break;
                default:
                    Menu();
                    break;
            }
        }

        public static void Vendas()
        {
            Console.Clear();
            TelaVendas();

            CarregarClientes();
            CarregarProdutos();
            ListarProdutos();
            precoTotal = 0;

            //receber cpf aqui
            string recebeCPF;
            Console.SetCursorPosition(1, 3);
            Console.Write("Digite o CPF do cliente: ");
            recebeCPF = Console.ReadLine();

            if (ExisteCPF(recebeCPF))
            {
                Console.SetCursorPosition(1, 5);
                Console.Write("Cliente: " + NomeCliente(recebeCPF));
            }
            else
            {
                Console.SetCursorPosition(1, 5);
                Console.Write("Este cliente não está cadastrado.");
                Console.SetCursorPosition(1, 6);
                Console.Write("F1 - Tentar novamente.");
                Console.SetCursorPosition(1, 7);
                Console.Write("F2 - Cadastrar Cliente.");
                Console.SetCursorPosition(1, 7);
                Console.Write("ESC - Voltar ao Menu Principal");

                ConsoleKeyInfo opc;
                opc = Console.ReadKey();

                switch (opc.Key)
                {
                    case ConsoleKey.F1:
                        {
                            Console.Clear();
                            Vendas();
                        }
                        break;
                    case ConsoleKey.F2:
                        {
                            Console.Clear();
                            ImagemMenuCliente(0);
                            CadastroCliente();
                            Vendas();
                        }
                        break;
                    case ConsoleKey.Escape:
                        {
                            Menu();
                        }
                        break;
                    default:
                        {
                            Console.Clear();
                            Vendas();
                        }
                        break;
                }
            }
            VendaProdutos(recebeCPF);
        }

        public static void VendaProdutos(string pCpf)
        {
            bool continuar = true;
            int linha = 12;
            int quantidade = 0;
            string idProduto = "";

            //Criar um dicionario para salvar uma STRING e uma INT
            //para poder acessar ao mesmo tempo o valor de ambas
            //Utilizado para realizar a compra de mais de um produto ao mesmo tempo
            //para assim que for calcular o preço total, obter o preço de cada item incluido
            //igualmente ao estoque, remover estoque especifico de tal produto de acordo com seu id
            //sendo que será atualizado de todos os ID/Quantidade que existirem preenchidos no dicionario
            Dictionary<string, int> dic = new Dictionary<string, int>();


            while (continuar)
            {
            //limpar espaço para não escrever em cima do que estava escrito antes
            //sem precisar de Console.Clear
            DigitarProduto://AREA DIGITAR PRODUTO
                Console.SetCursorPosition(1, 8);
                Console.Write("\t\t\t\t\t\t");
                Console.SetCursorPosition(1, 9);
                Console.Write("\t\t\t\t\t\t       ");
                Console.SetCursorPosition(1, 10);
                Console.Write("\t\t\t\t\t\t     ");

                Console.SetCursorPosition(1, 8);
                Console.Write("Digite o ID do produto: ");
                idProduto = Console.ReadLine();

                Console.SetCursorPosition(1, 9);
                Console.Write("Digite a quantidade de unidades: ");
                quantidade = Convert.ToInt32(Console.ReadLine());

                if (!EstoqueValido(idProduto, quantidade))
                {
                    Console.SetCursorPosition(1, 9);
                    Console.Write("\t\t\t\t\t\t       ");
                    Console.SetCursorPosition(1, 9);
                    Console.Write("Este produto está fora de estoque");
                    Console.SetCursorPosition(1, 10);
                    Console.Write("Pressione qualquer tecla para inserir outro produto");
                    Console.ReadKey();
                    goto DigitarProduto;//VOLTAR SEM PRECISAR DE CONSOLE.CLEAR
                }

                Desenhar.Retangulo(53, 20, 1, 11, ConsoleColor.Gray, 0);
                Console.SetCursorPosition(1, linha++);

                //ADICIONAR PRODUTO NA TELA E SOMAR O VALOR DELE AO PREÇO TOTAL
                IncluirProduto(idProduto, quantidade);

                //ADICIONAR PRODUTO AO DICIONARIO
                dic.Add(idProduto, quantidade);

                Console.SetCursorPosition(31, 31);
                Console.Write("Valor Total: " + precoTotal.ToString("C"));

                Console.SetCursorPosition(2, 33);
                Console.Write("ENTER  - Adicionar mais produtos");
                Console.SetCursorPosition(2, 34);
                Console.Write("ESPAÇO - Concluir venda");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Spacebar:
                        continuar = false;
                        break;
                }
            }


        //RECEBER QUANTIDADE EM DINHEIRO PARA CALCULAR O TROCO
        //Area para ser retornada com GOTO se necessário
        ReceberValor:
            //limpar espaço para não escrever em cima do que estava escrito antes
            //sem precisar de Console.Clear
            Console.SetCursorPosition(2, 37);
            Console.Write("\t\t\t\t\t\t");
            Console.SetCursorPosition(2, 39);
            Console.Write("\t\t\t\t\t\t");
            Console.SetCursorPosition(2, 40);
            Console.Write("\t\t\t\t\t\t  ");


            //RECEBER VALOR
            Console.SetCursorPosition(2, 37);
            Console.Write("Valor recebido: ");
            double recebido = Convert.ToDouble(Console.ReadLine());


            //VERIFICAR SE É POSSIVEL REALIZAR A VENDA
            if (recebido < precoTotal)
            {
                Console.SetCursorPosition(2, 39);
                Console.Write("Valor recebido não suficiente.");
                Console.SetCursorPosition(2, 40);
                Console.Write("Pressione qualquer tecla para inserir novamente");
                Console.ReadKey();
                goto ReceberValor;//SOLICITAR VALOR NOVAMENTE CASO VALOR RECEBIDO FOR MENOR QUE PREÇO
            }
            else
            {
                //CALCULAR E MOSTRAR O VALOR DO TROCO
                Console.SetCursorPosition(25, 37);
                Console.Write("Troco: " + (recebido - precoTotal).ToString("C"));

                Console.SetCursorPosition(2, 39);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Venda concluída com sucesso.");
                Console.ResetColor();

                string HoraData = DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString();

                //ADICIONAR VENDA ATUAL AO RELATORIO DE VENDAS
                double preco = 0;


                foreach (var str in dic)//percorrer uma variavel em todo o dicionario
                {
                    //Key e Value são funções do dicionário (funções já existentes/nativas)
                    //Key foi preenchido como ID do produto
                    //Value foi preenchido como quantidade
                    preco = Convert.ToDouble(PrecoProduto(str.Key)) * quantidade; //calcular preco de cada item/produto do dicionario, Key*quantidade
                    RegistrarRelatorio(new Relatorio(pCpf, NomeCliente(pCpf), NomeProduto(str.Key), str.Value.ToString(), preco.ToString(), HoraData)); //registrar um relatório para cada item do dicionario (key/value)
                    AtualizarEstoque(str.Key, "-", str.Value); //atualizar estoque para cada item do dicionario
                }

                SalvarRelatorio();

                Console.SetCursorPosition(2, 41);
                Console.Write("F1  - Realizar uma nova venda");
                Console.SetCursorPosition(2, 42);
                Console.Write("ESC - Voltar ao Menu Principal");

                ConsoleKeyInfo opc;
                opc = Console.ReadKey();

                switch (opc.Key)
                {
                    case ConsoleKey.F1:
                        {
                            Console.Clear();
                            precoTotal = 0;
                            Vendas();
                            VendaProdutos(pCpf);
                        }
                        break;
                    case ConsoleKey.Escape:
                        {
                            Menu();
                        }
                        break;
                }
            }
        }
        #endregion

        #region Relatório
        public static void Relatorio()
        {
            Console.Clear();
            Console.Title = "Livraria Liderança -> Vendas -> Relatório de Vendas";
            Console.CursorVisible = false;

            Console.SetWindowSize(100, 39);
            Desenhar.Retangulo(Console.WindowWidth - 2, Console.WindowHeight - 2, 0, 0, ConsoleColor.Gray, 0);

            string titulo = "Relatório de Vendas";
            Console.SetCursorPosition(1, 1);
            //Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t\t\t\t\t\t\t\t\t\t  ");
            Console.SetCursorPosition((Console.WindowWidth / 2) - (titulo.Length / 2) - 1, 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(titulo);
            Console.ResetColor();

            Console.SetCursorPosition(9, 4);
            Console.Write("Data");
            Console.SetCursorPosition(29, Console.CursorTop);
            Console.Write("CPF");
            Console.SetCursorPosition(43, Console.CursorTop);
            Console.Write("Nome");
            Console.SetCursorPosition(56, Console.CursorTop);
            Console.Write("Produto");
            Console.SetCursorPosition(73, Console.CursorTop);
            Console.Write("Quantidade");
            Console.SetCursorPosition(90, Console.CursorTop);
            Console.Write("Preço");

            CarregarRelatorio();

            Console.SetCursorPosition(2, 7);
            ListarVendas();

            Console.SetCursorPosition(2, Console.WindowHeight - 3);
            Console.Write("Pressione ESC para voltar ao Menu Principal");

            ConsoleKeyInfo opc;
            opc = Console.ReadKey();

            switch (opc.Key)
            {
                case ConsoleKey.Escape:
                    {
                        Console.Clear();
                        Menu();
                    }
                    break;
            }
        }
        #endregion


        #region Interface Gráfica
        static void TelaLogin()
        {
            Console.Title = "Livraria Liderança -> Login";
            Console.Clear();
            Desenhar.Retangulo(Console.WindowWidth - 1, Console.WindowHeight - 2, 0, 0, ConsoleColor.Cyan, 0);

            string[] logo = new string[8] { "        ██╗", "██╗     ╚═╝  ██████╗   ██████╗ ██████╗   █████╗  ███╗   ██╗ ██████╗  █████╗ ", "██║     ██╗  ██╔═══██╗ ██╔═══╝ ██╔══██╗ ██╔══██╗ ██╠██╗ ██║ ██╔═══╝ ██╔══██╗", "██║     ██║  ██║   ██║ ████╗   ██████╔╝ ███████║ ██║╚██╗██║ ██║     ███████║", "██╚═══╗ ██║  ██║   ██║ ██╔═╝   ██╔══██╗ ██╔══██║ ██║  ╚███║ ██║     ██╔══██║", "██████║ ██║  ██████╔═╝ ██████╗ ██║  ██║ ██║  ██║ ██║   ╚██║ ██████╗ ██║  ██║", "╚═════╝ ╚═╝  ╚═════╝   ╚═════╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝    ╚═╝ ╚═██╔═╝ ╚═╝  ╚═╝", "                                                              ╚═╝" };

            for (int i = 0; i < logo.Length; i++)
            {
                Console.SetCursorPosition(1, i + 3);
                Console.WriteLine(logo[i]);
            }

            Console.SetCursorPosition(((Console.WindowWidth / 2) - 13), ((Console.WindowHeight / 2) + 1));
            Console.Write("Usuario");
            Desenhar.Retangulo(15, 1, ((Console.WindowWidth / 2) - 5), (Console.WindowHeight / 2), ConsoleColor.Gray, 0);

            Console.SetCursorPosition(((Console.WindowWidth / 2) - 13), (Console.WindowHeight / 2 + 4));
            Console.Write("Senha");
            Desenhar.Retangulo(15, 1, ((Console.WindowWidth / 2) - 5), ((Console.WindowHeight / 2) + 3), ConsoleColor.Gray, 0);
        }

        public static void ImagemMenu(int opcao)
        {
            Console.SetWindowSize(100, 39);
            Console.CursorVisible = false;
            string titulo = "Livraria Liderança";

            Desenhar.Retangulo(20, 37, 0, 0, ConsoleColor.Gray, 0);
            Console.SetCursorPosition(1, 1);
            ////Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\t Menu\t    ");
            Console.ResetColor();

            string[] opcoes = new string[5] { "F1 ► AJUDA", "F2 ► CLIENTES", "F3 ► PRODUTOS", "F4 ► VENDAS", "F12 ► SAIR" };
            Console.SetCursorPosition((Console.WindowWidth / 2) - titulo.Length / 2, 0);
            Console.Write(titulo);

            Desenhar.Retangulo(20, 7, 0, 2, ConsoleColor.Gray, 4);
            Desenhar.Retangulo(20, 7, 0, 18, ConsoleColor.Gray, 4);
            Desenhar.Retangulo(20, 7, 0, 26, ConsoleColor.Gray, 4);
            int n = 6;

            for (int i = 0; i < opcoes.Length; i++)
            {
                Console.SetCursorPosition(10 - (opcoes[i].Length / 2), n);
                if (opcao > opcoes.Length)
                {
                    //MOTIVO DE USAR ImagemMenu(10) 
                    //se 'opcao' for > que a quantidade de opções
                    //escrever todas as opções cor a cor padrão do console
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(opcoes[i]);
                }
                else if (opcao != i)
                {
                    //se 'opcao' for diferente do indice da opção
                    //colorir o texto de cinza escuro para simular um efeito
                    //de "não acessivel"
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(opcoes[i]);
                }
                else if (opcao == i)
                {
                    //se 'opcao' for igual ao indice da opção
                    //colorir o texto de azul claro
                    //para simular um efeito de "selecionado"
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(opcoes[i]);
                    Console.ResetColor();
                }

                //as mesmas informações dos comentarios acima
                //se aplicam a ImagemMenuClientes, ImagemMenuProdutos e ImagemMenuVendas

                //calculos para centralizar as posições nos retangulos
                if (i != 3)
                    n += 8;
                else
                    n += 6;//menos espaço no ultimo pois retangulo de sair é menor
            }
        }

        public static void QuadradoDefault()
        {
            Desenhar.Retangulo(55, 34, 42, 2, ConsoleColor.Gray, 0);
        }
        public static void ImagemMenuCliente(int opcao)
        {
            string[] opcoes = new string[3] { "F8 ► CADASTRO", "F9 ► CONSULTA", "F10 ► EXCLUSÃO" };

            ImagemMenu(1);

            Desenhar.Retangulo(20, 7, 20, 10, ConsoleColor.Gray, 5);
            Desenhar.Retangulo(20, 7, 20, 18, ConsoleColor.Gray, 6);
            Desenhar.Retangulo(20, 7, 20, 26, ConsoleColor.Gray, 7);

            int n = 6;
            for (int i = 0; i < opcoes.Length; i++)
            {
                n += 8;
                Console.SetCursorPosition(30 - (opcoes[i].Length / 2), n);


                if (opcao > opcoes.Length)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(opcoes[i]);
                }
                else if (opcao != i)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(opcoes[i]);
                }
                else if (opcao == i)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(opcoes[i]);
                    Console.ResetColor();
                }
            }
            Console.ResetColor();
        }
        public static void TelaCadastroCliente()
        {
            Console.Title = "Livraria Liderança -> Clientes -> Cadastro";
            Console.ResetColor();

            QuadradoDefault();

            Console.SetCursorPosition(43, 3);
            //Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t\t\t\t\t ");
            string title = "Cadastro de Clientes";
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(70 - title.Length / 2, 3);
            Console.Write(title);
            Console.ResetColor();
        }

        public static void TelaConsultaCliente()
        {
            Console.Title = "Livraria Liderança -> Clientes -> Consulta";

            QuadradoDefault();

            Console.SetCursorPosition(43, 3);
            //Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t\t\t\t\t ");
            string title = "Consulta de Clientes";
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(70 - title.Length / 2, 3);
            Console.Write(title);
            Console.ResetColor();
        }

        public static void TelaEditarCliente()
        {
            Console.Title = "Livraria Liderança -> Clientes -> Editar";

            QuadradoDefault();

            Console.SetCursorPosition(43, 3);
            //Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t\t\t\t\t ");
            string title = "Edição de Clientes";
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(70 - title.Length / 2, 3);
            Console.Write(title);
            Console.ResetColor();
        }

        public static void TelaExcluirCliente()
        {
            Console.Title = "Livraria Liderança -> Clientes -> Excluir";

            QuadradoDefault();

            Console.SetCursorPosition(43, 3);
            //Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t\t\t\t\t ");
            string title = "Exclusão de Clientes";
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(70 - title.Length / 2, 3);
            Console.Write(title);
            Console.ResetColor();
        }
        public static void ImagemMenuProdutos(int opcao)
        {

            string[] opcoes = new string[2] { "F8 ► CONSULTA", "F9 ► EDIÇÃO" };

            ImagemMenu(2);

            Desenhar.Retangulo(20, 7, 20, 18, ConsoleColor.Gray, 5);
            Desenhar.Retangulo(20, 7, 20, 26, ConsoleColor.Gray, 7);

            int n = 14;
            for (int i = 0; i < opcoes.Length; i++)
            {
                n += 8;
                Console.SetCursorPosition(30 - (opcoes[i].Length / 2), n);


                if (opcao > opcoes.Length)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(opcoes[i]);
                }
                else if (opcao != i)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(opcoes[i]);
                }
                else if (opcao == i)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(opcoes[i]);
                    Console.ResetColor();
                }
            }
        }

        public static void TelaConsultaProduto()
        {
            Console.Title = "Livraria Liderança -> Produtos -> Consulta";
            Console.ResetColor();

            QuadradoDefault();

            Console.SetCursorPosition(43, 3);
            //Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t\t\t\t\t ");
            string title = "Consulta de Estoque";
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(70 - title.Length / 2, 3);
            Console.Write(title);
            Console.ResetColor();
        }

        public static void TelaEditarProduto()
        {
            Console.Title = "Livraria Liderança -> Produtos -> Editar Produto";
            Console.ResetColor();

            QuadradoDefault();

            Console.SetCursorPosition(43, 3);
            //Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t\t\t\t\t ");
            string title = "Editar Produto";
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(70 - title.Length / 2, 3);
            Console.Write(title);
            Console.ResetColor();
        }

        public static void Gambiarra() //Linhas verticais na tela de vendas
        {
            string[] linha = new string[4] { "┬", "│", "┼", "┴" };

            int n = 0;
            // topo
            Console.SetCursorPosition(78, n);
            Console.Write(linha[0]);
            Console.SetCursorPosition(98, n);
            Console.Write(linha[0]);

            for (int i = 0; i < Console.WindowHeight - 2; i++)
            {
                Console.SetCursorPosition(78, ++n);
                Console.Write(linha[1]);
            }
            n = 0;
            for (int i = 0; i < Console.WindowHeight - 2; i++)
            {
                Console.SetCursorPosition(98, ++n);
                Console.Write(linha[1]);
            }

            Console.SetCursorPosition(78, 6);
            Console.Write(linha[2]);
            Console.SetCursorPosition(98, 6);
            Console.Write(linha[2]);

            Console.SetCursorPosition(78, Console.WindowHeight - 1);
            Console.Write(linha[3]);
            Console.SetCursorPosition(98, Console.WindowHeight - 1);
            Console.Write(linha[3]);
        }

        public static void ImagemMenuVendas(int opcao)
        {

            string[] opcoes = new string[2] { "F8 ► VENDER", "F9 ► RELATÓRIO" };

            ImagemMenu(3);

            Desenhar.Retangulo(20, 7, 20, 18, ConsoleColor.Gray, 5);
            Desenhar.Retangulo(20, 7, 20, 26, ConsoleColor.Gray, 7);

            int n = 14;
            for (int i = 0; i < opcoes.Length; i++)
            {
                n += 8;
                Console.SetCursorPosition(30 - (opcoes[i].Length / 2), n);


                if (opcao > opcoes.Length)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(opcoes[i]);
                }
                else if (opcao != i)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(opcoes[i]);
                }
                else if (opcao == i)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(opcoes[i]);
                    Console.ResetColor();
                }
            }
        }

        public static void TelaVendas()
        {
            Console.Title = "Livraria Liderança -> Vendas -> Realizar Venda";

            Console.CursorVisible = false;
            Console.SetWindowSize(120, 45);

            Desenhar.Retangulo(55, 43, 0, 0, ConsoleColor.Gray, 0);
            Desenhar.Retangulo(60, 43, 58, 0, ConsoleColor.Gray, 0);
            Desenhar.Retangulo(60, 5, 58, 0, ConsoleColor.Gray, 10);

            string titulo = "Vendas";
            Console.SetCursorPosition(1, 1);
            //Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t\t\t\t       ");
            Console.SetCursorPosition(28 - titulo.Length / 2, 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(titulo);
            Console.ResetColor();


            Gambiarra();

            string[] linha = new string[3] { "Infantis", "Mangá", "Herois" };

            int n = 59;

            for (int i = 0; i < linha.Length; i++)
            {
                Console.SetCursorPosition((n + 9) - (linha[i].Length / 2), 3);
                Console.Write(linha[i]);
                n += 20;
            }
        }
        #endregion

    }
}
