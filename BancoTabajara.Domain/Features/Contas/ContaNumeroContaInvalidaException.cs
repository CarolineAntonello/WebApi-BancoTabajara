using BancoTabajara.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Contas
{
    public class ContaNumeroContaInvalidaException : BusinessException
    {
        public ContaNumeroContaInvalidaException() : base(ErrorCodes.NotAllowed, "Número da conta não pode ser menor ou igual a zero!")
        {
        }
    }
}