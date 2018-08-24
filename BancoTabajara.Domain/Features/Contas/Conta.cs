using BancoTabajara.Domain.Base;
using BancoTabajara.Domain.Features.Clientes;
using BancoTabajara.Domain.Features.Extratos;
using BancoTabajara.Domain.Features.Movimentacoes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Contas
{
    public class Conta : Entidade
    {
        public int NumeroConta { get; set; }
        public double Saldo { get; internal set; }
        public bool Ativada { get; set; }
        public double Limite { get; set; }
        public double SaldoTotal { get { return CalcularSaldo(); } }

        public virtual Cliente Cliente { get; set; }
        public virtual List<Movimentacao> Movimentacoes { get; set; }

        public Conta()
        {
            Movimentacoes = new List<Movimentacao>();
        }

        public override void Validar()
        {
            if (NumeroConta <= 0)
                throw new ContaNumeroContaInvalidaException();

            if (SaldoTotal < 0)
                throw new ContaSaldoMenorQueZeroException();

            if (Limite < 0)
                throw new ContaLimiteInvalidoException();

            if (Cliente == null)
                throw new ContaClienteInvalidoException();
        }

        private double CalcularSaldo()
        {
            return Limite + Saldo;
        }

        public void AlterarEstado()
        {
            Ativada = !Ativada;
        }

        public void RealizarDeposito(double valor)
        {
            Movimentacao movimentacao = new Movimentacao();
            Saldo += valor;
            movimentacao.ObterMovimentacao(TipoOperacaoEnum.Credito, valor);
            Movimentacoes.Add(movimentacao);
        }

        public void RealizarSaque(double valor)
        {
            Movimentacao movimentacao = new Movimentacao();
            if (SaldoTotal >= valor)
            {
                Saldo -= valor;
                movimentacao.ObterMovimentacao(TipoOperacaoEnum.Debito, valor);
                Movimentacoes.Add(movimentacao);
            }
            else
            {
                throw new ContaSaldoInsuficienteException();
            }
        }

        public void RealizarTransferencia(double valor, Conta destino)
        {
            RealizarSaque(valor);
            destino.RealizarDeposito(valor);
        }

        public void VerificaNumeroConta(int numeroAntigo)
        {
            if (NumeroConta != numeroAntigo)
            {
                throw new ContaNumeroAlteradoException();
            }
        } 

        public Extrato GerarExtrato()
        {
            var extrato = new Extrato();
            return extrato.ObterExtrato(this);
        }
    }
}
