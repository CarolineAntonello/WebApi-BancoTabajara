using BancoTabajara.Domain.Features.Contas;
using BancoTabajara.Domain.Features.Movimentacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Extratos
{
    public class Extrato
    {
        public int NumeroConta { get; set; }
        public DateTime DataEmissao { get; set; }
        public string NomeCliente { get; set; }
        public double Saldo { get; set; }
        public List<Movimentacao> Movimentacoes { get; set; }
        public double Limite { get; set; }
        

        public Extrato ObterExtrato(Conta conta)
        {
            NumeroConta = conta.NumeroConta;
            DataEmissao = DateTime.Now;
            NomeCliente = conta.Cliente.Nome;
            Limite = conta.Limite;
            Saldo = conta.Saldo;
            Movimentacoes = conta.Movimentacoes.ToList();
            return this;
        }
    }
}
