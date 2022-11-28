using System.Text;
using System.Text.RegularExpressions;
using DesafioFundamentos.Models;

namespace DesafioFundamentos
{
    internal class Program
    {
        private static string AppTitle = "Estacionamento";
        private static decimal precoInicial = 0;
        private static decimal precoPorHora = 0;

        static void Main(string[] args)
        {
            // Coloca o encoding para UTF8 para exibir acentuação
            Console.OutputEncoding = Encoding.UTF8;

            ConsoleColor red = ConsoleColor.Red;
            ConsoleColor gray = ConsoleColor.Gray;
            ConsoleColor blue = ConsoleColor.Blue;
            ConsoleColor yellow = ConsoleColor.Yellow;

            Title("Bem-vindo");

            string bemVindo = "Seja bem vindo ao sistema de estacionamento!";

            LimparConsole();
            Console.WriteLine(bemVindo);

            if (precoInicial == 0)
            {
                LimparConsole();
                Console.Write($"{bemVindo}\n Digite o preço inicial: ");

                if (decimal.TryParse(ReadLine(), out precoInicial) == false)
                {
                    LimparConsole();
                    Console.WriteLine(" Desculpe, você precisa informar um número para o preço inicial.");

                    Pause();

                    Main(args);

                    return;
                }
            }

            if (precoPorHora == 0)
            {
                LimparConsole();
                Console.Write($"{bemVindo}\n Agora digite o preço por hora: ");

                if (decimal.TryParse(ReadLine(), out precoPorHora) == false)
                {
                    LimparConsole();
                    Console.WriteLine(" Desculpe, você precisa informar um número para o preço por hora.");

                    Pause();

                    Main(args);

                    return;
                }
            }

            // Instancia a classe Estacionamento, já com os valores obtidos anteriormente
            Estacionamento es = new Estacionamento(precoInicial, precoPorHora);

            bool exibirMenu = true;

            // Realiza o loop do menu
            while (exibirMenu)
            {
                Title("Menu");

                LimparConsole();

                ConsoleColor[] cores = new ConsoleColor[] { blue, gray, yellow };

                WriteColor($"Sistema de {AppTitle.ToLower()}", ConsoleColor.White);
                WriteColor("  [1] [-] [Cadastrar veículo]", cores: cores);
                WriteColor("  [2] [-] [Remover veículo]", cores: cores);
                WriteColor("  [3] [-] [Listar veículos]", cores: cores);
                WriteColor("  [4] [-] [Encerrar]", cores: cores);

                WriteColor();

                WriteColor("Escolha uma opção: ");

                ConsoleKeyInfo opcao = Console.ReadKey(true);

                Console.Clear();

                switch (opcao.KeyChar.ToString())
                {
                    case "1":
                        es.AdicionarVeiculo();
                        break;

                    case "2":
                        es.RemoverVeiculo();
                        break;

                    case "3":
                        es.ListarVeiculos();
                        break;

                    case "4":
                        exibirMenu = false;
                        break;

                    default:
                        WriteColor("  [Opção inválida]", red);
                        break;
                }

                if (exibirMenu) Pause();
            }

            LimparConsole();
            Console.WriteLine("O programa se encerrou");
        }

        public static void Title(string title) => Console.Title = $"DIO: {AppTitle} | {title}";

        /// <summary>
        /// Método criado para verificar se o valor informado é vazio.
        /// </summary>
        /// <param name="valorInformado"></param>
        /// <returns>Retorna <c>true</c> se o parâmetro for nulo ou vazio.</returns>
        public static bool CheckValorInformadoVazio(string valorInformado)
        {
            bool isNullOrEmpty = string.IsNullOrEmpty(valorInformado);

            if (isNullOrEmpty)
            {
                LimparConsole();
                WriteColor("Desculpe, você precisa informar um valor.");

                Pause();
            }

            return isNullOrEmpty;
        }

        /// <summary>
        /// Método criado para exibir uma mensagem na tela e aguarda o usuário pressionar qualquer tecla.
        /// </summary>
        public static void Pause()
        {
            Console.ResetColor();
            WriteColor("\nPressione qualquer tecla para continuar ou ESC para encerrar.");

            ConsoleKeyInfo tecla = Console.ReadKey(true);

            if (tecla.Key == ConsoleKey.Escape)
                Environment.Exit(0);
        }

        /// <summary>
        /// Método criado para ler a entrada na cor amarela.
        /// </summary>
        /// <returns>A entrada</returns>
        public static string ReadLine()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            string x = Console.ReadLine();

            Console.ResetColor();

            return x;
        }

        /// <summary>
        /// Método criado para escrever na tela o texto colorido.
        /// </summary>
        /// <example>Exemplo de uso:
        /// <code>
        /// WriteColor(
        ///     mensagem: "[Este] é [apenas] um [teste]...",
        ///     corPadrao: ConsoleColor.DarkGreen,
        ///     cores: new ConsoleColor[]{
        ///         ConsoleColor.Red,
        ///         ConsoleColor.Yellow,
        ///         ConsoleColor.Magenta
        ///     }
        /// );
        /// </code>
        /// </example>
        /// <param name="mensagem">A mensagem que será impressa.</param>
        /// <param name="corPadrao">A cor do texto em geral.</param>
        /// <param name="cores">Uma lista de cores para cada texto entre colchetes.</param>
        /// <param name="quebrarLinha">Falso para não quebrar a linha.</param>
        public static void WriteColor(string mensagem = "", ConsoleColor corPadrao = ConsoleColor.Gray, ConsoleColor[] cores = null, bool quebrarLinha = true)
        {
            int indice = 0;

            string[] partes = Regex.Split(mensagem, @"(\[[^\]]*\])");

            Console.ForegroundColor = corPadrao;

            for (int i = 0; i < partes.Length; i++)
            {
                string parte = partes[i];

                if (parte.StartsWith("[") && parte.EndsWith("]"))
                {
                    if (cores != null) {
                        if (indice < cores.Length)
                            Console.ForegroundColor = cores[indice];
                        else
                            Console.ForegroundColor = cores[0];
                    }

                    parte = parte.Substring(1, parte.Length - 2);
                    indice++;
                }

                Console.Write(parte);
                Console.ForegroundColor = corPadrao;
            }

            if (quebrarLinha)
                Console.WriteLine();
        }

        /// <summary>
        /// Limpa a tela do console e altera a cor do próximo texto para branco.
        /// </summary>
        public static void LimparConsole()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
