using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASM_NET104_WedQuanAn.Migrations
{
    public partial class ASMFINAL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    SDTKH = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    DiaChiKH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TENKH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgayHD = table.Column<DateTime>(type: "Date", nullable: false),
                    EmailKH = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Nhom",
                columns: table => new
                {
                    MaNhom = table.Column<int>(type: "int", nullable: false),
                    TenNhom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nhom", x => x.MaNhom);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserEmail = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Pass = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserEmail);
                });

            migrationBuilder.CreateTable(
                name: "ThucDon",
                columns: table => new
                {
                    MaTD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hinh = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Nhom = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThucDon", x => x.MaTD);
                    table.ForeignKey(
                        name: "FK_ThucDon_nhom",
                        column: x => x.Nhom,
                        principalTable: "Nhom",
                        principalColumn: "MaNhom",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    MaTD = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => new { x.ID, x.MaTD });
                    table.ForeignKey(
                        name: "fk_ct_hang",
                        column: x => x.ID,
                        principalTable: "Cart",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ct_td",
                        column: x => x.MaTD,
                        principalTable: "ThucDon",
                        principalColumn: "MaTD",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_MaTD",
                table: "CartItem",
                column: "MaTD");

            migrationBuilder.CreateIndex(
                name: "IX_ThucDon_Nhom",
                table: "ThucDon",
                column: "Nhom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "ThucDon");

            migrationBuilder.DropTable(
                name: "Nhom");
        }
    }
}
