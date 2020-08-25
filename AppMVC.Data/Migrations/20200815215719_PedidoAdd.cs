using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppMVC.Data.Migrations
{
    public partial class PedidoAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "QuantidadeVenda",
                table: "Vendas");

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
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ValorTotalProduto = table.Column<decimal>(nullable: false),
                    QuantidadeTotalProduto = table.Column<int>(nullable: false),
                    ProdutoId = table.Column<Guid>(nullable: false),
                    VendaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Vendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ProdutoId",
                table: "Pedidos",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_VendaId",
                table: "Pedidos",
                column: "VendaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedidos");

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

            migrationBuilder.AddColumn<Guid>(
                name: "ProdutoId",
                table: "Vendas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeVenda",
                table: "Vendas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
