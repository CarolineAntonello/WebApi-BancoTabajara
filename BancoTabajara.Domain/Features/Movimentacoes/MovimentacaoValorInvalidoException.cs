using BancoTabajara.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Movimentacoes
{
    public class MovimentacaoValorInvalidoException : BusinessException
    {
        public MovimentacaoValorInvalidoException() : base(ErrorCodes.NotAllowed, "Movimentação não pode ter valor menor ou igual a zero!")
        {
        }
    }
}