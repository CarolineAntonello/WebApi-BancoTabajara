using BancoTabajara.Domain.Exceptions;
using BancoTabajara.Domain.Features.Contas;
using BancoTabajara.Infra.ORM.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Infra.ORM.Features.Contas
{
    public class ContaRepository : IContaRepository
    {
        private BancoTabajaraContext _context;

        public ContaRepository(BancoTabajaraContext context)
        {
            _context = context;
        }

        public Conta Add(Conta conta)
        {
            _context.Clientes.Attach(conta.Cliente);
            var novaConta = _context.Contas.Add(conta);
            _context.SaveChanges();
            return novaConta;
        }

        public bool Update(Conta conta)
        {
            _context.Entry(conta).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var conta = _context.Contas.Where(o => o.Id == id).FirstOrDefault();
            if (conta == null)
                throw new NotFoundException();
            _context.Entry(conta).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public IQueryable<Conta> GetAll(int quantidade)
        {
            if (quantidade == 0)
                return _context.Contas.Include(c => c.Cliente).Include(c => c.Movimentacoes);
            else
                return (_context.Contas.Include(c => c.Cliente).Include(c => c.Movimentacoes)).Take(quantidade);

        }

        public Conta GetById(int id)
        {
            return _context.Contas.Include(c => c.Cliente).Include(c => c.Movimentacoes).FirstOrDefault(c => c.Id == id);
        }

    }
}
