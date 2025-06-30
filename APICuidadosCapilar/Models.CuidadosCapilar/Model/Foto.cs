using System;
using System.Collections.Generic;

namespace Models.CuidadosCapilar.Model;

public partial class Foto
{
    public int IdFoto { get; set; }

    public int IdCuidado { get; set; }

    public string UrlImagem { get; set; } = null!;

    public string? Descricao { get; set; }

    public DateTime DataUpload { get; set; }

    public virtual Cuidado IdCuidadoNavigation { get; set; } = null!;
}
