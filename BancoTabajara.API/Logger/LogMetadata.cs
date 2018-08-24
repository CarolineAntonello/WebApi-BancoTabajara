using BancoTabajara.API.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BancoTabajara.API.Logger
{
    /// Classe responsável por carregar as propriedades pertinentes na hora de montar uma mensagem de log
    public class LogMetadata
    {
        /// The request URI.
        public string RequestUri { get; set; }

        /// The request method (GET, POST, etc).
        public string RequestMethod { get; set; }

        /// The request timestamp.
        public DateTime RequestTimestamp { get; set; }

        /// The response status code.
        public int? ResponseStatusCode { get; set; }

        /// The response timestamp.
        public DateTime ResponseTimestamp { get; set; }

        /// The response exception payload.
        public ExceptionPayload ResponseExceptionPayLoad { get; set; }
    }
}