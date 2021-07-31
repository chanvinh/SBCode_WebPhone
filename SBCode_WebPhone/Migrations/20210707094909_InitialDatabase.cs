using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SBCode_WebPhone.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DangHoatDong = table.Column<bool>(type: "bit", nullable: false),
                    MaNgauNhien = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.MaKh);
                });

            migrationBuilder.CreateTable(
                name: "Loai",
                columns: table => new
                {
                    MaLoai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaLoaiCha = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loai", x => x.MaLoai);
                    table.ForeignKey(
                        name: "FK_Loai_Loai_MaLoaiCha",
                        column: x => x.MaLoaiCha,
                        principalTable: "Loai",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Criteria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsSystem = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TagValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagKey);
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    MaDh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaKh = table.Column<int>(type: "int", nullable: true),
                    NguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiGiao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongTien = table.Column<double>(type: "float", nullable: false),
                    TinhTrangDonHang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.MaDh);
                    table.ForeignKey(
                        name: "FK_DonHang_KhachHang_MaKh",
                        column: x => x.MaKh,
                        principalTable: "KhachHang",
                        principalColumn: "MaKh",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HangHoa",
                columns: table => new
                {
                    MaHangHoa = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    TenHh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DonGia = table.Column<double>(type: "float", nullable: false),
                    GiamGia = table.Column<byte>(type: "tinyint", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    Hinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MaLoai = table.Column<int>(type: "int", nullable: true),
                    DiemReview = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoa", x => x.MaHangHoa);
                    table.ForeignKey(
                        name: "FK_HangHoa_Loai_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "Loai",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_KhachHang_UserId",
                        column: x => x.UserId,
                        principalTable: "KhachHang",
                        principalColumn: "MaKh",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonHangChiTiet",
                columns: table => new
                {
                    MaDh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaHh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangChiTiet", x => new { x.MaDh, x.MaHh });
                    table.ForeignKey(
                        name: "FK_DonHangChiTiet_DonHang_MaDh",
                        column: x => x.MaDh,
                        principalTable: "DonHang",
                        principalColumn: "MaDh",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonHangChiTiet_HangHoa_MaHh",
                        column: x => x.MaHh,
                        principalTable: "HangHoa",
                        principalColumn: "MaHangHoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HangHoaTag",
                columns: table => new
                {
                    TagKey = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    MaHangHoa = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoaTag", x => new { x.TagKey, x.MaHangHoa });
                    table.ForeignKey(
                        name: "FK_HangHoaTag_HangHoa_MaHangHoa",
                        column: x => x.MaHangHoa,
                        principalTable: "HangHoa",
                        principalColumn: "MaHangHoa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HangHoaTag_Tag_TagKey",
                        column: x => x.TagKey,
                        principalTable: "Tag",
                        principalColumn: "TagKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HinhPhus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    MaHangHoa = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhPhus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HinhPhus_HangHoa_MaHangHoa",
                        column: x => x.MaHangHoa,
                        principalTable: "HangHoa",
                        principalColumn: "MaHangHoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReviewHangHoas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayReview = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiemReview = table.Column<byte>(type: "tinyint", nullable: false),
                    TieuChi = table.Column<int>(type: "int", nullable: false),
                    MaHangHoa = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewHangHoas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewHangHoas_HangHoa_MaHangHoa",
                        column: x => x.MaHangHoa,
                        principalTable: "HangHoa",
                        principalColumn: "MaHangHoa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewHangHoas_Reviews_TieuChi",
                        column: x => x.TieuChi,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaKh",
                table: "DonHang",
                column: "MaKh");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangChiTiet_MaHh",
                table: "DonHangChiTiet",
                column: "MaHh");

            migrationBuilder.CreateIndex(
                name: "IX_HangHoa_MaLoai",
                table: "HangHoa",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_HangHoa_TenHh",
                table: "HangHoa",
                column: "TenHh",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HangHoaTag_MaHangHoa",
                table: "HangHoaTag",
                column: "MaHangHoa");

            migrationBuilder.CreateIndex(
                name: "IX_HinhPhus_MaHangHoa",
                table: "HinhPhus",
                column: "MaHangHoa");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_Email",
                table: "KhachHang",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loai_MaLoaiCha",
                table: "Loai",
                column: "MaLoaiCha");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewHangHoas_MaHangHoa",
                table: "ReviewHangHoas",
                column: "MaHangHoa");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewHangHoas_TieuChi",
                table: "ReviewHangHoas",
                column: "TieuChi");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

           

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonHangChiTiet");

            migrationBuilder.DropTable(
                name: "HangHoaTag");

            migrationBuilder.DropTable(
                name: "HinhPhus");

            migrationBuilder.DropTable(
                name: "ReviewHangHoas");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "HangHoa");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "Loai");
        }
    }
}
