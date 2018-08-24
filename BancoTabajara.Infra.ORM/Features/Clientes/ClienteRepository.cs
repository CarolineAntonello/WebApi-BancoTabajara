using BancoTabajara.Domain.Exceptions;
using BancoTabajara.Domain.Features.Clientes;
using BancoTabajara.Infra.ORM.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Infra.ORM.Features.Clientes
{
    public class ClienteRepository : IClienteRepository
    {
        private BancoTabajaraContext _context;

        public ClienteRepository(BancoTabajaraContext context)
        {
            _context = context;
        }

        public Cliente Add(Cliente cliente)
        {
            var novoCliente = _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return novoCliente;
        }

        public IQueryable<Cliente> GetAll(int quantidade)
        {
            if (quantidade == 0)
                return _context.Clientes;
            else
                return _context.Clientes.Take(quantidade);
        }

        public Cliente GetById(int clienteId)
        {
            return _context.Clientes.FirstOrDefault(c => c.Id == clienteId);
        }
        public bool Delete(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(p => p.Id == id);
            if (cliente == null)
                throw new NotFoundException();
            _context.Entry(cliente).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }
        public bool Update(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public Cliente Login(string nome, string CPF)
        {
            return _context.Clientes.FirstOrDefault(c => c.Nome == nome && c.CPF == CPF);
        }
    }
}
