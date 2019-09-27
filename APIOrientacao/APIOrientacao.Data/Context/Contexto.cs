using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<Situacao> Situacao { get; set; }
        public DbSet<SituacaoProjeto> SituacaoProjeto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Para cada objeto de relacionamento mapeados no contexto, a propriedade de restrição de "Cascade" é desabilitada
            foreach(var relacionamento in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relacionamento.DeleteBehavior = DeleteBehavior.Restrict;
            }

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

            #region Situacao
            modelBuilder.Entity<Situacao>(e =>
            {
                e.ToTable("Situacao");
                e.HasKey(c => c.IdSituacao).HasName("IdSituacao");

                e.Property(c => c.IdSituacao)
                    .HasColumnName("IdSituacao")
                    .ValueGeneratedOnAdd();

                e.Property(c => c.Descricao).HasColumnName("Descricao")
                    .IsRequired()
                    .HasMaxLength(100);
            });
            #endregion

            #region SituacaoProjeto
            modelBuilder.Entity<SituacaoProjeto>(e => {
                e.ToTable("SituacaoProjeto");
                e.HasKey(c => new { c.IdProjeto, c.IdSituacao});

                e.Property(c => c.IdProjeto)
                    .HasColumnName("IdProjeto")
                    .IsRequired();

                e.Property(c => c.IdSituacao)
                    .HasColumnName("IdSituacao")
                    .IsRequired();

                e.Property(c => c.DataRegistro)
                    .HasColumnName("DataRegistro")
                    .HasColumnType("datetime")
                    .IsRequired();

                e.HasOne(d => d.Situacao)
                    .WithMany(p => p.SituacoesProjeto)
                    .HasForeignKey(d => d.IdSituacao)
                    .HasConstraintName("FK_SituacaoSituacaoProjeto");

                e.HasOne(d => d.Projeto)
                    .WithMany(p => p.SituacoesProjeto)
                    .HasForeignKey(d => d.IdProjeto)
                    .HasConstraintName("FK_ProjetoSituacaoProjeto");
            });
            #endregion

            #region Projeto
            modelBuilder.Entity<Projeto>(e =>
            {
                e.ToTable("Projeto");
                e.HasKey(c => c.IdProjeto).HasName("IdProjeto");

                e.Property(c => c.IdProjeto)
                    .HasColumnName("IdProjeto")
                    .ValueGeneratedOnAdd();

                e.Property(c => c.IdPessoa)
                    .HasColumnName("IdPessoa")
                    .IsRequired();

                e.Property(c => c.Nota)
                    .HasColumnName("Nota")
                    .IsRequired();

                e.Property(c => c.Encerrado)
                    .HasColumnName("Encerrado")
                    .IsRequired();

                e.Property(c => c.Nome)
                    .HasColumnName("Nome")
                    .IsRequired()
                    .HasMaxLength(200);

                e.HasOne(d => d.Aluno)
                    .WithMany(p => p.Projetos)
                    .HasForeignKey(d => d.IdPessoa)
                    .HasConstraintName("FK_AlunoProjeto");
            });
            #endregion
        }
    }
}
