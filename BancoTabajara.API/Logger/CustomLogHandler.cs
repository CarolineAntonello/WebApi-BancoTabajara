using BancoTabajara.API.Exceptions;
using BancoTabajara.Infra.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BancoTabajara.API.Logger
{
    public class CustomLogHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //Cria o objeto de log conforme o request
            var logMetadata = BuildLogMetadata(request);

            //Inicia a escrita do log no inicio do request
            WriteStartLog(logMetadata);

            return await base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    //Pega a resposta do request
                    var response = task.Result;

                    // Atualiza o log metadata com as informações da resposta.
                    logMetadata.ResponseStatusCode = (int)response.StatusCode;
                    logMetadata.ResponseTimestamp = DateTime.Now;

                    if (response.Content != null && response.Content is ObjectContent<ExceptionPayload>)
                    {
                        //Se tiver uma exception aqui ele atribui a mensagem do ExceptionPayLoad
                        logMetadata.ResponseExceptionPayLoad = (response.Content as ObjectContent<ExceptionPayload>).Value as ExceptionPayload;
                    }

                    //Termina a escrita do log no fim do request
                    WriteEndLog(logMetadata);

                    return response;
                }, cancellationToken);
        }

        // Private methods

        /// Esse método é responsável por contruir o objeto de LogMetaData
        private LogMetadata BuildLogMetadata(HttpRequestMessage request)
        {
            return new LogMetadata
            {
                RequestMethod = request.Method.Method,
                RequestUri = request.RequestUri.ToString(),
                RequestTimestamp = DateTime.Now
            };
        }

        /// Esse método é responsável pela escrita inicial do request
        private void WriteStartLog(LogMetadata logMetadata)
        {
            // Custumização da mensagem
            var message = string.Format("[{0}] - Início: {1}", logMetadata.RequestMethod, logMetadata.RequestUri);

            TraceLogManager.Debug(message);
        }

        /// Esse método é responsável pela escrita final do request
        private void WriteEndLog(LogMetadata logMetadata)
        {
            // Verifica se o request teve uma exceção
            if (logMetadata.ResponseExceptionPayLoad != null)
            {
                TraceLogManager.Error("[{0}] - Exception - Status: {1} - Message: {2}\r\nStackTrace: {3}", logMetadata.RequestMethod, logMetadata.ResponseExceptionPayLoad.ErrorCode,
                    logMetadata.ResponseExceptionPayLoad.ErrorMessage, logMetadata.ResponseExceptionPayLoad.Exception.StackTrace);
            }

            //Pega o tempo de exceção do request
            var executionTime = logMetadata.ResponseTimestamp.Subtract(logMetadata.RequestTimestamp);

            // Custumização da mensagem
            var message = string.Format("[{0}] - Fim: {1} [Tempo de Execução: {2}] [Status: {3}]", logMetadata.RequestMethod, logMetadata.RequestUri, executionTime, logMetadata.ResponseStatusCode);

            TraceLogManager.Debug(message);
        }
    }
}