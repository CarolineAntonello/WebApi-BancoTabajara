using BancoTabajara.Applications.Features.Clientes.Commands;
using BancoTabajara.Domain.Features.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Applications.Features.Clientes
{
    public interface IClienteService
    {
        int Add(ClienteRegisterCommand cliente);

        bool Update(ClienteUpdateCommand cliente);

        bool Delete(ClienteRemoveCommand cliente);

        Cliente GetById(int id);

        IQueryable<Cliente> GetAll(int quantidade = 0);

        Cliente Login(string nome, string CPF);
    }
}
