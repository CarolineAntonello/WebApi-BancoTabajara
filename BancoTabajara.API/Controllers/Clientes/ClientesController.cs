using BancoTabajara.API.Controllers.Common;
using BancoTabajara.API.Extensions;
using BancoTabajara.API.Filters;
using BancoTabajara.Applications.Features.Clientes;
using BancoTabajara.Applications.Features.Clientes.Commands;
using BancoTabajara.Applications.Features.Clientes.ViewModel;
using BancoTabajara.Domain.Features.Clientes;
using BancoTabajara.Infra.Logger;
using log4net;
using System.Web.Http;

namespace BancoTabajara.API.Controllers.Clientes
{
    [CustomAuthorize]
    [RoutePrefix("api/clientes")]
    public class ClientesController : ApiControllerBase
    {
        private IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public IHttpActionResult Post(ClienteRegisterCommand clienteCmd)
        {
            TraceLogManager.Info("Iniciando Post de cliente!");
            TraceLogManager.Info("Validando dados do cliente!");
            var validator = clienteCmd.Validar();
            if (!validator.IsValid)
            {
                TraceLogManager.Error("Dado invalido: " + validator.Errors[0]);
                return HandleValidationFailure(validator.Errors);
            }
            TraceLogManager.Info("Dados do cliente válidos.");
            TraceLogManager.Info("Adicionando cliente");
            return HandleCallback(() => _clienteService.Add(clienteCmd));
        }

        [HttpPut]
        public IHttpActionResult Put(ClienteUpdateCommand clienteCmd)
        {
            TraceLogManager.Info("Iniciando Put de cliente!");
            TraceLogManager.Info("Validando dados do cliente!");
            var validator = clienteCmd.Validar();
            if (!validator.IsValid)
            {
                TraceLogManager.Error("Dado invalido: " + validator.Errors[0]);
                return HandleValidationFailure(validator.Errors);
            }
            TraceLogManager.Info("Dados do cliente válidos!");
            TraceLogManager.Info("Atualizando cliente!");
            return HandleCallback(() => _clienteService.Update(clienteCmd));
        }

        [HttpDelete]
        public IHttpActionResult Delete(ClienteRemoveCommand clienteCmd)
        {
            TraceLogManager.Info("Iniciando Delete do cliente!");
            TraceLogManager.Info("Validando dados do cliente!");
            var validator = clienteCmd.Validar();
            if (!validator.IsValid)
            {
                TraceLogManager.Error("Dado invalido: " + validator.Errors[0]);
                return HandleValidationFailure(validator.Errors);
            }
            TraceLogManager.Info("Dados do cliente válidos!");
            TraceLogManager.Info("Deletando cliente!");
            return HandleCallback(() => _clienteService.Delete(clienteCmd));
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            TraceLogManager.Info("Obtendo dados de todos os cliente por get");
            var quantidade = Request.GetQueryQuantidadeExtension();
            return HandleQueryable<Cliente, ClienteViewModel>(_clienteService.GetAll(quantidade));
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            TraceLogManager.Info("Obtendo dados do cliente por get");
            return HandleQuery<Cliente, ClienteViewModel>(_clienteService.GetById(id));
        }
    }
}
