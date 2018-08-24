using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Infra.ORM.Context
{
    [ExcludeFromCodeCoverage]
    public class DbContextFactory : IDbContextFactory<BancoTabajaraContext>
    {
        public BancoTabajaraContext Create()
        {
            return new BancoTabajaraContext();
        }
    }
}
