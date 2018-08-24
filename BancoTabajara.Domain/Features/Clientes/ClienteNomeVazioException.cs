using BancoTabajara.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Clientes
{
    public class ClienteNomeVazioException : BusinessException
    {
        public ClienteNomeVazioException() : base(ErrorCodes.NotAllowed, "Cliente não pode ter nome vazio!")
        {
        }
    }
}