using MarkdownLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinq.Dojo.Crud
{
    class Program
    {
        static IClienteController controller = new ClienteController(new BancoDeDados());

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ");
            Console.WriteLine(" ██████╗██╗███╗   ██╗ ██████╗     ██████╗  ██████╗      ██╗ ██████╗      ██████╗██████╗ ██╗   ██╗██████╗ ");
            Console.WriteLine("██╔════╝██║████╗  ██║██╔═══██╗    ██╔══██╗██╔═══██╗     ██║██╔═══██╗    ██╔════╝██╔══██╗██║   ██║██╔══██╗");
            Console.WriteLine("██║     ██║██╔██╗ ██║██║   ██║    ██║  ██║██║   ██║     ██║██║   ██║    ██║     ██████╔╝██║   ██║██║  ██║");
            Console.WriteLine("██║     ██║██║╚██╗██║██║▄▄ ██║    ██║  ██║██║   ██║██   ██║██║   ██║    ██║     ██╔══██╗██║   ██║██║  ██║");
            Console.WriteLine("╚██████╗██║██║ ╚████║╚██████╔╝    ██████╔╝╚██████╔╝╚█████╔╝╚██████╔╝    ╚██████╗██║  ██║╚██████╔╝██████╔╝");
            Console.WriteLine(" ╚═════╝╚═╝╚═╝  ╚═══╝ ╚══▀▀═╝     ╚═════╝  ╚═════╝  ╚════╝  ╚═════╝      ╚═════╝╚═╝  ╚═╝ ╚═════╝ ╚═════╝ ");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
            PrintMenu();

            Console.WriteLine("[INFO] Programa finalizado, tecle ENTER para sair");
            Console.ReadKey();
        }

        static void PrintMenu()
        {
            Console.WriteLine(" ");
            Console.WriteLine("Selecione a opção abaixo:");
            Console.WriteLine("1 - Cadastrar novo cliente");
            Console.WriteLine("2 - Pesquisar cliente");
            Console.WriteLine("3 - Deletar cliente");
            Console.WriteLine("4 - Relatório de todos os clientes");
            Console.WriteLine("5 - Fechar Programa");

            try
            {
                var opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        var cliente = new Cliente();
                        Console.WriteLine("---------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("Informe Nome do cliente:");
                        cliente.Nome = Console.ReadLine();
                        Console.WriteLine("Informe Idade do cliente:");
                        cliente.Idade = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Informe Endereco do cliente:");
                        cliente.Endereco = Console.ReadLine();
                        Console.WriteLine("Informe Email do cliente:");
                        cliente.Email = Console.ReadLine();
                        Console.WriteLine("Informe CPF do cliente:");
                        cliente.CPF = Console.ReadLine();
                        Console.WriteLine("Informe Telefone do cliente:");
                        cliente.Telefone = Console.ReadLine();
                        controller.CadastraCliente(cliente);
                        PrintReport(controller.RetornaTodosClientes());
                        PrintMenu();
                        break;
                    case "2":
                        Console.WriteLine("---------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("Informe CPF do cliente:");
                        var cpf1 = Console.ReadLine();
                        var cliente1 = controller.PesquisaCliente(cpf1);
                        PrintReport(new List<Cliente> { cliente1 });
                        PrintMenu();
                        break;
                    case "3":
                        Console.WriteLine("---------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("Informe CPF do cliente:");
                        var cpf2 = Console.ReadLine();
                        controller.DeletaCliente(cpf2);
                        PrintReport(controller.RetornaTodosClientes());
                        PrintMenu();
                        break;
                    case "4":
                        Console.WriteLine("---------------------------------------------------------------------------------------------------------");
                        PrintReport(controller.RetornaTodosClientes());
                        PrintMenu();
                        break;
                    case "5":
                        break;
                    default:
                        Console.WriteLine("ERRO: Opção inválida");
                        PrintMenu();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERRO: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                PrintMenu();
            }
        }

        static void PrintReport(IEnumerable<Cliente> list)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(

             list.Select(s => new
             {
                 CPF = s.CPF,
                 Nome = s.Nome,
                 Idade = s.Idade,
                 Telefone = s.Telefone,
                 Email = s.Email,
                 Endereco = s.Endereco                 
             }).ToMarkdownTable());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
