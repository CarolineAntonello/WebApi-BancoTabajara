using BancoTabajara.Applications.Features.Contas.Commands;
using BancoTabajara.Applications.Features.Movimentacoes.Commands;
using BancoTabajara.Common.Tests.Features.Clientes;
using BancoTabajara.Domain.Features.Clientes;
using BancoTabajara.Domain.Features.Contas;
using BancoTabajara.Domain.Features.Movimentacoes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Common.Tests.Features.Contas
{
    public class ContaObjectMother
    {
        public static Conta GetConta()
        {
            return new Conta
            {
                Ativada = true,
                NumeroConta = 666,
                Limite = 1000,

                Cliente = new Cliente
                {
                    Nome = "Thiago",
                    CPF = "75894099013",
                    RG = "232095474",
                    DataNascimento = DateTime.Now.AddYears(-20)
                }

            };
        }

        public static Conta GetContaNumeroContaNegativaComMock(Cliente cliente)
        {
            return new Conta
            {
                Cliente = cliente,
                Ativada = true,
                NumeroConta = -666,
                Limite = 1000

            };
        }

        public static Conta GetContSaldoNegativoComMock(Cliente cliente)
        {
            return new Conta
            {
                Cliente = cliente,
                Ativada = true,
                NumeroConta = 666,
                Limite = 1000

            };
        }

        public static Conta GetContaComMock(Cliente cliente)
        {
            return new Conta
            {
                Cliente = cliente,
                Ativada = true,
                NumeroConta = 666,
                Limite = 1000


            };
        }

        public static Conta GetContaInativaComMock(Cliente cliente)
        {
            return new Conta
            {
                Cliente = cliente,
                Ativada = false,
                NumeroConta = 666,
                Limite = 1000

            };
        }

        public static Conta GetContaLimiteNegativo()
        {
            return new Conta
            {
                Cliente = ClienteObjectMother.GetCliente(),
                Ativada = true,
                NumeroConta = 666,
                Limite = -10

            };
        }

        public static Conta GetContaLimiteNegativoComMock(Cliente cliente)
        {
            return new Conta
            {
                Cliente = cliente,
                Ativada = true,
                NumeroConta = 666,
                Limite = -10

            };
        }

        public static Conta GetContaSemCliente()
        {
            return new Conta
            {
                Ativada = true,
                NumeroConta = 666,
                Limite = 1000

            };
        }

        public static Conta GetContaComMovimentacao()
        {
            return new Conta
            {
                Cliente = ClienteObjectMother.GetCliente(),
                Ativada = true,
                NumeroConta = 666,
                Limite = 1000,

                Movimentacoes = new List<Movimentacao>
                {
                    new Movimentacao
                    {
                        Id = 1,
                        Data = DateTime.Now.AddDays(-1),
                        Valor = 100,
                        TipoOperacao = TipoOperacaoEnum.Credito
                    },
                    new Movimentacao
                    {
                        Id = 2,
                        Data = DateTime.Now.AddDays(-3),
                        Valor = 150,
                        TipoOperacao = TipoOperacaoEnum.Debito
                    }
                }

            };
        }



        public static Conta GetContaValida()
        {
            return new Conta
            {
                Id = 1,
                Cliente = ClienteObjectMother.GetClienteValido(),
                Ativada = true,
                NumeroConta = 666,
                Limite = 1000,

                Movimentacoes = new List<Movimentacao>
                {
                    new Movimentacao
                    {
                        Id = 1,
                        Data = DateTime.Now.AddDays(-1),
                        Valor = 100,
                        TipoOperacao = TipoOperacaoEnum.Credito
                    },
                    new Movimentacao
                    {
                        Id = 2,
                        Data = DateTime.Now.AddDays(-3),
                        Valor = 150,
                        TipoOperacao = TipoOperacaoEnum.Debito
                    }
                }
            };
        }

        public static ContaRegisterCommand GetContaValidaParaRegistrar()
        {
            return new ContaRegisterCommand
            {
                ClienteId = ClienteObjectMother.GetClienteValidoParaAtualizar().Id,
                Ativada = true,
                NumeroConta = 666,
                Limite = 1000,
                Movimentacoes = new List<MovimentacaoRegisterCommand>
                {
                    new MovimentacaoRegisterCommand
                    {
                        
                        Data = DateTime.Now.AddDays(-1),
                        Valor = 100,
                        TipoOperacao = TipoOperacaoEnum.Credito
                    },
                    new MovimentacaoRegisterCommand
                    {
                       
                        Data = DateTime.Now.AddDays(-3),
                        Valor = 150,
                        TipoOperacao = TipoOperacaoEnum.Debito
                    }
                }
            };
        }
    

        public static ContaUpdateCommand GetContaValidaParaAtualizar()
        {
            return new ContaUpdateCommand
            {
                Id = 1,
                ClienteId = ClienteObjectMother.GetClienteValidoParaAtualizar().Id,
                Ativada = true,
                NumeroConta = 666,
                Limite = 10000,

                Movimentacoes = new List<MovimentacaoUpdateCommand>
                {
                    new MovimentacaoUpdateCommand
                    {
                        Id = 1,
                        Data = DateTime.Now.AddDays(-1),
                        Valor = 1000,
                        TipoOperacao = TipoOperacaoEnum.Credito
                    },
                    new MovimentacaoUpdateCommand
                    {
                        Id = 2,
                        Data = DateTime.Now.AddDays(-3),
                        Valor = 1500,
                        TipoOperacao = TipoOperacaoEnum.Debito
                    }
                }
            };
        }

        public static ContaRemoveCommand GetContaValidaParaDeletar()
        {
            return new ContaRemoveCommand()
            {
                Id = 1
            };
        }

    }
}
