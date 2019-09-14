using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIOrientacao.Data.Migrations
{
    public partial class MapeamentoProgrammerDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curso",
                schema: "dbo",
                columns: table => new
                {
                    IdCurso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdCurso", x => x.IdCurso);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                schema: "dbo",
                columns: table => new
                {
                    IdPessoa = table.Column<int>(nullable: false),
                    Matricula = table.Column<string>(maxLength: 8, nullable: false),
                    RegistroAtivo = table.Column<bool>(nullable: false),
                    IdCurso = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdPessoaAluno", x => x.IdPessoa);
                    table.ForeignKey(
                        name: "FK_CursoAluno",
                        column: x => x.IdCurso,
                        principalSchema: "dbo",
                        principalTable: "Curso",
                        principalColumn: "IdCurso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "PFK_PessoaAluno",
                        column: x => x.IdPessoa,
                        principalSchema: "dbo",
                        principalTable: "Pessoa",
                        principalColumn: "IdPessoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_IdCurso",
                schema: "dbo",
                table: "Aluno",
                column: "IdCurso");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aluno",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Curso",
                schema: "dbo");
        }
    }
}
