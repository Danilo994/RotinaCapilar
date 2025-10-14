using System;
using System.Collections.Generic;

namespace Models.CuidadosCapilar.Model;

public partial class Avaliacao
{
    public int IdAvaliacao { get; set; }

    public int IdCuidado { get; set; }

    public int? Nota { get; set; }

    public string? Observacao { get; set; }

    public DateTime DataAvaliacao { get; set; }

    public virtual Cuidado? IdCuidadoNavigation { get; set; }
}
