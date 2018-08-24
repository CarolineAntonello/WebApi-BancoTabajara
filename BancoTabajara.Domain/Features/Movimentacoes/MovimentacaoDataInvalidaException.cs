using BancoTabajara.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Movimentacoes
{
    public class MovimentacaoDataInvalidaException : BusinessException
    {
        public MovimentacaoDataInvalidaException() : base(ErrorCodes.NotAllowed, "Movimentação não pode ter data menor que atual!")
        {
        }
    }
}