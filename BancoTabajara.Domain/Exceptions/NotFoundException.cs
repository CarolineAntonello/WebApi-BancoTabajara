using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException() : base(ErrorCodes.NotFound, "Registro não encontrado!") { }
    }
}
