using BancoTabajara.Domain.Features.Movimentacoes;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Applications.Features.Movimentacoes.Commands
{
    public class MovimentacaoUpdateCommand
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public TipoOperacaoEnum TipoOperacao { get; set; }

        public ValidationResult Validar()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<MovimentacaoUpdateCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Data).NotNull().NotEmpty();
                RuleFor(c => c.Valor).NotNull().NotEmpty();
                RuleFor(c => c.TipoOperacao).NotNull().NotEmpty();
            }
        }
    }
}