using BancoTabajara.API.Controllers.Common;
using BancoTabajara.API.Extensions;
using BancoTabajara.API.Filters;
using BancoTabajara.Applications.Features.Contas;
using BancoTabajara.Applications.Features.Contas.Commands;
using BancoTabajara.Applications.Features.Contas.ViewModel;
using BancoTabajara.Domain.Features.Contas;
using log4net;
using System.Web.Http;

namespace BancoTabajara.API.Controllers.Contas
{
    [CustomAuthorize]
    [RoutePrefix("api/contas")]
    public class ContasController : ApiControllerBase
    {
        private IContaService _contaService;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ContasController(IContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpPost]
        public IHttpActionResult Post(ContaRegisterCommand conta)
        {
            _log.Info("Iniciando Post de Conta!");
            _log.Info("Validando dados da Conta!");
            var validator = conta.Validar();
            if (!validator.IsValid)
            {
                _log.Error("Dado invalido: " + validator.Errors[0]);
                return HandleValidationFailure(validator.Errors);
            }
            _log.Info("Dados da conta válidos.");
            _log.Info("Adicionando conta");
            return HandleCallback(() => _contaService.Add(conta));
        }

        [HttpPut]
        public IHttpActionResult Put(ContaUpdateCommand conta)
        {
            _log.Info("Atualizando Conta!");
            _log.Info("Validando dados da Conta!");
            var validator = conta.Validar();
            if (!validator.IsValid)
            {
                _log.Error("Dado invalido: " + validator.Errors[0]);
                return HandleValidationFailure(validator.Errors);
            }
            _log.Info("Dados da conta válidos.");
            _log.Info("Atualizando conta");
            return HandleCallback(() => _contaService.Update(conta));
        }

        [HttpDelete]
        public IHttpActionResult Delete(ContaRemoveCommand conta)
        {
            _log.Info("Deletando Conta!");
            _log.Info("Validando dados da Conta!");
            var validator = conta.Validar();
            if (!validator.IsValid)
            {
                _log.Error("Dado invalido: " + validator.Errors[0]);
                return HandleValidationFailure(validator.Errors);
            }
            _log.Info("Dados da conta válidos.");
            _log.Info("Deletando conta");
            return HandleCallback(() => _contaService.Delete(conta));
        }

        [HttpPatch]
        [Route("{id:int}/deposito")]
        public IHttpActionResult EfetuarDeposito(int id, [FromBody]double valor)
        {
            _log.Info("Efetuando Depósito da Conta!");
            return HandleCallback(() => _contaService.EfetuarDeposito(id, valor));

        }

        [HttpPatch]
        [Route("{id:int}/saque")]
        public IHttpActionResult EfetuarSaque(int id, [FromBody]double valor)
        {
            _log.Info("Efetuando Saque da Conta!");
            return HandleCallback(() => _contaService.EfetuarSaque(id, valor));
        }

        [HttpPatch]
        [Route("{idOrigem:int}/{idDestino:int}/tranferencia")]
        public IHttpActionResult EfetuarTransferencia(int idOrigem, int idDestino, [FromBody]double valor)
        {
            _log.Info("Efetuando Transferência da Conta!");
            return HandleCallback(() => _contaService.EfetuarTrasferencia(idOrigem, idDestino, valor));
        }

        [HttpPatch]
        [Route("{id:int}/estado")]
        public IHttpActionResult AlterarEstado(int id)
        {
            _log.Info("Alterando estado da Conta!");
            return HandleCallback(() => _contaService.AlterarEstado(id));
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            _log.Info("Obtendo dados de todos os conta por get");
            var quantidade = Request.GetQueryQuantidadeExtension();
            return HandleQueryable<Conta, ContaViewModel>(_contaService.GetAll(quantidade));
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            _log.Info("Obtendo dados do conta por get");
            return HandleQuery<Conta, ContaViewModel>(_contaService.GetById(id));
        }

        [HttpGet]
        [Route("{id:int}/extrato")]
        public IHttpActionResult GetExtrato(int id)
        {
            _log.Info("Gerando extrato da conta");
            return HandleCallback(() => _contaService.GetExtrato(id));
        }
    }
}
