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
    public class ContaRemoveCommand
    {
        public int Id { get; set; }

        public virtual ValidationResult Validar()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<ContaRemoveCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Id).NotNull().NotEmpty();
            }
        }
    }
}