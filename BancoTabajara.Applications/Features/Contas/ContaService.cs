using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BancoTabajara.Applications.Features.Contas.Commands;
using BancoTabajara.Domain.Exceptions;
using BancoTabajara.Domain.Features.Clientes;
using BancoTabajara.Domain.Features.Contas;
using BancoTabajara.Domain.Features.Extratos;
using BancoTabajara.Domain.Features.Movimentacoes;

namespace BancoTabajara.Applications.Features.Contas
{
    public class ContaService : IContaService
    {
        IContaRepository _repository;
        IClienteRepository _repositoryCliente;

        public ContaService(IContaRepository repository, IClienteRepository repositoryCliente)
        {
            _repository = repository;
            _repositoryCliente = repositoryCliente;
        }

        public int Add(ContaRegisterCommand contaCmd)
        {
            var conta = Mapper.Map<ContaRegisterCommand, Conta>(contaCmd);
            conta.Cliente = _repositoryCliente.GetById(contaCmd.ClienteId) ?? throw new NotFoundException();
            var novaConta = _repository.Add(conta);

            return novaConta.Id;
        }
        public bool Update(ContaUpdateCommand contaCmd)
        {
            // Obtém a entidade Indexada pelo EF e valida
            var contaDb = _repository.GetById(contaCmd.Id) ?? throw new NotFoundException();
            var cliente = _repositoryCliente.GetById(contaCmd.ClienteId) ?? throw new NotFoundException();

            contaDb.VerificaNumeroConta(contaCmd.NumeroConta);
            // Mapeia para o objeto do banco
            Mapper.Map(contaCmd, contaDb);
            contaDb.Cliente = cliente;
            
            return _repository.Update(contaDb);
        }

        public bool Delete(ContaRemoveCommand contaCmd)
        {
            return _repository.Delete(contaCmd.Id);
        }

        public IQueryable<Conta> GetAll(int quantidade)
        {
            return _repository.GetAll(quantidade);
        }

        public Conta GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool AlterarEstado(int id)
        {
            var conta = _repository.GetById(id);
            conta.AlterarEstado();
            return _repository.Update(conta);
        }

        public bool EfetuarDeposito(int id, double valor)
        {
            var conta = _repository.GetById(id);
            conta.RealizarDeposito(valor);
            conta.Validar();
            return _repository.Update(conta);
        }

        public bool EfetuarSaque(int id, double valor)
        {
            var conta = _repository.GetById(id);
            conta.RealizarSaque(valor);
            conta.Validar();
            return _repository.Update(conta);
        }

        public bool EfetuarTrasferencia(int idOrigem, int idDestino, double valor)
        {
            var contaOrigem = _repository.GetById(idOrigem);
            var contaDestino = _repository.GetById(idDestino);
            contaOrigem.RealizarTransferencia(valor, contaDestino);
            contaOrigem.Validar();
            var resultado = _repository.Update(contaOrigem);
            if (resultado)
            {
                contaDestino.Validar();
                return _repository.Update(contaDestino);
            }
            else
                return resultado;
        }

        public Extrato GetExtrato(int contaID)
        {
            var conta = _repository.GetById(contaID);
            return conta.GerarExtrato();
        }
    }
}
