using BancoTabajara.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Contas
{
    public class ContaSaldoInsuficienteException : BusinessException
    {
        public ContaSaldoInsuficienteException() : base(ErrorCodes.NotAllowed, "Saldo insuficiente para realizar o saque!")
        {
        }
    }
}
