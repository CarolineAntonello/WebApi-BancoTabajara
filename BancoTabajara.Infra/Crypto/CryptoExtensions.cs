using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BancoTabajara.Infra.Crypto
{
    /// Classe para manipular a cripografia no servidor
    [ExcludeFromCodeCoverage]
    public static class CryptoExtensions
    {
        /// <summary>
        /// Método para gerar uma hash com base no algoritmo HMACSHA1
        /// </summary>
        /// <param name="passwordToHash">É a string atual (método de extensão) que será a base para a geração da hash</param>
        /// <returns>Retorna a hash do valor da string atual</returns>
        public static string GenerateHash(this string passwordToHash)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passwordToHash,
                salt: new byte[0],
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
