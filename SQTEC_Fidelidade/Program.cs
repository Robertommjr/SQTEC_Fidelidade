using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Dominio;

namespace SQTEC_Fidelidade
{
    class Program
    {
        static void Main(string[] args)
        {
            String linha;

            try
            {
                //Cria um novo StreamReader passando o nome do arquivo que será lido. 
                //Como o arquivo vai estar no mesmo diretório que o .exe, não é necessário passar o caminho.
                StreamReader sr = new StreamReader("clientes.txt");
                List<Cliente> listaClientes = new List<Cliente>();

                //Lendo a primeira linha do arquivo
                linha = sr.ReadLine();

                //Caso o valor lido não seje nulo, continua lendo as demais linha até o fim do arquivo.
                while (linha != null)
                {
                    var linhaSplit = linha.Split(';');
                    listaClientes.Add(new Cliente(linhaSplit[0], linhaSplit[1], DateTime.Parse(linhaSplit[2]), linhaSplit[3], int.Parse(linhaSplit[4])));

                    //Read the next line
                    linha = sr.ReadLine();
                }

                //close the file
                sr.Close();

                foreach (var cliente in listaClientes.OrderBy(c => c.nome))
                {
                    Console.WriteLine((cliente.nome + " - " + cliente.regiao).PadRight(35, ' ') + " | " + cliente.GetIdade().ToString().PadLeft(3, ' ') + " anos | Ouro: " + cliente.qtdOuro.ToString().PadLeft(4, ' ') + " | Prata: " + cliente.qtdPrata.ToString().PadLeft(2, ' ') + " | Bronze: " + cliente.qtdBronze.ToString().PadLeft(2, ' '));
                }

                Console.WriteLine();
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

                //open file stream
                using (StreamWriter file = File.CreateText("data.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    //serialize object directly into file stream
                    serializer.Serialize(file, listaClientes);
                }

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
