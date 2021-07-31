using Microsoft.EntityFrameworkCore.Migrations;

namespace SBCode_WebPhone.Migrations
{
    public partial class AddColumnNewToHangHoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoluongBan",
                table: "HangHoa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ThuongHieu",
                table: "HangHoa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XuatXu",
                table: "HangHoa",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoluongBan",
                table: "HangHoa");

            migrationBuilder.DropColumn(
                name: "ThuongHieu",
                table: "HangHoa");

            migrationBuilder.DropColumn(
                name: "XuatXu",
                table: "HangHoa");
        }
    }
}
