using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente
    {
        public string id { get; set; }
        public string nome { get; set; }
        public DateTime dtNascimento { get; set; }
        public string regiao { get; set; }
        public int qtdPontos { get; set; }
        public int qtdOuro { get; set; }
        public int qtdPrata { get; set; }
        public int qtdBronze { get; set; }

        public Cliente() { }

        public Cliente(string id, string nome, DateTime dtNascimento, string regiao, int qtdPontos)
        {
            this.id = id;
            this.nome = nome;
            this.dtNascimento = dtNascimento;
            this.regiao = regiao;
            this.qtdPontos = qtdPontos;
            this.qtdOuro = qtdPontos / 10000;
            this.qtdPrata = (qtdPontos % 10000) / 1000;
            this.qtdBronze = (qtdPontos % 1000) / 100;
        }

        public int GetIdade()
        {
            var dtAgora = DateTime.Now;
            var idade = dtAgora.Year - dtNascimento.Year;

            if (dtAgora.Month < dtNascimento.Month || (dtAgora.Month == dtNascimento.Month && dtAgora.Day < dtNascimento.Day))
                idade--;

            return idade;
        }
    }
}
