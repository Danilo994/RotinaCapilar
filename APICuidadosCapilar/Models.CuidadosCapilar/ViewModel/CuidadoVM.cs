using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CuidadosCapilar.ViewModel
{
    public class CuidadoVM
    {
        public int idCuidado {  get; set; }
        public DateTime DataCuidado { get; set; }
        public string Lavagem { get; set; } = string.Empty;
        public List<string> Produtos { get; set; } = new List<string>();
        public int? idAvaliacao { get; set; }
        public int? Nota {  get; set; }
        public string? Observacao { get; set; }
        public DateTime? DataAvaliacao { get; set; }
    }
}
