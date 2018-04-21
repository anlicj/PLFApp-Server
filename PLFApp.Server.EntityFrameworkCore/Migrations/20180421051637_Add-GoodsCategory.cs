using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PLFApp.Server.EntityFrameworkCore.Migrations
{
    public partial class AddGoodsCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoodsCategoryId",
                table: "t_Goods",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "t_GoodsCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryImageSrc = table.Column<string>(maxLength: 255, nullable: false),
                    GoodsCategoryName = table.Column<string>(maxLength: 10, nullable: false),
                    IsShowOnHomePage = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_GoodsCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_Goods_GoodsCategoryId",
                table: "t_Goods",
                column: "GoodsCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_t_GoodsCategory_IsShowOnHomePage",
                table: "t_GoodsCategory",
                column: "IsShowOnHomePage");

            migrationBuilder.AddForeignKey(
                name: "FK_t_Goods_t_GoodsCategory_GoodsCategoryId",
                table: "t_Goods",
                column: "GoodsCategoryId",
                principalTable: "t_GoodsCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_Goods_t_GoodsCategory_GoodsCategoryId",
                table: "t_Goods");

            migrationBuilder.DropTable(
                name: "t_GoodsCategory");

            migrationBuilder.DropIndex(
                name: "IX_t_Goods_GoodsCategoryId",
                table: "t_Goods");

            migrationBuilder.DropColumn(
                name: "GoodsCategoryId",
                table: "t_Goods");
        }
    }
}
