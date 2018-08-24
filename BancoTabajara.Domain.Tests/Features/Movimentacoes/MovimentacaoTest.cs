using BancoTabajara.Common.Tests.Features.Movimentacoes;
using BancoTabajara.Domain.Features.Movimentacoes;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Tests.Features.Movimentacoes
{
    [TestFixture]
    public class MovimentacaoTest
    {
        Movimentacao _movimentacao;

        [SetUp]
        public void Initialize()
        {
            _movimentacao = MovimentacaoObjectMother.GetMovimentacao();
        }

        [Test]
        public void Domain_Movimentacao_Validar_DevePassar()
        {
            //Action
            Action act = () => _movimentacao.Validar();
            //Verify
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void Domain_Movimentacao_Validar_MovimentacaoValorIgualAZero_DeveJogarExcecao()
        {
            _movimentacao.Valor = 0;
            //Action
            Action act = () => _movimentacao.Validar();
            //Verify
            act.Should().Throw<MovimentacaoValorInvalidoException>();
        }

        [Test]
        public void Domain_Movimentacao_Validar_MovimentacaoDataMenorQueAtual_DeveJogarExcecao()
        {
            _movimentacao.Data = DateTime.Now.AddDays(-1);
            //Action
            Action act = () => _movimentacao.Validar();
            //Verify
            act.Should().Throw<MovimentacaoDataInvalidaException>();
        }

        [Test]
        public void Domain_Movimentacao_Validar_MovimentacaoSemTipoOperacao_DeveJogarExcecao()
        {
            _movimentacao = MovimentacaoObjectMother.GetMovimentacaoSemTipoOperacao();
            //Action
            Action act = () => _movimentacao.Validar();
            //Verify
            act.Should().Throw<MovimentacaoTipoOperacaoInvalidaException>();
        }
       
    }
}
