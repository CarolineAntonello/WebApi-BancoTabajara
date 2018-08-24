using BancoTabajara.API.Controllers.Contas;
using BancoTabajara.API.Exceptions;
using BancoTabajara.Applications.Features.Contas;
using BancoTabajara.Applications.Features.Contas.Commands;
using BancoTabajara.Applications.Features.Contas.ViewModel;
using BancoTabajara.Common.Tests.Features.Contas;
using BancoTabajara.Controller.Tests.Initializer;
using BancoTabajara.Domain.Exceptions;
using BancoTabajara.Domain.Features.Contas;
using BancoTabajara.Domain.Features.Extratos;
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

namespace BancoTabajara.Controller.Tests.Features.Contas
{
    [TestFixture]
    public class ContasControllerTest : TestControllerBase
    {
        private ContasController _contasController;
        private Mock<IContaService> _contaServiceMock;
        private Mock<Conta> _conta;
        private Mock<ContaRegisterCommand> _contaRegisterCmd;
        private Mock<ContaRemoveCommand> _contaRemoveCmd;
        private Mock<ContaUpdateCommand> _contaUpdateCmd;
        private Mock<ValidationResult> _validator;
        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _contaServiceMock = new Mock<IContaService>();
            _contasController = new ContasController(_contaServiceMock.Object)
            {
                Request = request
            };
            _conta = new Mock<Conta>();
            _validator = new Mock<ValidationResult>();
            _contaRegisterCmd = new Mock<ContaRegisterCommand>();
            _contaRegisterCmd.Setup(cmd => cmd.Validar()).Returns(_validator.Object);
            _contaRemoveCmd = new Mock<ContaRemoveCommand>();
            _contaRemoveCmd.Setup(cmd => cmd.Validar()).Returns(_validator.Object);
            _contaUpdateCmd = new Mock<ContaUpdateCommand>();
            _contaUpdateCmd.Setup(cmd => cmd.Validar()).Returns(_validator.Object);
            var isValid = true;
            _validator.Setup(v => v.IsValid).Returns(isValid);
        }

        #region GET

