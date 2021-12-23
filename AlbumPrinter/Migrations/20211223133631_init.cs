using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlbumPrinter.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    RequiredSpace = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemDescriptions",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    ItemType = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemDescriptions", x => new { x.OrderId, x.ItemType });
                    table.ForeignKey(
                        name: "FK_OrderItemDescriptions_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItemDescriptions");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
