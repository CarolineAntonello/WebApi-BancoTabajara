using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Applications.Features.Clientes.Queries
{
    public class ClienteQuery
    {
        public virtual int Qtd { get; set; }

        public ClienteQuery(int _qtd)
        {
            Qtd = _qtd;
        }

    }
}
