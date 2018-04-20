using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PLFApp.Server.EntityFrameworkCore.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_Goods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    GoodsName = table.Column<string>(maxLength: 30, nullable: false),
                    GoodsPictureUrl = table.Column<string>(maxLength: 255, nullable: false),
                    GoodsPrice = table.Column<decimal>(nullable: false),
                    GoodsState = table.Column<int>(nullable: false),
                    Inventory = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Goods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_Member",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    HeadImageUrl = table.Column<string>(maxLength: 255, nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    LoginIp = table.Column<string>(maxLength: 23, nullable: true),
                    MobilePhone = table.Column<string>(maxLength: 16, nullable: false),
                    NickName = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Member", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_Goods_GoodsName_GoodsState",
                table: "t_Goods",
                columns: new[] { "GoodsName", "GoodsState" });

            migrationBuilder.CreateIndex(
                name: "IX_t_Member_MobilePhone",
                table: "t_Member",
                column: "MobilePhone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_Goods");

            migrationBuilder.DropTable(
                name: "t_Member");
        }
    }
}
