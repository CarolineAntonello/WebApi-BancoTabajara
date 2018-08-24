using BancoTabajara.Domain.Features.Movimentacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Applications.Features.Movimentacoes.ViewModel
{
    public class MovimentacaoViewModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public TipoOperacaoEnum TipoOperacao { get; set; }
    }
}
