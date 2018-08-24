using BancoTabajara.Domain.Features.Clientes;
using BancoTabajara.Domain.Features.Contas;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Infra.ORM.Context
{
    [ExcludeFromCodeCoverage]
    public class BancoTabajaraContext : DbContext
    {
        public BancoTabajaraContext(string connection = "Name=Skywalkers_BancoTabajara") : base(connection) {
            Configuration.ProxyCreationEnabled = false;
        }

        protected BancoTabajaraContext(DbConnection connection) : base(connection, true) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Conta> Contas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
