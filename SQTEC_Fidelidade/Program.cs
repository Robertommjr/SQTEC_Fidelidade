using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Dominio;
using Servico;

namespace SQTEC_Fidelidade
{
    class Program
    {
        internal static readonly ClienteService _clienteService = new ClienteService();

        static void Main(string[] args)
        {
            try
            {
                var listaClientes = _clienteService.AtualizaDados(_clienteService.LerArquivo());

                foreach (var cliente in listaClientes.OrderBy(c => c.nome))
                {
                    Console.WriteLine((cliente.nome + " - " + cliente.regiao).PadRight(35, ' ') + " | " + cliente.GetIdade().ToString().PadLeft(3, ' ') + " anos | Ouro: " + cliente.qtdOuro.ToString().PadLeft(4, ' ') + " | Prata: " + cliente.qtdPrata.ToString().PadLeft(2, ' ') + " | Bronze: " + cliente.qtdBronze.ToString().PadLeft(2, ' '));
                }

                Console.WriteLine();

                List<Cliente> result = listaClientes
                    .GroupBy(c => c.regiao)
                    .Select(cl => new Cliente
                    {
                        regiao = cl.First().regiao,
                        qtdOuro = cl.Sum(c => c.qtdOuro),
                        qtdPrata = cl.Sum(c => c.qtdPrata),
                        qtdBronze = cl.Sum(c => c.qtdBronze),
                    }).ToList();

                foreach (var cliente in result.OrderBy(c => c.regiao))
                {
                    Console.WriteLine(cliente.regiao.PadRight(20, ' ') + " | Ouro: " + cliente.qtdOuro.ToString().PadLeft(4, ' ') + " | Prata: " + cliente.qtdPrata.ToString().PadLeft(2, ' ') + " | Bronze: " + cliente.qtdBronze.ToString().PadLeft(2, ' '));
                }

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
