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

                ConsoleColor cor = ConsoleColor.Yellow;

                WriteColor($"Sistema de {AppTitle.ToLower()}");
                WriteColor("  [1] - Cadastrar veículo", cor);
                WriteColor("  [2] - Remover veículo", cor);
                WriteColor("  [3] - Listar veículos", cor);
                WriteColor("  [4] - Encerrar", cor);

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
                        WriteColor("  [Opção inválida]", ConsoleColor.Red);
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
        /// Método criado para escrever na tela com parte do texto colorido.
        /// </summary>
        /// <param name="mensagem">A mensagem que será impressa.</param>
        /// <param name="cor">A cor do texto.</param>
        /// <param name="breakline">Falso para não querbrar a linha.</param>
        public static void WriteColor(string mensagem = "", ConsoleColor cor = default, bool breakline = true)
        {
            string[] partes = Regex.Split(mensagem, @"(\[[^\]]*\])");

            for (int i = 0; i < partes.Length; i++)
            {
                string parte = partes[i];

                if (parte.StartsWith("[") && parte.EndsWith("]"))
                {
                    Console.ForegroundColor = cor;
                    parte = parte.Substring(1, parte.Length - 2);
                }
                
                Console.Write(parte);
                Console.ResetColor();
            }

            if (breakline)
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
