using static DesafioFundamentos.Program;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Title("Cadastrando veículo...");

            Console.Clear();
            WriteColor("Digite a placa do veículo para estacionar: ", default, false);

            string placa = ReadLine();

            if (CheckValorInformadoVazio(placa))
            {
                AdicionarVeiculo();
            }
            else
            {
                Console.Clear();

                if (TemVeiculo(placa, false) == false)
                {
                    Title("Veículo cadastrado!");

                    veiculos.Add(placa.ToUpper());

                    WriteColor($"  O veículo de placa [{placa.ToUpper()}]" +
                               $" foi adicionado com sucesso!", ConsoleColor.Yellow);
                }
            }
        }

        public void RemoverVeiculo()
        {
            ConsoleColor cor = ConsoleColor.Yellow;

            if (TemVeiculo())
            {
                Console.Clear();

                ListarVeiculos();

                Title("Removendo veículo...");

                WriteColor();

                WriteColor("Digite a placa do veículo do qual deseja remover: ", default, false);

                string placa = ReadLine();
                
                if (CheckValorInformadoVazio(placa))
                {
                    RemoverVeiculo();

                    return;
                }

                // Verifica se o veículo existe
                if (TemVeiculo(placa.ToUpper()))
                {
                    Console.Clear();

                    placa = placa.ToUpper();

                    WriteColor($"Removendo veículo de placa: [{placa}]", cor);
                    WriteColor("Digite a quantidade de horas que o veículo permaneceu estacionado: ", cor, false);

                    string horaInformada = ReadLine();

                    if (CheckValorInformadoVazio(horaInformada))
                    {
                        RemoverVeiculo();
                    }
                    else
                    {
                        Console.Clear();

                        if (horaInformada != null)
                        {
                            Title("Veículo removido!");

                            int horas = int.Parse(horaInformada);
                            string valorTotal = $"R$ {precoInicial + precoPorHora * horas:N}";
                            
                            veiculos.Remove(placa);

                            WriteColor($"  O veículo de placa [{placa}] foi removido" +
                                       $" e o custo total foi de [{valorTotal}].", cor);
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    WriteColor($"Desculpe, o veículo de placa [{placa}] não está estacionado aqui. " +
                               $"Confira se digitou a placa corretamente.", ConsoleColor.Yellow);
                }
            }
        }

        public void ListarVeiculos()
        {
            Title("Listando veículo...");

            // Verifica se há veículos no estacionamento
            if (TemVeiculo())
            {
                WriteColor("Os veículos estacionados são:");

                foreach (string placa in veiculos)
                {
                    WriteColor($"  • [{placa}];", ConsoleColor.White);
                }
            }
        }

        public bool TemVeiculo(string placa = null, bool mostrarMensagem = true)
        {
            bool _return;
            string _mensagem = string.Empty;

            if (string.IsNullOrEmpty(placa))
            {
                _return = veiculos.Any();

                if (_return == false) _mensagem = "  Não há veículos estacionados.";
            }
            else
            {
                _return = veiculos.Contains(placa.ToUpper());
                
                string texto = _return ? "já" : "não";

                _mensagem = $"  O veículo de placa [{placa.ToUpper()}] {texto} se encontra no estacionamento.";

            }

            if (mostrarMensagem || _return)
            {
                if (!string.IsNullOrEmpty(_mensagem))
                    WriteColor(_mensagem, ConsoleColor.Yellow);

                Console.ResetColor();
            }

            return _return;
        }
    }
}
