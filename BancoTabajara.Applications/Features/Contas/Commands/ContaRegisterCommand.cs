using BancoTabajara.Applications.Features.Clientes.Commands;
using BancoTabajara.Applications.Features.Movimentacoes.Commands;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Applications.Features.Contas.Commands
{
    [ExcludeFromCodeCoverage]
    public class ContaRegisterCommand
    {
        public int NumeroConta { get; set; }
        public double Saldo { get; internal set; }
        public bool Ativada { get; set; }
        public double Limite { get; set; }
        public int ClienteId { get; set; }
        public virtual List<MovimentacaoRegisterCommand> Movimentacoes { get; set; }


        public virtual ValidationResult Validar()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<ContaRegisterCommand>
        {
            public Validator()
            {
                RuleFor(c => c.NumeroConta).NotNull().NotEmpty();
                RuleFor(c => c.Limite).NotNull().NotEmpty();
                RuleFor(c => c.ClienteId).NotNull().GreaterThan(0);            }
        }
    }
}
