using BancoTabajara.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Movimentacoes
{
    public class MovimentacaoTipoOperacaoInvalidaException : BusinessException
    {
        public MovimentacaoTipoOperacaoInvalidaException() : base(ErrorCodes.NotAllowed, "O tipo de operação da movimentação não pode ser vazio!")
        {
        }
    }
}
