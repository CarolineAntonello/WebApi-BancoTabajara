using BancoTabajara.Domain.Features.Movimentacoes;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Infra.ORM.Features.Movimentacoes
{
   public class MovimentacaoConfiguration : EntityTypeConfiguration<Movimentacao>
    {
        public MovimentacaoConfiguration()
        {
            ToTable("TBMovimentacao");
            HasKey(m => m.Id);
            Property(m => m.Data).IsRequired();
            Property(m => m.TipoOperacao).IsRequired();
            Property(m => m.Valor).IsRequired();
         
        }
    }
}
