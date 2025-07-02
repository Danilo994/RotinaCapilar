using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Models.CuidadosCapilar.Model;

public partial class DBRotinaCapilarContext : DbContext
{
    public DBRotinaCapilarContext()
    {
    }

    public DBRotinaCapilarContext(DbContextOptions<DBRotinaCapilarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Avaliacao> Avaliacaos { get; set; }

    public virtual DbSet<Cuidado> Cuidados { get; set; }

    public virtual DbSet<CuidadoProduto> CuidadoProdutos { get; set; }

    public virtual DbSet<Foto> Fotos { get; set; }

    public virtual DbSet<Lavagem> Lavagems { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.HasKey(e => e.IdAvaliacao).HasName("PK__Avaliaca__78C432D8278F5D6E");

            entity.ToTable("Avaliacao");

            entity.Property(e => e.DataAvaliacao).HasColumnType("datetime");
            entity.Property(e => e.Observacao).HasMaxLength(500);

            entity.HasOne(d => d.IdCuidadoNavigation).WithMany(p => p.Avaliacaos)
                .HasForeignKey(d => d.IdCuidado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Avaliacao_Cuidado");
        });

        modelBuilder.Entity<Cuidado>(entity =>
        {
            entity.HasKey(e => e.IdCuidado).HasName("PK__Cuidado__BD8A4AE3C310B939");

            entity.ToTable("Cuidado");

            entity.Property(e => e.DataCriacao).HasColumnType("datetime");
            entity.Property(e => e.DataCuidado).HasColumnType("datetime");
            entity.Property(e => e.DataModificacao).HasColumnType("datetime");

            entity.HasOne(d => d.IdLavagemNavigation).WithMany(p => p.Cuidados)
                .HasForeignKey(d => d.IdLavagem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cuidado_Lavagem");
        });

        modelBuilder.Entity<CuidadoProduto>(entity =>
        {
            entity.HasKey(e => e.IdCuidadoProdutos).HasName("PK__CuidadoP__B4C691942E4613E4");

            entity.HasOne(d => d.IdCuidadoNavigation).WithMany(p => p.CuidadoProdutos)
                .HasForeignKey(d => d.IdCuidado)
                .HasConstraintName("FK_CuidadoProdutos_Cuidado");

            entity.HasOne(d => d.IdProdutoNavigation).WithMany(p => p.CuidadoProdutos)
                .HasForeignKey(d => d.IdProduto)
                .HasConstraintName("FK_CuidadoProdutos_Produto");
        });

        modelBuilder.Entity<Foto>(entity =>
        {
            entity.HasKey(e => e.IdFoto).HasName("PK__Foto__007D321DD4937FF8");

            entity.ToTable("Foto");

            entity.Property(e => e.DataUpload).HasColumnType("datetime");
            entity.Property(e => e.Descricao).HasMaxLength(200);
            entity.Property(e => e.UrlImagem).HasMaxLength(300);

            entity.HasOne(d => d.IdCuidadoNavigation).WithMany(p => p.Fotos)
                .HasForeignKey(d => d.IdCuidado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Foto_Cuidado");
        });

        modelBuilder.Entity<Lavagem>(entity =>
        {
            entity.HasKey(e => e.IdLavagem).HasName("PK__Lavagem__59C12C759F2E0C01");

            entity.ToTable("Lavagem");

            entity.Property(e => e.NomeLavagem).HasMaxLength(255);
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.IdProduto).HasName("PK__Produtos__2E883C2339BE998F");

            entity.Property(e => e.NomeProduto).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
