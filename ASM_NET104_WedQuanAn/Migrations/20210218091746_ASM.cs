using Microsoft.EntityFrameworkCore.Migrations;

namespace ASM_NET104_WedQuanAn.Migrations
{
    public partial class ASM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_ThucDon_Nhom",
                table: "ThucDon",
                column: "Nhom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThucDon");

            migrationBuilder.DropTable(
                name: "Nhom");
        }
    }
}