        [Test]
        public void Controller_Contas_Get_DevePassar()
        {
            // Arrange
            var conta = ContaObjectMother.GetContaValida();
            var response = new List<Conta>() { conta }.AsQueryable();
            _contaServiceMock.Setup(s => s.GetAll(0)).Returns(response);
            // Action
            var callback = _contasController.Get();
            //Assert
            _contaServiceMock.Verify(s => s.GetAll(0), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<ContaViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(conta.Id);
        }

        [Test]
        public void Controller_Contas_Get_Com_Quantidade_DevePassar()
        {
            // Arrange
            var conta = ContaObjectMother.GetContaValida();
            var uri = "http://localhost:9001/api/contas?quantidade=3";
            var quantidade = 3;
            var response = new List<Conta>() { conta, conta, conta }.AsQueryable();
            _contaServiceMock.Setup(s => s.GetAll(quantidade)).Returns(response);
            _contasController.Request = GetUri(uri);
            // Action
            var callback = _contasController.Get();
            //Assert
            _contaServiceMock.Verify(s => s.GetAll(quantidade), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<ContaViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.Count.Should().Be(quantidade);
            httpResponse.Content.First().Id.Should().Be(conta.Id);
        }

        [Test]
        public void Controller_Contas_Get_Com_Quantidade_E_Outros_Filtros_DevePassar()
        {
            // Arrange
            var conta = ContaObjectMother.GetContaValida();
            var uri = "http://localhost:9001/api/contas?numero=123";
            var response = new List<Conta>() { conta, conta, conta }.AsQueryable();
            _contaServiceMock.Setup(s => s.GetAll(0)).Returns(response);
            _contasController.Request = GetUri(uri);
            // Action
            var callback = _contasController.Get();
            //Assert
            _contaServiceMock.Verify(s => s.GetAll(0), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<ContaViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(conta.Id);
        }

        [Test]
        public void Controller_Contas_GerarExtrato_DevePassar()
        {
            // Arrange
            var conta = ContaObjectMother.GetContaValida();
            var extrato = conta.GerarExtrato();
           
            _contaServiceMock.Setup(s => s.GetExtrato(conta.Id)).Returns(extrato);
            // Action
            var callback = _contasController.GetExtrato(conta.Id);
            //Assert
            _contaServiceMock.Verify(s => s.GetExtrato(conta.Id), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<Extrato>>().Subject;
            httpResponse.Content.Should().NotBeNull(); 
        }

        [Test]
        public void Controller_Contas_GetById_DevePassar()
        {
            // Arrange
            var conta = ContaObjectMother.GetContaValida();
            _contaServiceMock.Setup(c => c.GetById(conta.Id)).Returns(conta);
            // Action
            IHttpActionResult callback = _contasController.GetById(conta.Id);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<ContaViewModel>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(conta.Id);
            _contaServiceMock.Verify(s => s.GetById(conta.Id), Times.Once);
        }

        #endregion

        #region POST

        [Test]
        public void Controller_Contas_Post_DevePassar()
        {
            // Arrange
            var id = 1;
            _contaServiceMock.Setup(c => c.Add(_contaRegisterCmd.Object)).Returns(id);
           // Action
            IHttpActionResult callback = _contasController.Post(_contaRegisterCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<int>>().Subject;
            httpResponse.Content.Should().Be(id);
            _contaServiceMock.Verify(s => s.Add(_contaRegisterCmd.Object), Times.Once);
        }

        [Test]
        public void Controller_Contas_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var isValid = false;
            _validator.Setup(v => v.IsValid).Returns(isValid);
            // Action
            var callback = _contasController.Post(_contaRegisterCmd.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            _contaRegisterCmd.Verify(cmd => cmd.Validar(), Times.Once);
            _contaRegisterCmd.VerifyNoOtherCalls();
        }
        #endregion

        #region PUT

        [Test]
        public void Controller_Contas_Put_DevePassar()
        {
            // Arrange
            var isUpdated = true;
            _contaServiceMock.Setup(c => c.Update(_contaUpdateCmd.Object)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _contasController.Put(_contaUpdateCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _contaServiceMock.Verify(s => s.Update(_contaUpdateCmd.Object), Times.Once);
        }

        [Test]
        public void Controller_Contas_Put_ShouldHandleNotFoundexception()
        {
            // Arrange
            _contaServiceMock.Setup(c => c.Update(_contaUpdateCmd.Object)).Throws<NotFoundException>();
            // Action
            IHttpActionResult callback = _contasController.Put(_contaUpdateCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.Content.ErrorCode.Should().Be((int)ErrorCodes.NotFound);
            // Perceba que é um cenário onde o servico disporou a exception. Logo, ele deve ser chamado.
            _contaServiceMock.Verify(s => s.Update(_contaUpdateCmd.Object), Times.Once);
        }

        [Test]
        public void Controller_Contas_Put_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var isValid = false;
            _validator.Setup(v => v.IsValid).Returns(isValid);
            // Action
            var callback = _contasController.Put(_contaUpdateCmd.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            _contaUpdateCmd.Verify(cmd => cmd.Validar(), Times.Once);
            _contaUpdateCmd.VerifyNoOtherCalls();
        }

        #endregion

        #region PATCH

        [Test]
        public void Controller_Contas_AlterarEstado_DevePassar()
        {
            // Arrange
            var isUpdated = true;
            _contaServiceMock.Setup(c => c.AlterarEstado(_conta.Object.Id)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _contasController.AlterarEstado(_conta.Object.Id);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _contaServiceMock.Verify(s => s.AlterarEstado(_conta.Object.Id), Times.Once);
        }

        [Test]
        public void Controller_Contas_EfetuarDeposito_DevePassar()
        {
            // Arrange
            var isUpdated = true;
            var valor = 100;
            _contaServiceMock.Setup(c => c.EfetuarDeposito(_conta.Object.Id, valor)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _contasController.EfetuarDeposito(_conta.Object.Id, valor);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _contaServiceMock.Verify(s => s.EfetuarDeposito(_conta.Object.Id, valor), Times.Once);
        }

        [Test]
        public void Controller_Contas_EfetuarSaque_DevePassar()
        {
            // Arrange
            var isUpdated = true;
            var valor = 100;
            _contaServiceMock.Setup(c => c.EfetuarSaque(_conta.Object.Id, valor)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _contasController.EfetuarSaque(_conta.Object.Id, valor);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _contaServiceMock.Verify(s => s.EfetuarSaque(_conta.Object.Id, valor), Times.Once);
        }

        [Test]
        public void Controller_Contas_EfetuarTransferencia_DevePassar()
        {
            // Arrange
            var contaOrigem = ContaObjectMother.GetContaValida();
            var contaDestino = ContaObjectMother.GetContaValidaParaAtualizar();
            contaDestino.Id = 2;
            var isUpdated = true;
            var valor = 100;
            _contaServiceMock.Setup(c => c.EfetuarTrasferencia(contaOrigem.Id, contaDestino.Id, valor)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _contasController.EfetuarTransferencia(contaOrigem.Id, contaDestino.Id, valor);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _contaServiceMock.Verify(s => s.EfetuarTrasferencia(contaOrigem.Id, contaDestino.Id, valor), Times.Once);
        }
        #endregion

        #region DELETE

        [Test]
        public void Controller_Contas_Delete_DevePassar()
        {
            // Arrange
            var isUpdated = true;
            _contaServiceMock.Setup(c => c.Delete(_contaRemoveCmd.Object)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _contasController.Delete(_contaRemoveCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _contaServiceMock.Verify(s => s.Delete(_contaRemoveCmd.Object), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        #endregion

    }
}