using BancoTabajara.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Contas
{
    public class ContaNumeroAlteradoException : BusinessException
    {
        public ContaNumeroAlteradoException() : base(ErrorCodes.NotAllowed, "Não é possível alterar o numero da conta!")
        {
        }
    }
}
