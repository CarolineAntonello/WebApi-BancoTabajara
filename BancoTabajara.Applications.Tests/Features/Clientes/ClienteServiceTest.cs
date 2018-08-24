using BancoTabajara.Applications.Features.Clientes;
using BancoTabajara.Applications.Tests.Initializer;
using BancoTabajara.Common.Tests.Features.Clientes;
using BancoTabajara.Domain.Exceptions;
using BancoTabajara.Domain.Features.Clientes;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Applications.Tests.Features.Clientes
{
    [TestFixture]
    public class ClienteServiceTest : TestBase
    {

        private Mock<IClienteRepository> _repositoryFake;
        private IClienteService _service;

        [SetUp]
        public void Initialize()
        {
            _repositoryFake = new Mock<IClienteRepository>();
            _service = new ClienteService(_repositoryFake.Object);
        }

        [Test]
        public void Service_Cliente_AdicionarCliente_DevePassar()
        {
            //Arrange
            var cliente = ClienteObjectMother.GetClienteValido();
            var clienteCmd = ClienteObjectMother.GetClienteValidoParaRegistrar();
            _repositoryFake.Setup(x => x.Add(It.IsAny<Cliente>()))
                .Returns(cliente);
            //Action
            var novoClienteId = _service.Add(clienteCmd);
            //Verify
            _repositoryFake.Verify(x => x.Add(It.IsAny<Cliente>()), Times.Once);
            novoClienteId.Should().Be(cliente.Id);
        }

        [Test]
        public void Service_Cliente_AtualizarCliente_DevePassar()
        {
            //Arrange
            var cliente = ClienteObjectMother.GetClienteValido();
            var clienteCmd = ClienteObjectMother.GetClienteValidoParaAtualizar();
            var atualizado = true;
            _repositoryFake.Setup(x => x.GetById(clienteCmd.Id)).Returns(cliente);
            _repositoryFake.Setup(pr => pr.Update(cliente)).Returns(atualizado);
            //Action
            var clienteAtualizado = _service.Update(clienteCmd);
            //Verify
            _repositoryFake.Verify(pr => pr.GetById(clienteCmd.Id), Times.Once);
            _repositoryFake.Verify(pr => pr.Update(cliente), Times.Once);
            clienteAtualizado.Should().BeTrue();
        }

        [Test]
        public void Service_Cliente_AtualizarCliente_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var clienteCmd = ClienteObjectMother.GetClienteValidoParaAtualizar();
            _repositoryFake.Setup(x => x.GetById(clienteCmd.Id)).Returns((Cliente)null);
            //Action
            Action act = () => _service.Update(clienteCmd);
            //Assert
            act.Should().Throw<NotFoundException>();
            _repositoryFake.Verify(pr => pr.GetById(clienteCmd.Id), Times.Once);
            _repositoryFake.Verify(pr => pr.Update(It.IsAny<Cliente>()), Times.Never);
        }

        [Test]
        public void Service_Cliente_Delete_DevePassar()
        {
            //Arrange
            var clienteCmd = ClienteObjectMother.GetClienteValidoParaDeletar();
            var removido = true;
            _repositoryFake.Setup(pr => pr.Delete(clienteCmd.Id)).Returns(removido);
            //Action
            var clienteRemovido = _service.Delete(clienteCmd);
            //Assert
            _repositoryFake.Verify(pr => pr.Delete(clienteCmd.Id), Times.Once);
            clienteRemovido.Should().BeTrue();
        }

        [Test]
        public void Service_Cliente_Delete_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var clienteCmd = ClienteObjectMother.GetClienteValidoParaDeletar();
            _repositoryFake.Setup(x => x.Delete(clienteCmd.Id)).Throws<NotFoundException>();
            //Action
            Action act = () => _service.Delete(clienteCmd);
            //Assert
            act.Should().Throw<NotFoundException>();
            _repositoryFake.Verify(pr => pr.Delete(clienteCmd.Id), Times.Once);
        }

        [Test]
        public void Service_Cliente_PegarClientePorId_DevePassar()
        {
            //Arrange
            var cliente = ClienteObjectMother.GetClienteValido();
            _repositoryFake.Setup(pr => pr.GetById(cliente.Id)).Returns(cliente);
            //Action
            var recebido = _service.GetById(cliente.Id);
            //Verify
            _repositoryFake.Verify(pr => pr.GetById(cliente.Id), Times.Once);
            recebido.Should().NotBeNull();
            recebido.Id.Should().Be(cliente.Id);
        }

        [Test]
        public void Service_Cliente_PegarClientePorId_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var cliente = ClienteObjectMother.GetClienteValido();
            _repositoryFake.Setup(pr => pr.GetById(cliente.Id)).Throws<NotFoundException>();
            //Action
            Action act = () => _service.GetById(cliente.Id);
            //Assert
            act.Should().Throw<NotFoundException>();
             _repositoryFake.Verify(pr => pr.GetById(cliente.Id), Times.Once);
        }

        [Test]
        public void Service_Cliente_PegarTodosOsClientes_DevePassar()
        {
            //Arrange
            var cliente = ClienteObjectMother.GetClienteValido();
            var listaClientes = new List<Cliente>() { cliente }.AsQueryable();
            _repositoryFake.Setup(pr => pr.GetAll(0)).Returns(listaClientes);
            //Action
            var recebidos = _service.GetAll();
            //Assert
            _repositoryFake.Verify(pr => pr.GetAll(0), Times.Once);
            recebidos.Should().NotBeNull();
            recebidos.Count().Should().Be(listaClientes.Count());
            recebidos.First().Should().Be(listaClientes.First());
        }
    }
}
