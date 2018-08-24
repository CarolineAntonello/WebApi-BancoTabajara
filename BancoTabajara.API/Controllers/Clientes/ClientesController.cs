using BancoTabajara.API.Controllers.Common;
using BancoTabajara.API.Extensions;
using BancoTabajara.API.Filters;
using BancoTabajara.Applications.Features.Clientes;
using BancoTabajara.Applications.Features.Clientes.Commands;
using BancoTabajara.Applications.Features.Clientes.ViewModel;
using BancoTabajara.Domain.Features.Clientes;
using log4net;
using System.Web.Http;

namespace BancoTabajara.API.Controllers.Clientes
{
    [CustomAuthorize]
    [RoutePrefix("api/clientes")]
    public class ClientesController : ApiControllerBase
    {
        private IClienteService _clienteService;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public IHttpActionResult Post(ClienteRegisterCommand clienteCmd)
        {
            _log.Info("Iniciando Post de cliente!");
            _log.Info("Validando dados do cliente!");
            var validator = clienteCmd.Validar();
            if (!validator.IsValid)
            {
                _log.Error("Dado invalido: " + validator.Errors[0]);
                return HandleValidationFailure(validator.Errors);
            }
            _log.Info("Dados do cliente válidos.");
            _log.Info("Adicionando cliente");
            return HandleCallback(() => _clienteService.Add(clienteCmd));
        }

        [HttpPut]
        public IHttpActionResult Put(ClienteUpdateCommand clienteCmd)
        {
            _log.Info("Iniciando Put de cliente!");
            _log.Info("Validando dados do cliente!");
            var validator = clienteCmd.Validar();
            if (!validator.IsValid)
            {
                _log.Error("Dado invalido: " + validator.Errors[0]);
                return HandleValidationFailure(validator.Errors);
            }
            _log.Info("Dados do cliente válidos!");
            _log.Info("Atualizando cliente!");
            return HandleCallback(() => _clienteService.Update(clienteCmd));
        }

        [HttpDelete]
        public IHttpActionResult Delete(ClienteRemoveCommand clienteCmd)
        {
            _log.Info("Iniciando Delete do cliente!");
            _log.Info("Validando dados do cliente!");
            var validator = clienteCmd.Validar();
            if (!validator.IsValid)
            {
                _log.Error("Dado invalido: " + validator.Errors[0]);
                return HandleValidationFailure(validator.Errors);
            }
            _log.Info("Dados do cliente válidos!");
            _log.Info("Deletando cliente!");
            return HandleCallback(() => _clienteService.Delete(clienteCmd));
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            _log.Info("Obtendo dados de todos os cliente por get");
            var quantidade = Request.GetQueryQuantidadeExtension();
            return HandleQueryable<Cliente, ClienteViewModel>(_clienteService.GetAll(quantidade));
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            _log.Info("Obtendo dados do cliente por get");
            return HandleQuery<Cliente, ClienteViewModel>(_clienteService.GetById(id));
        }
    }
}
