using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIOrientacao.Data.Context
{
    public class Contexto : DbContext
    {
        public Contexto() { }

        public Contexto(DbContextOptions<Contexto> options): base(options) { }

        public DbSet<Pessoa> Pessoa { get; set; }

        public void PessoaMB(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>(e =>
            {
                e.ToTable("Pessoa");
                e.HasKey(c => c.IdPessoa).HasName("IdPessoa");

                e.Property(c => c.IdPessoa)
                    .HasColumnName("IdPessoa")
                    .ValueGeneratedOnAdd();

                e.Property(c => c.Nome)
                    .HasColumnName("Nome")
                    .IsRequired()
                    .HasMaxLength(300);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForSqlServerUseIdentityColumns();
            modelBuilder.HasDefaultSchema("dbo");

            PessoaMB(modelBuilder);
        }
    }
}
