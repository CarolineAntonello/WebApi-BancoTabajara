using BancoTabajara.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Clientes
{
    public class ClienteRGInvalidoException : BusinessException
    {
        public ClienteRGInvalidoException() : base(ErrorCodes.NotAllowed, "Cliente não pode ter RG invalido!")
        {
        }
    }
}