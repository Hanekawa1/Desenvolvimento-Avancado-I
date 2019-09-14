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

        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForSqlServerUseIdentityColumns();
            modelBuilder.HasDefaultSchema("dbo");


            #region Aluno
            modelBuilder.Entity<Aluno>(e => 
            {
                e.ToTable("Aluno");
                e.HasKey(c => c.IdPessoa).HasName("IdPessoaAluno");

                e.Property(c => c.IdPessoa)
                    .HasColumnName("IdPessoa")
                    .IsRequired();

                e.Property(c => c.IdCurso)
                    .HasColumnName("IdCurso")
                    .IsRequired();

                e.Property(c => c.Matricula)
                    .HasColumnName("Matricula")
                    .IsRequired()
                    .HasMaxLength(8);

                e.Property(c => c.RegistroAtivo)
                    .HasColumnName("RegistroAtivo")
                    .IsRequired();

                e.HasOne(d => d.Pessoa)
                    .WithOne(p => p.Aluno)
                    .HasForeignKey<Aluno>(d => d.IdPessoa)
                    .HasConstraintName("PFK_PessoaAluno");

                e.HasOne(d => d.Curso)
                    .WithMany(p => p.Alunos)
                    .HasForeignKey(d => d.IdCurso)
                    .HasConstraintName("FK_CursoAluno");

            });
            #endregion

            #region Curso
            modelBuilder.Entity<Curso>(e =>
            {
                e.ToTable("Curso");
                e.HasKey(c => c.IdCurso).HasName("IdCurso");

                e.Property(c => c.IdCurso)
                    .HasColumnName("IdCurso")
                    .ValueGeneratedOnAdd();

                e.Property(c => c.Nome)
                    .IsRequired()
                    .HasMaxLength(150);
            });
            #endregion

            #region Pessoa
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
            #endregion
        }
    }
}
