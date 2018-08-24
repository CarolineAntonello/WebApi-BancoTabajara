using BancoTabajara.Domain.Features.Contas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Infra.ORM.Features.Contas
{
   public  class ContaConfiguration : EntityTypeConfiguration<Conta>
    {
        public ContaConfiguration()
        {
            ToTable("TBConta");
            HasKey(c => c.Id);
            Property(c => c.Ativada).HasColumnType("bit").IsRequired();
            Property(c => c.Limite).IsRequired();
            Property(c => c.NumeroConta).HasColumnType("int").IsRequired();
            Property(c => c.Saldo).IsRequired();
            HasRequired(c => c.Cliente);
            HasMany(c => c.Movimentacoes).WithOptional();
        }
    }
}
