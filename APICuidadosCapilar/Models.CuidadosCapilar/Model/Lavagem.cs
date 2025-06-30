using System;
using System.Collections.Generic;

namespace Models.CuidadosCapilar.Model;

public partial class Lavagem
{
    public int IdLavagem { get; set; }

    public string NomeLavagem { get; set; } = null!;

    public virtual ICollection<Cuidado> Cuidados { get; set; } = new List<Cuidado>();
}
