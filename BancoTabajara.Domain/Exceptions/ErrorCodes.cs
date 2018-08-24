using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Exceptions
{
    public enum ErrorCodes
    {
        /// <summary>
        /// Unauthorized: Não autorizado, a diferença para 403 é que o usuário não está autenticado.
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// Ação proibida. O server entendeu o pedido, mas não pode executá-lo (está autenticado mas não tem permissão)
        /// </summary>
        Forbidden = 0403,

        /// <summary>
        ///  Não encontrado
        /// </summary>
        NotFound = 0404,

        /// <summary>
        /// HttpStatus Conflict: Já existente
        /// </summary>
        AlreadyExists = 0409,

        /// <summary>
        /// equivale ao httpStatus 405 not allowed
        /// </summary>
        NotAllowed = 0405,

        /// <summary>
        /// Objeto invalido equivale ao httpStaus 422 Unprocessable Entity
        /// </summary>
        InvalidObject = 0422,

        /// <summary>
        /// Exceção não tratada.(Internal Server error)
        /// </summary>
        Unhandled = 0500,

        /// <summary>
        /// Serviço não disponível
        /// </summary>
        ServiceUnavailable = 0503,
    }
}
