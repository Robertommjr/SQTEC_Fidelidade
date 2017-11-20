using Dominio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servico
{
    public class ClienteService
    {
        public List<Cliente> LerArquivo()
        {
            string linha;
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

            return listaClientes;
        }

        public List<Cliente> LerDados()
        {
            string linha;
            List<Cliente> listaClientes = new List<Cliente>();

            //Cria um novo StreamReader passando o nome do arquivo que será lido. 
            //Como o arquivo vai estar no mesmo diretório que o .exe, não é necessário passar o caminho.
            StreamReader sr = new StreamReader("data.json");

            //Lendo a primeira linha do arquivo
            linha = sr.ReadLine();

            //Caso o valor lido não seje nulo, continua lendo as demais linha até o fim do arquivo.
            if (linha != null)
            {
                listaClientes = JsonConvert.DeserializeObject<List<Cliente>>(linha);
            }

            //fecha o arquivo
            sr.Close();

            return listaClientes;
        }

        public void SalvarDados(List<Cliente> listaClientes)
        {
            //open file stream
            using (StreamWriter file = File.CreateText("data.json"))
            {
                JsonSerializer serializer = new JsonSerializer();

                //serialize object directly into file stream
                serializer.Serialize(file, listaClientes);
            }
        }

        public List<Cliente> AtualizaDados(List<Cliente> lstClientesArquivo)
        {
            var lstClientesDados = LerDados();

            foreach (var cliente in lstClientesArquivo)
            {
                var clienteDados = lstClientesDados.Where(c => c.id == cliente.id).FirstOrDefault();
                if (clienteDados != null)
                {
                    lstClientesDados[lstClientesDados.IndexOf(clienteDados)] = cliente;
                }
                else
                {
                    lstClientesDados.Add(cliente);
                }
            }

            SalvarDados(lstClientesDados);

            return lstClientesDados;
        }
    }
}
