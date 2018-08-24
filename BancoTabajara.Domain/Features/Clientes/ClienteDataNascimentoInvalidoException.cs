using BancoTabajara.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Clientes
{
    public class ClienteDataNascimentoInvalidoException : BusinessException
    {
        public ClienteDataNascimentoInvalidoException() : base(ErrorCodes.NotAllowed, "Cliente não pode ter data de nascimento maior que a data atual!")
        {
        }
    }
}