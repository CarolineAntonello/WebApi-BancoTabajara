using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancoTabajara.Domain.Features.Movimentacoes;

namespace BancoTabajara.Common.Tests.Features.Movimentacoes
{
    public class MovimentacaoObjectMother
    {
        public static Movimentacao GetMovimentacao()
        {
            return new Movimentacao
            {
                Valor = 2000,
                Data = DateTime.Now,
                TipoOperacao = TipoOperacaoEnum.Credito
            };
        }

        public static Movimentacao GetMovimentacaoSemTipoOperacao()
        {
            return new Movimentacao
            {
                Valor = 2000,
                Data = DateTime.Now
            };
        }
    }
}
