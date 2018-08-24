using BancoTabajara.API.Controllers.Clientes;
using BancoTabajara.Applications.Features.Clientes;
using BancoTabajara.Applications.Features.Clientes.Commands;
using BancoTabajara.Applications.Features.Clientes.ViewModel;
using BancoTabajara.Common.Tests.Features.Clientes;
using BancoTabajara.Controller.Tests.Initializer;
using BancoTabajara.Domain.Features.Clientes;
using FluentAssertions;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace BancoTabajara.Controller.Tests.Features.Clientes
{
    [TestFixture]
    public class ClientesControllerTest : TestControllerBase
    {
        private ClientesController _clientesController;
        private Mock<IClienteService> _clienteServiceMock;
        private Mock<Cliente> _cliente;
        private Mock<ClienteRegisterCommand> _clienteRegisterCmd;
        private Mock<ClienteRemoveCommand> _clienteRemoveCmd;
        private Mock<ClienteUpdateCommand> _clienteUpdateCmd;
        private Mock<ValidationResult> _validator;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _clienteServiceMock = new Mock<IClienteService>();
            _clientesController = new ClientesController(_clienteServiceMock.Object)
            {
                Request = request,

            };
            _cliente = new Mock<Cliente>();
            _validator = new Mock<ValidationResult>();
            _clienteRegisterCmd = new Mock<ClienteRegisterCommand>();
            _clienteRegisterCmd.Setup(cmd => cmd.Validar()).Returns(_validator.Object);
            _clienteRemoveCmd = new Mock<ClienteRemoveCommand>();
            _clienteRemoveCmd.Setup(cmd => cmd.Validar()).Returns(_validator.Object);
            _clienteUpdateCmd = new Mock<ClienteUpdateCommand>();
            _clienteUpdateCmd.Setup(cmd => cmd.Validar()).Returns(_validator.Object);
            var isValid = true;
            _validator.Setup(v => v.IsValid).Returns(isValid);
        }

        #region GET

        [Test]
        public void Controller_Clientes_Get_DevePassar()
        {
            // Arrange
            var cliente = ClienteObjectMother.GetClienteValido();
            var response = new List<Cliente>() { cliente }.AsQueryable();
            _clienteServiceMock.Setup(s => s.GetAll(0)).Returns(response);
            // Action
            var callback = _clientesController.Get();
            //Assert
            _clienteServiceMock.Verify(s => s.GetAll(0), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<ClienteViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(cliente.Id);
        }

        [Test]
        public void Controller_Clientes_Get_Com_Quantidade_DevePassar()
        {
            // Arrange
            var cliente = ClienteObjectMother.GetClienteValido();
            var uri = "http://localhost:9001/api/clientes?quantidade=3";
            var quantidade = 3;
            var response = new List<Cliente>() { cliente, cliente, cliente }.AsQueryable();
            _clienteServiceMock.Setup(s => s.GetAll(quantidade)).Returns(response);
            _clientesController.Request = GetUri(uri);
            // Action
            var callback = _clientesController.Get();
            //Assert
            _clienteServiceMock.Verify(s => s.GetAll(quantidade), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<ClienteViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.Count.Should().Be(quantidade);
            httpResponse.Content.First().Id.Should().Be(cliente.Id);
        }

        [Test]
        public void Controller_Clientes_Get_Com_Outros_Filtros_DevePassar()
        {
            // Arrange
            var cliente = ClienteObjectMother.GetClienteValido();
            var uri = "http://localhost:9001/api/clientes?nome=lucas";
            var response = new List<Cliente>() { cliente, cliente, cliente }.AsQueryable();
            _clienteServiceMock.Setup(s => s.GetAll(0)).Returns(response);
            _clientesController.Request = GetUri(uri);
            // Action
            var callback = _clientesController.Get();
            //Assert
            _clienteServiceMock.Verify(s => s.GetAll(0), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<ClienteViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(cliente.Id);
        }

        [Test]
        public void Controller_Clientes_GetById_DevePassar()
        {
            // Arrange
            var cliente = ClienteObjectMother.GetClienteValido();
            _clienteServiceMock.Setup(c => c.GetById(cliente.Id)).Returns(cliente);
            // Action
            IHttpActionResult callback = _clientesController.GetById(cliente.Id);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<ClienteViewModel>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(cliente.Id);
            _clienteServiceMock.Verify(s => s.GetById(cliente.Id), Times.Once);
        }

        #endregion

        #region POST

        [Test]
        public void Controller_Clientes_Post_DevePassar()
        {
            // Arrange
            var cliente = ClienteObjectMother.GetClienteValido();
            var clienteCmd = ClienteObjectMother.GetClienteValidoParaRegistrar();
            _clienteServiceMock.Setup(c => c.Add(clienteCmd)).Returns(cliente.Id);
            // Action
            IHttpActionResult callback = _clientesController.Post(clienteCmd);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<int>>().Subject;
            httpResponse.Content.Should().Be(cliente.Id);
            _clienteServiceMock.Verify(s => s.Add(clienteCmd), Times.Once);
           
        }
        #endregion

        #region PUT

        [Test]
        public void Controller_Clientes_Put_DevePassar()
        {
            // Arrange
            var cliente = ClienteObjectMother.GetClienteValidoParaAtualizar();
            var isUpdated = true;
            _clienteServiceMock.Setup(c => c.Update(cliente)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _clientesController.Put(cliente);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _clienteServiceMock.Verify(s => s.Update(cliente), Times.Once);
        }

        #endregion

        #region DELETE

        [Test]
        public void Controller_Clientes_Delete_DevePassar()
        {
            // Arrange
            var cliente = ClienteObjectMother.GetClienteValidoParaDeletar();
            var isRemoved = true;
            _clienteServiceMock.Setup(c => c.Delete(cliente)).Returns(isRemoved);
            // Action
            IHttpActionResult callback = _clientesController.Delete(cliente);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _clienteServiceMock.Verify(s => s.Delete(cliente), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        #endregion

    }
}