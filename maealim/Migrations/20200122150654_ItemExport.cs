﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace maealim.Migrations
{
    public partial class ItemExport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemExports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemOfProductId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    Recipient = table.Column<string>(nullable: false),
                    Notce = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemExports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemExports_ItemOfProducts_ItemOfProductId",
                        column: x => x.ItemOfProductId,
                        principalTable: "ItemOfProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemExports_ItemOfProductId",
                table: "ItemExports",
                column: "ItemOfProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemExports");
        }
    }
}
