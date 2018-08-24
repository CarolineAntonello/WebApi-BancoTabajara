using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Contas
{
    public interface IContaRepository
    {
        Conta Add(Conta conta);

        Conta GetById(int id);

        IQueryable<Conta> GetAll(int quantidade);

        bool Delete(int id);

        bool Update(Conta conta);

    }
}
