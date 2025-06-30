using System;
using System.Collections.Generic;

namespace Models.CuidadosCapilar.Model;

public partial class CuidadoProduto
{
    public int IdCuidadoProdutos { get; set; }

    public int? IdCuidado { get; set; }

    public int? IdProduto { get; set; }

    public virtual Cuidado? IdCuidadoNavigation { get; set; }

    public virtual Produto? IdProdutoNavigation { get; set; }
}
