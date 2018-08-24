using AutoMapper;
using BancoTabajara.Applications.Features.Movimentacoes.Commands;
using BancoTabajara.Domain.Features.Movimentacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Applications.Features.Movimentacoes
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MovimentacaoRegisterCommand, Movimentacao>();
            CreateMap<MovimentacaoUpdateCommand, Movimentacao>();
           
        }
    }
}
