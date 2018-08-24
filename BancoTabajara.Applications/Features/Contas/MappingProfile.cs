using AutoMapper;
using BancoTabajara.Applications.Features.Contas.Commands;
using BancoTabajara.Applications.Features.Contas.Queries;
using BancoTabajara.Domain.Features.Contas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Applications.Features.Contas
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ContaRegisterCommand, Conta>();
            CreateMap<ContaUpdateCommand, Conta>();
            CreateMap<Conta, ContaQuery>();
        }
    }
}
