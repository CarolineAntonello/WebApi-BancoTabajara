using BancoTabajara.Common.Tests.Features.Contas;
using BancoTabajara.Domain.Exceptions;
using BancoTabajara.Domain.Features.Contas;
using BancoTabajara.Infra.ORM.Features.Contas;
using BancoTabajara.Infra.ORM.Tests.Context;
using BancoTabajara.Infra.ORM.Tests.Initializer;
using Effort;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Infra.ORM.Tests.Features.Contas
{
    [TestFixture]
    public class ContaRepositoryTest : EffortTestBase
    {
        private FakeDbContext _context;
        private ContaRepository _repository;
        private Conta _conta;
        private Conta _contaSeed;

        [SetUp]
        public void Setup()
        {
            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _context = new FakeDbContext(connection);
            _repository = new ContaRepository(_context);

            _conta = ContaObjectMother.GetContaValida();
            //Seed
            _contaSeed = ContaObjectMother.GetContaValida();
            _context.Contas.Add(_contaSeed);
            _context.Clientes.Add(_contaSeed.Cliente);
            _context.Clientes.Add(_conta.Cliente);
            _context.SaveChanges();
        }

        #region ADD
        [Test]
        public void Repositorio_Conta_Adicionar_Corretamente()
        {
            //Action
            var contaRegistrado = _repository.Add(_conta);

            //Assert
            contaRegistrado.Should().NotBeNull();
            contaRegistrado.Should().Be(_conta);
        }
        #endregion

        #region GETs

        [Test]
        public void Repositorio_Conta_PegarTodos_DevePassar()
        {
            //Action
            var contas = _repository.GetAll(0).ToList();

            //Assert
            contas.Should().NotBeNull();
            contas.Should().HaveCount(_context.Contas.Count());
            contas.First().Should().Be(_contaSeed);
        }

        [Test]
        public void Repositorio_Conta_PegarPorId_DevePassar()
        {
            //Action
            var contaResult = _repository.GetById(1);

            //Assert
            contaResult.Should().NotBeNull();
            contaResult.Cliente.Should().NotBeNull();
            contaResult.Should().Be(_contaSeed);
        }

        [Test]
        public void Repositorio_Conta_PegarPorId_DeveRetornarNulo()
        {
            //Action
            var notFoundId = 10;
            var productResult = _repository.GetById(notFoundId);

            //Assert
            productResult.Should().BeNull();
        }

        #endregion

        #region DELETE
        [Test]
        public void Repositorio_Conta_Deletar_Corretamente()
        {
            // Action
            var wasRemoved = _repository.Delete(_contaSeed.Id);
            //Verify
            wasRemoved.Should().BeTrue();
            _context.Contas.Where(p => p.Id == _contaSeed.Id).ToList().Should().BeEmpty();
        }

        [Test]
        public void Repositorio_Conta_Deletar_DeveJogarExcessao_NotFoundException()
        {
            //Assert
            var notFoundId = 10;
            // Action
            Action callbackDelete = () => _repository.Delete(notFoundId);
            //Verify
            callbackDelete.Should().Throw<NotFoundException>();
        }
        #endregion

        #region UPDATE

        [Test]
        public void Repositorio_Conta_Atualizar_Corretamente()
        {
            var wasUpdated = false;
            var newLimite = 5000;
            _contaSeed.Limite = newLimite;
            var action = new Action(() => { wasUpdated = _repository.Update(_contaSeed); });
            // O EF não deve lançar exception
            action.Should().NotThrow<Exception>();
            wasUpdated.Should().BeTrue();
        }
        #endregion
    }
}
