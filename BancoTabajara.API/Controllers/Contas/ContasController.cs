using BancoTabajara.API.Controllers.Common;
using BancoTabajara.API.Extensions;
using BancoTabajara.API.Filters;
using BancoTabajara.Applications.Features.Contas;
using BancoTabajara.Applications.Features.Contas.Commands;
using BancoTabajara.Applications.Features.Contas.ViewModel;
using BancoTabajara.Domain.Features.Contas;
using BancoTabajara.Infra.Logger;
using log4net;
using System.Web.Http;

namespace BancoTabajara.API.Controllers.Contas
{
    [CustomAuthorize]
    [RoutePrefix("api/contas")]
    public class ContasController : ApiControllerBase
    {
        private IContaService _contaService;

        public ContasController(IContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpPost]
        public IHttpActionResult Post(ContaRegisterCommand conta)
        {
            TraceLogManager.Info("Iniciando Post de Conta!");
            TraceLogManager.Info("Validando dados da Conta!");
            var validator = conta.Validar();
            if (!validator.IsValid)
            {
                TraceLogManager.Error("Dado invalido: " + validator.Errors[0]);
                return HandleValidationFailure(validator.Errors);
            }
            TraceLogManager.Info("Dados da conta válidos.");
            TraceLogManager.Info("Adicionando conta");
            return HandleCallback(() => _contaService.Add(conta));
        }

        [HttpPut]
        public IHttpActionResult Put(ContaUpdateCommand conta)
        {
            TraceLogManager.Info("Atualizando Conta!");
            TraceLogManager.Info("Validando dados da Conta!");
            var validator = conta.Validar();
            if (!validator.IsValid)
            {
                TraceLogManager.Error("Dado invalido: " + validator.Errors[0]);
                return HandleValidationFailure(validator.Errors);
            }
            TraceLogManager.Info("Dados da conta válidos.");
            TraceLogManager.Info("Atualizando conta");
            return HandleCallback(() => _contaService.Update(conta));
        }

        [HttpDelete]
        public IHttpActionResult Delete(ContaRemoveCommand conta)
        {
            TraceLogManager.Info("Deletando Conta!");
            TraceLogManager.Info("Validando dados da Conta!");
            var validator = conta.Validar();
            if (!validator.IsValid)
            {
                TraceLogManager.Error("Dado invalido: " + validator.Errors[0]);
                return HandleValidationFailure(validator.Errors);
            }
            TraceLogManager.Info("Dados da conta válidos.");
            TraceLogManager.Info("Deletando conta");
            return HandleCallback(() => _contaService.Delete(conta));
        }

        [HttpPatch]
        [Route("{id:int}/deposito")]
        public IHttpActionResult EfetuarDeposito(int id, [FromBody]double valor)
        {
            TraceLogManager.Info("Efetuando Depósito da Conta!");
            return HandleCallback(() => _contaService.EfetuarDeposito(id, valor));

        }

        [HttpPatch]
        [Route("{id:int}/saque")]
        public IHttpActionResult EfetuarSaque(int id, [FromBody]double valor)
        {
            TraceLogManager.Info("Efetuando Saque da Conta!");
            return HandleCallback(() => _contaService.EfetuarSaque(id, valor));
        }

        [HttpPatch]
        [Route("{idOrigem:int}/{idDestino:int}/tranferencia")]
        public IHttpActionResult EfetuarTransferencia(int idOrigem, int idDestino, [FromBody]double valor)
        {
            TraceLogManager.Info("Efetuando Transferência da Conta!");
            return HandleCallback(() => _contaService.EfetuarTrasferencia(idOrigem, idDestino, valor));
        }

        [HttpPatch]
        [Route("{id:int}/estado")]
        public IHttpActionResult AlterarEstado(int id)
        {
            TraceLogManager.Info("Alterando estado da Conta!");
            return HandleCallback(() => _contaService.AlterarEstado(id));
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            TraceLogManager.Info("Obtendo dados de todos os conta por get");
            var quantidade = Request.GetQueryQuantidadeExtension();
            return HandleQueryable<Conta, ContaViewModel>(_contaService.GetAll(quantidade));
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            TraceLogManager.Info("Obtendo dados do conta por get");
            return HandleQuery<Conta, ContaViewModel>(_contaService.GetById(id));
        }

        [HttpGet]
        [Route("{id:int}/extrato")]
        public IHttpActionResult GetExtrato(int id)
        {
            TraceLogManager.Info("Gerando extrato da conta");
            return HandleCallback(() => _contaService.GetExtrato(id));
        }
    }
}
