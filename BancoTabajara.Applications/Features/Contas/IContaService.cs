using BancoTabajara.Applications.Features.Contas.Commands;
using BancoTabajara.Domain.Features.Contas;
using BancoTabajara.Domain.Features.Extratos;
using BancoTabajara.Domain.Features.Movimentacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Applications.Features.Contas
{
    public interface IContaService
    {
        int Add(ContaRegisterCommand conta);

        bool Update(ContaUpdateCommand conta);

        bool Delete(ContaRemoveCommand conta);

        Conta GetById(int id);

        IQueryable<Conta> GetAll(int quantidade = 0);

        bool AlterarEstado(int id);

        bool EfetuarDeposito(int id, double valor);

        bool EfetuarSaque(int id, double valor);

        bool EfetuarTrasferencia(int idOrigem, int idDestino, double valor);

        Extrato GetExtrato(int contaID);
    }
}
