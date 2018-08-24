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
    public class ClienteRemoveCommand
    {
        public int Id { get; set; }

        public virtual ValidationResult Validar()
        {
            return new Validator().Validate(this);
        }

        private class Validator : AbstractValidator<ClienteRemoveCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Id).NotNull().NotEmpty();
            }
        }
    }
}
