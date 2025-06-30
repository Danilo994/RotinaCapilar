using System;
using System.Collections.Generic;

namespace Models.CuidadosCapilar.Model;

public partial class Produto
{
    public int IdProduto { get; set; }

    public string NomeProduto { get; set; } = null!;

    public virtual ICollection<CuidadoProduto> CuidadoProdutos { get; set; } = new List<CuidadoProduto>();
}
