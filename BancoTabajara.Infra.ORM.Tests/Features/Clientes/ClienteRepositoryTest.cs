using BancoTabajara.Common.Tests.Features.Clientes;
using BancoTabajara.Domain.Exceptions;
using BancoTabajara.Domain.Features.Clientes;
using BancoTabajara.Infra.ORM.Context;
using BancoTabajara.Infra.ORM.Features.Clientes;
using BancoTabajara.Infra.ORM.Tests.Context;
using BancoTabajara.Infra.ORM.Tests.Initializer;
using Effort;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Infra.ORM.Tests.Features.Clientes
{
    [TestFixture]
    public class ClienteRepositoryTest : EffortTestBase
    {
        private FakeDbContext _context;
        private ClienteRepository _repository;
        private Cliente _cliente;
        private Cliente _clienteSeed;

        [SetUp]
        public void Setup()
        {
            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _context = new FakeDbContext(connection);
            _repository = new ClienteRepository(_context);

            _cliente = ClienteObjectMother.GetClienteValido();
            //Seed
            _clienteSeed = ClienteObjectMother.GetClienteValido();
            _context.Clientes.Add(_clienteSeed);
            _context.SaveChanges();
        }


        #region ADD
        [Test]
        public void Repositorio_Cliente_Adicionar_Corretamente()
        {
            //Action
            var clienteRegistrado = _repository.Add(_cliente);

            //Assert
            clienteRegistrado.Should().NotBeNull();
            clienteRegistrado.Should().Be(_cliente);
        }
        #endregion

        #region GETs

        [Test]
        public void Repositorio_Cliente_PegarTodos_DevePassar()
        {
            //Action
            var clientes = _repository.GetAll(0).ToList();

            //Assert
            clientes.Should().NotBeNull();
            clientes.Should().HaveCount(_context.Clientes.Count());
            clientes.First().Should().Be(_clienteSeed);
        }

        [Test]
        public void Repositorio_Cliente_PegarPorId_DevePassar()
        {
            //Action
            var clienteResult = _repository.GetById(_clienteSeed.Id);

            //Assert
            clienteResult.Should().NotBeNull();
            clienteResult.Should().Be(_clienteSeed);
        }

        [Test]
        public void Repositorio_Cliente_PegarPorId_DeveRetornarNulo()
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
        public void Repositorio_Cliente_Deletar_Corretamente()
        {
            // Action
            var wasRemoved = _repository.Delete(_clienteSeed.Id);
            //Verify
            wasRemoved.Should().BeTrue();
            _context.Clientes.Where(p => p.Id == _clienteSeed.Id).ToList().Should().BeEmpty();
        }

        [Test]
        public void Repositorio_Cliente_Deletar_DeveJogarExcessao_NotFoundException()
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
        public void Repositorio_Cliente_Atualizar_Corretamente()
        {
            var wasUpdated = false;
            var newNome = "Ciclano";
            _clienteSeed.Nome = newNome;
            var action = new Action(() => { wasUpdated = _repository.Update(_clienteSeed); });
            // O EF não deve lançar exception
            action.Should().NotThrow<Exception>();
            wasUpdated.Should().BeTrue();
        }

        [Test]
        public void Repositorio_Cliente_Atualizar_DeveJogarExcessao_UnknownId()
        {
            _cliente = ClienteObjectMother.GetClienteValido();
            _cliente.Id = 20;
            var action = new Action(() => _repository.Update(_cliente));

            action.Should().Throw<DbUpdateConcurrencyException>();
        }
        #endregion

    }
}