using BancoTabajara.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Domain.Features.Clientes
{
    public class Cliente : Entidade
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime DataNascimento { get; set; }

        public override void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                throw new ClienteNomeVazioException();
            if (string.IsNullOrEmpty(CPF))
                throw new ClienteCPFInvalidoException();
            if (string.IsNullOrEmpty(RG))
                throw new ClienteRGInvalidoException();
            if (DataNascimento >= DateTime.Now)
                throw new ClienteDataNascimentoInvalidoException();
        }
    }
}
