using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppMVC.Data.Migrations
{
    public partial class VendaAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoFornecedor",
                table: "Fornecedores");

            migrationBuilder.AddColumn<int>(
                name: "TipoPessoa",
                table: "Fornecedores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoPessoa",
                table: "Clientes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    QuantidadeVenda = table.Column<int>(nullable: false),
                    TotalVenda = table.Column<decimal>(nullable: false),
                    DataVenda = table.Column<DateTime>(nullable: false),
                    TipoVenda = table.Column<int>(nullable: false),
                    StatusVenda = table.Column<int>(nullable: false),
                    Observacoes = table.Column<string>(type: "varchar(100)", nullable: true),
                    ClienteId = table.Column<Guid>(nullable: false),
                    ProdutoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendas_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ClienteId",
                table: "Vendas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ProdutoId",
                table: "Vendas",
                column: "ProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DropColumn(
                name: "TipoPessoa",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "TipoPessoa",
                table: "Clientes");

            migrationBuilder.AddColumn<int>(
                name: "TipoFornecedor",
                table: "Fornecedores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
