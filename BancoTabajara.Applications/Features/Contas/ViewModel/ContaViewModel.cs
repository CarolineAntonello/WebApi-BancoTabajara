using BancoTabajara.Applications.Features.Clientes.ViewModel;
using BancoTabajara.Applications.Features.Movimentacoes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Applications.Features.Contas.ViewModel
{
    public class ContaViewModel
    {
        public int Id { get; set; }
        public int NumeroConta { get; set; }
        public double Saldo { get; set; }
        public bool Ativada { get; set; }
        public double Limite { get; set; }
        public double SaldoTotal { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public List<MovimentacaoViewModel> Movimentacoes { get; set; }
    }
}
