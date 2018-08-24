using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Clientes
{
    public interface IClienteRepository
    {
        Cliente Login(string nome, string CPF);

        Cliente Add(Cliente cliente);

        Cliente GetById(int id);

        IQueryable<Cliente> GetAll(int quantidade);

        bool Delete(int id);

        bool Update(Cliente cliente);
   
    }
}
