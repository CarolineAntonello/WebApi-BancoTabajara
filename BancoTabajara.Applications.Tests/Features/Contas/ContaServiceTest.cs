using BancoTabajara.Applications.Features.Contas;
using BancoTabajara.Applications.Tests.Initializer;
using BancoTabajara.Common.Tests.Features.Contas;
using BancoTabajara.Domain.Exceptions;
using BancoTabajara.Domain.Features.Clientes;
using BancoTabajara.Domain.Features.Contas;
using BancoTabajara.Domain.Features.Extratos;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Applications.Tests.Features.Contas
{
    [TestFixture]
    public class ContaServiceTest : TestBase
    {
        Mock<IContaRepository> _repositoryFake;
        Mock<IClienteRepository> _repositoryClienteFake; 
        IContaService _service;
        Conta _conta;

        [SetUp]
        public void Initialize()
        {
            _repositoryFake = new Mock<IContaRepository>();
            _repositoryClienteFake = new Mock<IClienteRepository>();
            _service = new ContaService(_repositoryFake.Object, _repositoryClienteFake.Object);
        }

        [Test]
        public void Service_Conta_AdicionarConta_DevePassar()
        {
            //Arrange
            var conta = ContaObjectMother.GetContaValida();
            var contaCmd = ContaObjectMother.GetContaValidaParaRegistrar();
            _repositoryFake.Setup(x => x.Add(It.IsAny<Conta>()))
                .Returns(conta);
            _repositoryClienteFake.Setup(cl => cl.GetById(contaCmd.ClienteId)).Returns(conta.Cliente);
            //Action
            var novoContaId = _service.Add(contaCmd);
            //Verify
            _repositoryFake.Verify(x => x.Add(It.IsAny<Conta>()), Times.Once);
            novoContaId.Should().Be(conta.Id);
        }

        [Test]
        public void Service_Conta_AdicionarConta_Com_IdCliente_Inesistente_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var conta = ContaObjectMother.GetContaValida();
            var contaCmd = ContaObjectMother.GetContaValidaParaRegistrar();
            contaCmd.ClienteId = 20;
           // _repositoryFake.Setup(pr => pr.Add(conta));
            _repositoryClienteFake.Setup(cl => cl.GetById(contaCmd.ClienteId)).Returns((Cliente)null);
            //Action
            Action act = () => _service.Add(contaCmd);
            //Assert
            act.Should().Throw<NotFoundException>();
            //_repositoryFake.Verify(x => x.Add(conta), Times.Never);
            _repositoryClienteFake.Verify(pr => pr.GetById(contaCmd.ClienteId), Times.Once);
        }

        [Test]
        public void Service_Conta_AdicionarContaLimiteNegativo_DeveLancarExcecao()
        {
            //Arrange
            _conta = ContaObjectMother.GetContaLimiteNegativo();
            _conta.RealizarDeposito(100);
            //Action
            Action act = () => _conta.Validar();
            //Verify
            act.Should().Throw<ContaLimiteInvalidoException>();
            _repositoryFake.VerifyNoOtherCalls();
        }

        [Test]
        public void Service_Conta_AdicionarContaSemCliente_DeveLancarExcecao()
        {
            //Arrange
            _conta = ContaObjectMother.GetContaSemCliente();
            //Action
            Action act = () => _conta.Validar();
            //Verify
            act.Should().Throw<ContaClienteInvalidoException>();
            _repositoryFake.VerifyNoOtherCalls();
        }

        [Test]
        public void Service_Conta_AtualizarConta_DevePassar()
        {
            //Arrange
            var conta = ContaObjectMother.GetContaValida();
            var contaCmd = ContaObjectMother.GetContaValidaParaAtualizar();
            var atualizado = true;
            _repositoryFake.Setup(x => x.GetById(contaCmd.Id)).Returns(conta);
            _repositoryFake.Setup(pr => pr.Update(conta)).Returns(atualizado);
            _repositoryClienteFake.Setup(cl => cl.GetById(contaCmd.ClienteId)).Returns(conta.Cliente);
            //Action
            var contaAtualizado = _service.Update(contaCmd);
            //Verify
            _repositoryFake.Verify(pr => pr.GetById(contaCmd.Id), Times.Once);
            _repositoryFake.Verify(pr => pr.Update(conta), Times.Once);
            contaAtualizado.Should().BeTrue();
        }

        [Test]
        public void Service_Conta_AtualizarContaMudarNumero_Nao_Deve_Permitir()
        {
            //Arrange
            var conta = ContaObjectMother.GetContaValida();
            var contaCmd = ContaObjectMother.GetContaValidaParaAtualizar();
            contaCmd.Id = 1;
            contaCmd.NumeroConta = 1500;
            _repositoryFake.Setup(x => x.GetById(It.IsAny<int>())).Returns(conta);
            _repositoryClienteFake.Setup(cl => cl.GetById(contaCmd.ClienteId)).Returns(conta.Cliente);
            //Action
            Action act = () =>_service.Update(contaCmd);
            //Verify
            act.Should().Throw<ContaNumeroAlteradoException>();
            _repositoryFake.Verify(x => x.GetById(It.IsAny<int>()));
            _repositoryFake.VerifyNoOtherCalls();
        }

        [Test]
        public void Service_Conta_AtualizarConta_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var contaCmd = ContaObjectMother.GetContaValidaParaAtualizar();
            _repositoryFake.Setup(x => x.GetById(contaCmd.Id)).Returns((Conta)null);
            //Action
            Action act = () => _service.Update(contaCmd);
            //Assert
            act.Should().Throw<NotFoundException>();
            _repositoryFake.Verify(pr => pr.GetById(contaCmd.Id), Times.Once);
            _repositoryFake.Verify(pr => pr.Update(It.IsAny<Conta>()), Times.Never);
        }

        [Test]
        public void Service_Conta_AtualizarConta_Com_IdCliente_Inesistente_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var conta = ContaObjectMother.GetContaValida();
            var contaCmd = ContaObjectMother.GetContaValidaParaAtualizar();
            contaCmd.ClienteId = 20;
            var atualizado = false;
            _repositoryFake.Setup(x => x.GetById(contaCmd.Id)).Returns(conta);
            _repositoryFake.Setup(pr => pr.Update(conta)).Returns(atualizado);
            _repositoryClienteFake.Setup(cl => cl.GetById(contaCmd.ClienteId)).Returns((Cliente) null);
            //Action
            Action act = () => _service.Update(contaCmd);
            //Assert
            act.Should().Throw<NotFoundException>();
            _repositoryFake.Verify(pr => pr.GetById(contaCmd.Id), Times.Once);
            _repositoryClienteFake.Verify(pr => pr.GetById(contaCmd.ClienteId), Times.Once);
            _repositoryFake.Verify(pr => pr.Update(It.IsAny<Conta>()), Times.Never);
        }

        [Test]
        public void Service_Conta_Delete_DevePassar()
        {
            //Arrange
            var contaCmd = ContaObjectMother.GetContaValidaParaDeletar();
            var removido = true;
            _repositoryFake.Setup(pr => pr.Delete(contaCmd.Id)).Returns(removido);
            //Action
            var contaRemovido = _service.Delete(contaCmd);
            //Assert
            _repositoryFake.Verify(pr => pr.Delete(contaCmd.Id), Times.Once);
            contaRemovido.Should().BeTrue();
        }

        [Test]
        public void Service_Conta_Delete_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var contaCmd = ContaObjectMother.GetContaValidaParaDeletar();
            _repositoryFake.Setup(x => x.Delete(contaCmd.Id)).Throws<NotFoundException>();
            //Action
            Action act = () => _service.Delete(contaCmd);
            //Assert
            act.Should().Throw<NotFoundException>();
            _repositoryFake.Verify(pr => pr.Delete(contaCmd.Id), Times.Once);
        }

        [Test]
        public void Service_Conta_PegarContaPorId_DevePassar()
        {
            //Arrange
            _conta = ContaObjectMother.GetConta();
            _conta.Id = 1;
            _repositoryFake.Setup(x => x.GetById(It.IsAny<int>())).Returns(_conta);
            //Action
            var recebido = _service.GetById(_conta.Id);
            //Verify
            recebido.Should().NotBeNull();
            _repositoryFake.Verify(x => x.GetById(_conta.Id));
        }

        [Test]
        public void Service_Conta_PegarContaPorId_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var conta = ContaObjectMother.GetContaValida();
            _repositoryFake.Setup(pr => pr.GetById(conta.Id)).Throws<NotFoundException>();
            //Action
            Action act = () => _service.GetById(conta.Id);
            //Assert
            act.Should().Throw<NotFoundException>();
            _repositoryFake.Verify(pr => pr.GetById(conta.Id), Times.Once);
        }

        [Test]
        public void Service_Conta_PegarTodasAsContas_DevePassar()
        {
            //Arrange
            _conta = ContaObjectMother.GetConta();
            var contas = new List<Conta>() { _conta }.AsQueryable();
            _repositoryFake
                .Setup(x => x.GetAll(0))
                .Returns(contas);
            //Action
            var recebido = _service.GetAll();
            recebido.ToList().Count.Should().BeGreaterThan(0);
            _repositoryFake.Verify(x => x.GetAll(0));
        }

        [Test]
        public void Service_Conta_PegarTodasAsContasComQuantidade_DevePassar()
        {
            //Arrange
            _conta = ContaObjectMother.GetConta();
            var contas = new List<Conta>() { _conta, _conta, _conta }.AsQueryable();
            _repositoryFake
                .Setup(x => x.GetAll(0))
                .Returns(contas);
            //Action
            var recebido = _service.GetAll();
            recebido.ToList().Count.Should().BeGreaterThan(0);
            _repositoryFake.Verify(x => x.GetAll(0));
        }

        [Test]
        public void Service_Conta_EfetuarSaque_DevePassar()
        {
            //Arrange
            _conta = ContaObjectMother.GetContaValida();
            _repositoryFake.Setup(x => x.Update(It.IsAny<Conta>()));
            _repositoryFake.Setup(x => x.GetById(It.IsAny<int>())).Returns(_conta);
            //Action
            _service.EfetuarSaque(_conta.Id, 1000);
            //Verify
            _repositoryFake.Verify(x => x.Update(_conta));
            _conta.SaldoTotal.Should().Be(ContaObjectMother.GetContaValida().SaldoTotal - 1000);
        }

        [Test]
        public void Service_Conta_GerarExtrato_DevePassar()
        {
            //Arrange
            _conta = ContaObjectMother.GetConta();
            _conta.Id = 1;

            //Action
            _repositoryFake.Setup(x => x.GetById(_conta.Id)).Returns(_conta);
           var extrato =  _service.GetExtrato(_conta.Id);
            //Verify
            extrato.Limite.Should().Be(_conta.Limite);
            extrato.NomeCliente.Should().Be(_conta.Cliente.Nome);
            extrato.NumeroConta.Should().Be(_conta.NumeroConta);
            extrato.Saldo.Should().Be(_conta.Saldo);
        }


        [Test]
        public void Service_Conta_EfetuarSaqueSemLimite_NãoDevePermitir()
        {
            //Arrange
            _conta = ContaObjectMother.GetContaValida();
            _repositoryFake.Setup(x => x.Update(It.IsAny<Conta>()));
            _repositoryFake.Setup(x => x.GetById(It.IsAny<int>())).Returns(_conta);
            //Action
            Action action =() =>_service.EfetuarSaque(_conta.Id, 5000);
            //Verify
            action.Should().Throw<ContaSaldoInsuficienteException>();
            _conta.SaldoTotal.Should().Be(ContaObjectMother.GetContaValida().SaldoTotal);
            _repositoryFake.Verify(pr => pr.GetById(_conta.Id), Times.Once);
            _repositoryFake.Verify(pr => pr.Update(It.IsAny<Conta>()), Times.Never);
            _repositoryFake.VerifyNoOtherCalls();
        }

        [Test]
        public void Service_Conta_EfetuarDeposito_DevePassar()
        {
            //Arrange
            _conta = ContaObjectMother.GetContaValida();
            _repositoryFake.Setup(x => x.Update(It.IsAny<Conta>()));
            _repositoryFake.Setup(x => x.GetById(It.IsAny<int>())).Returns(_conta);
            //Action
            _service.EfetuarDeposito(_conta.Id, 1000);
            //Verify
            _repositoryFake.Verify(x => x.Update(_conta));
            _conta.SaldoTotal.Should().Be(ContaObjectMother.GetContaValida().SaldoTotal + 1000);
        }

        [Test]
        public void Service_Conta_EfetuarTransferencia_DevePassar()
        {
            //Arrange
            _conta = ContaObjectMother.GetContaValida();
            Conta contaDestino = ContaObjectMother.GetContaComMovimentacao();
            contaDestino.Id = 2;

            int idContaOrigem = 1;
            int idContaDestino = 2;

            _repositoryFake.Setup(x => x.GetById(idContaOrigem)).Returns(_conta);
            _repositoryFake.Setup(x => x.GetById(idContaDestino)).Returns(contaDestino);

            _repositoryFake.Setup(x => x.Update(_conta)).Returns(true);
            _repositoryFake.Setup(x => x.Update(contaDestino)).Returns(true);
            //Action
            _service.EfetuarTrasferencia(_conta.Id, contaDestino.Id, 1000);
            //Verify
            _repositoryFake.Verify(x => x.GetById(_conta.Id));
            _repositoryFake.Verify(x => x.Update(_conta));
            _repositoryFake.Verify(x => x.Update(contaDestino));
            _conta.SaldoTotal.Should().Be(ContaObjectMother.GetContaValida().SaldoTotal - 1000);
            contaDestino.SaldoTotal.Should().Be(ContaObjectMother.GetContaComMovimentacao().SaldoTotal + 1000);

        }

        [Test]
        public void Service_Conta_EfetuarTransferenciaErroAoGravarSaque_NãoDevePermitir()
        {
            //Arrange
            _conta = ContaObjectMother.GetContaValida();
            Conta contaDestino = ContaObjectMother.GetContaValida();
            _repositoryFake.Setup(x => x.GetById(It.IsAny<int>())).Returns(_conta);
            contaDestino.Id = 2;
            _repositoryFake.Setup(x => x.Update(It.IsAny<Conta>())).Returns(false);
            //Action
            _service.EfetuarTrasferencia(_conta.Id, contaDestino.Id, 1000);
            //Verify
            _repositoryFake.Verify(x => x.Update(_conta));
        }

        [Test]
        public void Service_Conta_EfetuarTransferenciaSemLimite_NãoDevePermitir()
        {
            //Arrange
            _conta = ContaObjectMother.GetContaValida();
            Conta contaDestino = ContaObjectMother.GetContaComMovimentacao();
            contaDestino.Id = 2;
            int idContaOrigem = 1;
            int idContaDestino = 2;

            _repositoryFake.Setup(x => x.GetById(idContaOrigem)).Returns(_conta);
            _repositoryFake.Setup(x => x.GetById(idContaDestino)).Returns(contaDestino);

            _repositoryFake.Setup(x => x.Update(_conta)).Returns(false);
            _repositoryFake.Setup(x => x.Update(contaDestino)).Returns(false);
            //Action
            Action action = () => _service.EfetuarTrasferencia(_conta.Id, contaDestino.Id, 5000);
            //Verify
            action.Should().Throw<ContaSaldoInsuficienteException>();
            _repositoryFake.Verify(x => x.GetById(_conta.Id));
            _repositoryFake.Verify(x => x.GetById(contaDestino.Id));
            _conta.SaldoTotal.Should().Be(ContaObjectMother.GetContaValida().SaldoTotal);
            contaDestino.SaldoTotal.Should().Be(ContaObjectMother.GetContaComMovimentacao().SaldoTotal);
            _repositoryFake.VerifyNoOtherCalls();
        }

        [Test]
        public void Service_Conta_AlterarEstado_DevePassar()
        {
            //Arrange
            _conta = ContaObjectMother.GetContaValida();
            _repositoryFake.Setup(x => x.GetById(_conta.Id)).Returns(_conta);
            _repositoryFake.Setup(x => x.Update(_conta)).Returns(true);
            //Action
            _service.AlterarEstado(_conta.Id);
            //Verify
            _repositoryFake.Verify(x => x.Update(_conta));
            _conta.Ativada.Should().Be(!ContaObjectMother.GetContaValida().Ativada);
        }
    }
}
