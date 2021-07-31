using Microsoft.EntityFrameworkCore.Migrations;

namespace SBCode_WebPhone.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "KhachHang",
                columns: new[] { "MaKh", "DangHoatDong", "DiaChi", "Email", "HoTen", "MaNgauNhien", "MatKhau", "SoDienThoai" },
                values: new object[] { 1, true, "GiaLai", "Phucmailk@gmail.com", "Phuc", "01", "123Aa@", "0868811469" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "KhachHang",
                keyColumn: "MaKh",
                keyValue: 1);
        }
    }
}
