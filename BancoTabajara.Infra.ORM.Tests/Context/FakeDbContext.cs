using BancoTabajara.Infra.ORM.Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Infra.ORM.Tests.Context
{
    public class FakeDbContext : BancoTabajaraContext
    {
        public FakeDbContext(DbConnection connection) : base(connection)
        {
        }
    }
}
