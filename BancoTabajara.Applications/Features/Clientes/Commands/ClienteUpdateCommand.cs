using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Applications.Features.Clientes.Commands
{
    [ExcludeFromCodeCoverage]
    public class ClienteUpdateCommand
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime DataNascimento { get; set; }

        public virtual ValidationResult Validar()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<ClienteUpdateCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Nome).NotNull().NotEmpty();
                RuleFor(c => c.CPF).NotNull().NotEmpty().Length(1, 12);
                RuleFor(c => c.RG).NotNull().NotEmpty().Length(1, 12);
                RuleFor(c => c.DataNascimento).NotNull().NotEmpty();
            }
        }
    }
}
