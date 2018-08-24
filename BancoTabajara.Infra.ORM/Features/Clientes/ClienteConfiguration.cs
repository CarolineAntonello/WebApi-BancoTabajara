using BancoTabajara.Domain.Features.Clientes;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Infra.ORM.Features.Clientes
{
    public class ClienteConfiguration : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguration()
        {
            ToTable("TBCliente");
            HasKey(c => c.Id);
            Property(c => c.Nome).HasColumnName("Nome").HasMaxLength(50).IsRequired();
            Property(c => c.CPF).HasColumnName("CPF").HasMaxLength(50).IsRequired();
            Property(c => c.RG).HasColumnName("RG").HasMaxLength(50).IsRequired();
            Property(c => c.DataNascimento).IsRequired();
        }
    }
}
