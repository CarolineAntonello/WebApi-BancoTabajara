using BancoTabajara.Applications.Features.Clientes.Commands;
using BancoTabajara.Domain.Features.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Common.Tests.Features.Clientes
{
    public static partial class ClienteObjectMother
    {
        public static Cliente GetCliente()
        {
            return new Cliente()
            {
                Nome = "Lucas",
                CPF = "75894099013",
                RG = "232095474",
                DataNascimento = DateTime.Now.AddYears(-20)
            };
        }

        public static Cliente GetClienteSemNome()
        {
            return new Cliente()
            {
                CPF = "75894099013",
                RG = "232095474",
                DataNascimento = DateTime.Now.AddYears(-20)
            };
        }

        public static Cliente GetClienteSemRG()
        {
            return new Cliente()
            {
                Nome = "Lucas",
                CPF = "75894099013",
                DataNascimento = DateTime.Now.AddYears(-20)
            };
        }

        public static Cliente GetClienteSemCPF()
        {
            return new Cliente()
            {
                Nome = "Lucas",
                RG = "232095474",
                DataNascimento = DateTime.Now.AddYears(-20)
            };
        }

        public static Cliente GetClienteDataInvalida()
        {
            return new Cliente()
            {
                Nome = "Lucas",
                CPF = "75894099013",
                RG = "232095474",
                DataNascimento = DateTime.Now.AddDays(5)
            };
        }



        public static Cliente GetClienteValido()
        {
            return new Cliente()
            {
                Id = 1,
                Nome = "Lucas",
                CPF = "75894099013",
                RG = "232095474",
                DataNascimento = DateTime.Now.AddYears(-20)
            };
        }

        public static ClienteRegisterCommand GetClienteValidoParaRegistrar()
        {
            return new ClienteRegisterCommand()
            {
                Nome = "Lucas",
                CPF = "75894099013",
                RG = "232095474",
                DataNascimento = DateTime.Now.AddYears(-20)
            };
        }

        public static ClienteRemoveCommand GetClienteValidoParaDeletar()
        {
            return new ClienteRemoveCommand()
            {
                Id = 1
            };
        }

        public static ClienteUpdateCommand GetClienteValidoParaAtualizar()
        {
            return new ClienteUpdateCommand()
            {
                Id = 1,
                Nome = "Lucas Varela",
                CPF = "75894099013",
                RG = "232095474",
                DataNascimento = DateTime.Now.AddYears(-21)
            };
        }

    }
}
