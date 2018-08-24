using AutoMapper;
using BancoTabajara.Applications.Features.Clientes.Commands;
using BancoTabajara.Applications.Features.Clientes.Queries;
using BancoTabajara.Applications.Features.Clientes.ViewModel;
using BancoTabajara.Domain.Features.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Applications.Features.Clientes
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClienteRegisterCommand, Cliente>();
            CreateMap<ClienteUpdateCommand, Cliente>();
            CreateMap<Cliente, ClienteViewModel>();
        }
    }
}
