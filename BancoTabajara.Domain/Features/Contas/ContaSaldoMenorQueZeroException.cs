using BancoTabajara.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Contas
{
    public class ContaSaldoMenorQueZeroException : BusinessException
    {
        public ContaSaldoMenorQueZeroException() : base(ErrorCodes.NotAllowed, "Saldo não pode ser menor que zero!")
        {
        }
    }
}
