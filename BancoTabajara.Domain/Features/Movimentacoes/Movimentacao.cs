using BancoTabajara.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Movimentacoes
{
    public class Movimentacao : Entidade
    {
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public TipoOperacaoEnum TipoOperacao { get; set; }
        

        public override void Validar()
        {
            if (Valor <= 0)
                throw new MovimentacaoValorInvalidoException();
            if (TipoOperacao.GetHashCode() < 1 || TipoOperacao.GetHashCode() > 2)
                throw new MovimentacaoTipoOperacaoInvalidaException();
            if (!Data.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                throw new MovimentacaoDataInvalidaException();
        }

        public void ObterMovimentacao(TipoOperacaoEnum tipoOperacao, double valor)
        {
            TipoOperacao = tipoOperacao;
            Valor = valor;
            Data = DateTime.Now;
        }
    }
}
