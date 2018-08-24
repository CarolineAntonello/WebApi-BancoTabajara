using BancoTabajara.Common.Tests.Features.Clientes;
using BancoTabajara.Common.Tests.Features.Contas;
using BancoTabajara.Domain.Features.Clientes;
using BancoTabajara.Domain.Features.Contas;
using BancoTabajara.Domain.Features.Movimentacoes;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Tests.Features.Contas
{
    [TestFixture]
    public class ContaDomainTest
    {
        private Conta _conta;
        private Mock<Cliente> _cliente;

        [SetUp]
        public void Initialize()
        {
            _cliente = new Mock<Cliente>();
        }

        [Test]
        public void Domain_Conta_Validar_NumeroDaContaInvalida_DeveLancarExcessao()
        {
            //Arrange
            _conta = ContaObjectMother.GetContaNumeroContaNegativaComMock(_cliente.Object);
            //Action
            Action act = () => _conta.Validar();
            //Verify
            act.Should().Throw<ContaNumeroContaInvalidaException>();
        }

        [Test]
        public void Domain_Conta_GerarExtrato_DeveFuncionar()
        {
            //Arrange
            _conta = ContaObjectMother.GetContaNumeroContaNegativaComMock(_cliente.Object);
            _conta.Cliente = ClienteObjectMother.GetCliente();
            var extrato = _conta.GerarExtrato();
            //Verify
            extrato.Limite.Should().Be(_conta.Limite);
            extrato.NomeCliente.Should().Be(_conta.Cliente.Nome);
            extrato.NumeroConta.Should().Be(_conta.NumeroConta);
            extrato.Saldo.Should().Be(_conta.Saldo);
        }

        [Test]
        public void Domain_Conta_Validar_SaldoNegativo_DeveLancarExcessao()
        {
            //Arrange
            _conta = ContaObjectMother.GetContSaldoNegativoComMock(_cliente.Object);
            _conta.Limite = -100;
            //Action
            Action act = () => _conta.Validar();
            //Verify
            act.Should().Throw<ContaSaldoMenorQueZeroException>();
        }

        [Test]
        public void Domain_Conta_CalcularSaldoTotal_Deveria_Retornar_Saldo_mais_Limite()
        {
            //Action
            _conta = ContaObjectMother.GetContaComMock(_cliente.Object);
            //Verify
            _conta.SaldoTotal.Should().Be(_conta.Saldo + _conta.Limite);
        }

        [Test]
        public void Domain_Conta_Validar_LimiteNegativoDeveLancarExcessao()
        {
            //Arrange
            _conta = ContaObjectMother.GetContaLimiteNegativoComMock(_cliente.Object);
            _conta.RealizarDeposito(100);
            //Action
            Action act = () => _conta.Validar();
            //Verify
            act.Should().Throw<ContaLimiteInvalidoException>();
        }

        [Test]
        public void Domain_Conta_Validar_ContaSemClienteDeveLancarExcessao()
        {
            //Arrange
            _conta = ContaObjectMother.GetContaSemCliente();
            //Action
            Action act = () => _conta.Validar();
            //Verify
            act.Should().Throw<ContaClienteInvalidoException>();
        }

        [Test]
        public void Domain_Conta_Validar_DevePassar()
        {
            //Arrange
            _conta = ContaObjectMother.GetContaComMock(_cliente.Object);
            //Action
            Action act = () => _conta.Validar();
            //Verify
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void Domain_Conta_AlterarEstado_Deveria_Mudar_Conta_para_Inativa()
        {
            //Arrange
            _conta = ContaObjectMother.GetContaComMock(_cliente.Object);
            //Action
            _conta.AlterarEstado();
            //Verify
            _conta.Ativada.Should().BeFalse();
        }

        [Test]
        public void Domain_Conta_AlterarEstado_Deveria_Mudar_Conta_para_Ativa()
        {
            //Action
            _conta = ContaObjectMother.GetContaInativaComMock(_cliente.Object);
            _conta.AlterarEstado();
            //Verify
            _conta.Ativada.Should().BeTrue();
        }

        [Test]
        public void Domain_Conta_RealizarDeposito_Deveria_Adicionar_Valor_No_Saldo_E_Criar_Movimentacao()
        {
            //Action
            _conta = ContaObjectMother.GetContaComMock(_cliente.Object);
            _conta.RealizarDeposito(100);
            //Verify
            _conta.Saldo.Should().Be(ContaObjectMother.GetContaComMock(_cliente.Object).Saldo + 100);
            _conta.Movimentacoes.Count().Should().BeGreaterThan(0);
            _conta.Movimentacoes.First().TipoOperacao.Should().Be(TipoOperacaoEnum.Credito);
            _conta.Movimentacoes.First().Valor.Should().Be(100);
        }

        [Test]
        public void Domain_Conta_RealizarSaque_Deveria_Remover_Valor_Do_Saldo_E_Criar_Movimentacao()
        {
            //Action
            _conta = ContaObjectMother.GetContaComMock(_cliente.Object);
            _conta.RealizarSaque(100);
            //Verify
            _conta.Saldo.Should().Be(ContaObjectMother.GetContaComMock(_cliente.Object).Saldo - 100);
            _conta.Movimentacoes.Count().Should().BeGreaterThan(0);
            _conta.Movimentacoes.First().TipoOperacao.Should().Be(TipoOperacaoEnum.Debito);
            _conta.Movimentacoes.First().Valor.Should().Be(100);
        }

        [Test]
        public void Domain_Conta_RealizarSaque_ComSaldoInsuficiente_Nao_Deveria_Permitir_Nao_Deveria_Criar_Movimentacao()
        {
            //Action
            _conta = ContaObjectMother.GetContaComMock(_cliente.Object);
            Action action = () => _conta.RealizarSaque(5000);
            //Verify
            action.Should().Throw<ContaSaldoInsuficienteException>();
            _conta.Saldo.Should().Be(ContaObjectMother.GetContaComMock(_cliente.Object).Saldo);
            _conta.Movimentacoes.Count().Should().Be(0);
        }

        [Test]
        public void Domain_Conta_RealizarTransferencia_Deveria_Debitar_De_Conta_E_Creditar_Em_Outra()
        {
            //Action
            Conta contadestino = ContaObjectMother.GetContaComMock(_cliente.Object);
            _conta = ContaObjectMother.GetContaComMock(_cliente.Object);
            _conta.RealizarTransferencia(100, contadestino);
            //Verify
            _conta.Saldo.Should().Be(ContaObjectMother.GetContaComMock(_cliente.Object).Saldo - 100);
            _conta.Movimentacoes.Count().Should().BeGreaterThan(0);
            _conta.Movimentacoes.First().TipoOperacao.Should().Be(TipoOperacaoEnum.Debito);
            _conta.Movimentacoes.First().Valor.Should().Be(100);
            contadestino.Saldo.Should().Be(ContaObjectMother.GetContaComMock(_cliente.Object).Saldo + 100);
            contadestino.Movimentacoes.Count().Should().BeGreaterThan(0);
            contadestino.Movimentacoes.First().TipoOperacao.Should().Be(TipoOperacaoEnum.Credito);
            contadestino.Movimentacoes.First().Valor.Should().Be(100);
        }

        [Test]
        public void Domain_Conta_RealizarTransferencia_ComSaldoInsuficiente_Nao_Deveria_Permitir_Nao_Deveria_Criar_Movimentacao()
        {
            //Action
            Conta contadestino = ContaObjectMother.GetContaComMock(_cliente.Object);
            _conta = ContaObjectMother.GetContaComMock(_cliente.Object);
            Action action = () => _conta.RealizarTransferencia(5000, contadestino);
            //Verify
            action.Should().Throw<ContaSaldoInsuficienteException>();
            _conta.Saldo.Should().Be(ContaObjectMother.GetContaComMock(_cliente.Object).Saldo);
            _conta.Movimentacoes.Count().Should().Be(0);
            contadestino.Saldo.Should().Be(ContaObjectMother.GetContaComMock(_cliente.Object).Saldo);
            contadestino.Movimentacoes.Count().Should().Be(0);
        }

        [Test]
        public void Domain_Conta_VerificaNumeroConta_Nao_Deveria_Permitir_Alterar_Numero_Conta()
        {
            //Action
            _conta = ContaObjectMother.GetContaComMock(_cliente.Object);
            Action action = () => _conta.VerificaNumeroConta(777);
            //Verify
            action.Should().Throw<ContaNumeroAlteradoException>();
        }

        [Test]
        public void Domain_Conta_VerificaNumeroConta_Deveria_Permitir_Numero_Conta_Igual()
        {
            //Action
            _conta = ContaObjectMother.GetContaComMock(_cliente.Object);
            _conta.VerificaNumeroConta(666);           
        }
    }
}

