using BancoTabajara.Domain.Exceptions;
using System;
using System.Runtime.Serialization;

namespace BancoTabajara.Domain.Features.Contas
{
    public class ContaClienteInvalidoException : BusinessException
    {
        public ContaClienteInvalidoException() : base(ErrorCodes.NotAllowed, "É preciso ter um cliente!")
        {
        }
    }
}