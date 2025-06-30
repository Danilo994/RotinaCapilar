using System;
using System.Collections.Generic;

namespace Models.CuidadosCapilar.Model;

public partial class Cuidado
{
    public int IdCuidado { get; set; }

    public int IdLavagem { get; set; }

    public DateTime DataCuidado { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataModificacao { get; set; }

    public virtual ICollection<Avaliacao> Avaliacaos { get; set; } = new List<Avaliacao>();

    public virtual ICollection<CuidadoProduto> CuidadoProdutos { get; set; } = new List<CuidadoProduto>();

    public virtual ICollection<Foto> Fotos { get; set; } = new List<Foto>();

    public virtual Lavagem IdLavagemNavigation { get; set; } = null!;
}
