using Microsoft.EntityFrameworkCore.Migrations;

namespace AppMVC.Data.Migrations
{
    public partial class VendasAdd2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Cep",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Logradouro",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Vendas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Vendas",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "Vendas",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Vendas",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "Vendas",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Vendas",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logradouro",
                table: "Vendas",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Vendas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
