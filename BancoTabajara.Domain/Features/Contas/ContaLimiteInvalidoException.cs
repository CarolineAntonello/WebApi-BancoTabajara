using BancoTabajara.Domain.Exceptions;
using System;
using System.Runtime.Serialization;

namespace BancoTabajara.Domain.Features.Contas
{
    public class ContaLimiteInvalidoException : BusinessException
    {
        public ContaLimiteInvalidoException() : base(ErrorCodes.NotAllowed, "Limite não pode ser negativo!")
        {
        }
    }
}