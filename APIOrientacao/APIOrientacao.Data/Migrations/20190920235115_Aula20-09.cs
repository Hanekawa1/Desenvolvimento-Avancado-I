using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIOrientacao.Data.Migrations
{
    public partial class Aula2009 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projeto",
                schema: "dbo",
                columns: table => new
                {
                    IdProjeto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdPessoa = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Encerrado = table.Column<bool>(nullable: false),
                    Nota = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdProjeto", x => x.IdProjeto);
                    table.ForeignKey(
                        name: "FK_AlunoProjeto",
                        column: x => x.IdPessoa,
                        principalSchema: "dbo",
                        principalTable: "Aluno",
                        principalColumn: "IdPessoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Situacao",
                schema: "dbo",
                columns: table => new
                {
                    IdSituacao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdSituacao", x => x.IdSituacao);
                });

            migrationBuilder.CreateTable(
                name: "SituacaoProjeto",
                schema: "dbo",
                columns: table => new
                {
                    IdSituacao = table.Column<int>(nullable: false),
                    IdProjeto = table.Column<int>(nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SituacaoProjeto", x => new { x.IdProjeto, x.IdSituacao });
                    table.ForeignKey(
                        name: "FK_ProjetoSituacaoProjeto",
                        column: x => x.IdProjeto,
                        principalSchema: "dbo",
                        principalTable: "Projeto",
                        principalColumn: "IdProjeto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SituacaoSituacaoProjeto",
                        column: x => x.IdSituacao,
                        principalSchema: "dbo",
                        principalTable: "Situacao",
                        principalColumn: "IdSituacao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_IdPessoa",
                schema: "dbo",
                table: "Projeto",
                column: "IdPessoa");

            migrationBuilder.CreateIndex(
                name: "IX_SituacaoProjeto_IdSituacao",
                schema: "dbo",
                table: "SituacaoProjeto",
                column: "IdSituacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SituacaoProjeto",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Projeto",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Situacao",
                schema: "dbo");
        }
    }
}
