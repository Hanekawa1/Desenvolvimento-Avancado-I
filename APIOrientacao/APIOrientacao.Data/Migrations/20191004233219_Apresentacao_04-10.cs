using Microsoft.EntityFrameworkCore.Migrations;

namespace APIOrientacao.Data.Migrations
{
    public partial class Apresentacao_0410 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdSituacaoProjeto",
                schema: "dbo",
                table: "Orientacao",
                newName: "IdTipoOrientacao");

            migrationBuilder.RenameIndex(
                name: "IX_Orientacao_IdSituacaoProjeto",
                schema: "dbo",
                table: "Orientacao",
                newName: "IX_Orientacao_IdTipoOrientacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdTipoOrientacao",
                schema: "dbo",
                table: "Orientacao",
                newName: "IdSituacaoProjeto");

            migrationBuilder.RenameIndex(
                name: "IX_Orientacao_IdTipoOrientacao",
                schema: "dbo",
                table: "Orientacao",
                newName: "IX_Orientacao_IdSituacaoProjeto");
        }
    }
}
