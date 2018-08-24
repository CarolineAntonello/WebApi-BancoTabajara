using BancoTabajara.Common.Tests.Features.Clientes;
using BancoTabajara.Domain.Features.Clientes;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Tests.Features.Clientes
{
    [TestFixture]
    public class ClienteTest
    {
        Cliente _cliente;
        [SetUp]
        public void Initialize()
        {
            _cliente = ClienteObjectMother.GetCliente();
        }

        [Test]
        public void Domain_Cliente_Validar_DevePassar()
        {
            //Action
            Action act = () => _cliente.Validar();
            //Verify
            act.Should().NotThrow<Exception>();
        }



        [Test]
        public void Domain_Cliente_Validar_ClienteSemNome_DeveJogarExcecao()
        {
            _cliente.Nome = "";
            //Action
            Action act = () => _cliente.Validar();
            //Verify
            act.Should().Throw<ClienteNomeVazioException>();
        }

        [Test]
        public void Domain_Cliente_Validar_ClienteSemCPF_DeveJogarExcecao()
        {
            _cliente.CPF = "";
            //Action
            Action act = () => _cliente.Validar();
            //Verify
            act.Should().Throw<ClienteCPFInvalidoException>();
        }

        [Test]
        public void Domain_Cliente_Validar_ClienteSemRG_DeveJogarExcecao()
        {
            _cliente.RG = "";
            //Action
            Action act = () => _cliente.Validar();
            //Verify
            act.Should().Throw<ClienteRGInvalidoException>();
        }

        [Test]
        public void Domain_Cliente_Validar_ClienteDataNascInvalido_DeveJogarExcecao()
        {
            _cliente.DataNascimento = DateTime.Now.AddDays(1);
            //Action
            Action act = () => _cliente.Validar();
            //Verify
            act.Should().Throw<ClienteDataNascimentoInvalidoException>();
        }
    }
}
