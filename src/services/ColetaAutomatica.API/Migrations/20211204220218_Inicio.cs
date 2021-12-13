using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColetaAutomatica.API.Migrations
{
    public partial class Inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitacaoProcesso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroProcesso = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoProcesso", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitacaoProcesso");
        }
    }
}
