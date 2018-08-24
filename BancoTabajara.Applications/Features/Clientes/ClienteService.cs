using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BancoTabajara.Applications.Features.Clientes.Commands;
using BancoTabajara.Applications.Features.Clientes.Queries;
using BancoTabajara.Domain.Exceptions;
using BancoTabajara.Domain.Features.Clientes;

namespace BancoTabajara.Applications.Features.Clientes
{
    public class ClienteService : IClienteService
    {
        IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public int Add(ClienteRegisterCommand clienteCmd)
        {
            var cliente = Mapper.Map<ClienteRegisterCommand, Cliente>(clienteCmd);
            var novoCliente = _repository.Add(cliente);

            return novoCliente.Id;
        }
        public bool Update(ClienteUpdateCommand clienteCmd)
        {
            var clienteDb = _repository.GetById(clienteCmd.Id);
            if (clienteDb == null)
                throw new NotFoundException();

            var updateCliente = Mapper.Map(clienteCmd, clienteDb);

            return _repository.Update(updateCliente);
        }

        public bool Delete(ClienteRemoveCommand clienteCmd)
        {
            return _repository.Delete(clienteCmd.Id);
        }

        public IQueryable<Cliente> GetAll(int quantidade = 0)
        {
            return _repository.GetAll(quantidade);
        }

        public Cliente GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Cliente Login(string nome, string CPF)
        {
            return _repository.Login(nome, CPF);
        }
    }
}
