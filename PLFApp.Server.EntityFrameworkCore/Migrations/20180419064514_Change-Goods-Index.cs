using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PLFApp.Server.EntityFrameworkCore.Migrations
{
    public partial class ChangeGoodsIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_t_Goods_GoodsName_GoodsState",
                table: "t_Goods");

            migrationBuilder.CreateIndex(
                name: "IX_t_Goods_GoodsName",
                table: "t_Goods",
                column: "GoodsName");

            migrationBuilder.CreateIndex(
                name: "IX_t_Goods_GoodsState",
                table: "t_Goods",
                column: "GoodsState");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_t_Goods_GoodsName",
                table: "t_Goods");

            migrationBuilder.DropIndex(
                name: "IX_t_Goods_GoodsState",
                table: "t_Goods");

            migrationBuilder.CreateIndex(
                name: "IX_t_Goods_GoodsName_GoodsState",
                table: "t_Goods",
                columns: new[] { "GoodsName", "GoodsState" });
        }
    }
}
