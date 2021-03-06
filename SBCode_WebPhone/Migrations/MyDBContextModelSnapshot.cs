// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SBCode_WebPhone.Data;

namespace SBCode_WebPhone.Migrations
{
    [DbContext(typeof(MyDBContext))]
    partial class MyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SBCode_WebPhone.Data.DonHang", b =>
                {
                    b.Property<Guid>("MaDh")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiaChiGiao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaKh")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayDat")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiNhan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TinhTrangDonHang")
                        .HasColumnType("int");

                    b.Property<double>("TongTien")
                        .HasColumnType("float");

                    b.HasKey("MaDh");

                    b.HasIndex("MaKh");

                    b.ToTable("DonHang");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.DonHangChiTiet", b =>
                {
                    b.Property<Guid>("MaDh")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaHh")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("DonGia")
                        .HasColumnType("float");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("MaDh", "MaHh");

                    b.HasIndex("MaHh");

                    b.ToTable("DonHangChiTiet");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.HangHoa", b =>
                {
                    b.Property<Guid>("MaHangHoa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<string>("ChiTiet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("DiemReview")
                        .HasColumnType("float");

                    b.Property<double>("DonGia")
                        .HasColumnType("float");

                    b.Property<byte>("GiamGia")
                        .HasColumnType("tinyint");

                    b.Property<string>("Hinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaLoai")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<int>("SoluongBan")
                        .HasColumnType("int");

                    b.Property<string>("TenHh")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ThuongHieu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("XuatXu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaHangHoa");

                    b.HasIndex("MaLoai");

                    b.HasIndex("TenHh")
                        .IsUnique();

                    b.ToTable("HangHoa");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.HangHoaTag", b =>
                {
                    b.Property<string>("TagKey")
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("MaHangHoa")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TagKey", "MaHangHoa");

                    b.HasIndex("MaHangHoa");

                    b.ToTable("HangHoaTag");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.HinhPhu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MaHangHoa")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MaHangHoa");

                    b.ToTable("HinhPhus");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.KhachHang", b =>
                {
                    b.Property<int>("MaKh")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("DangHoatDong")
                        .HasColumnType("bit");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaNgauNhien")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MatKhau")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("MaKh");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("KhachHang");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.Loai", b =>
                {
                    b.Property<int>("MaLoai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MaLoaiCha")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenLoai")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaLoai");

                    b.HasIndex("MaLoaiCha");

                    b.ToTable("Loai");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Criteria")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.ReviewHangHoa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("DiemReview")
                        .HasColumnType("tinyint");

                    b.Property<Guid>("MaHangHoa")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("NgayReview")
                        .HasColumnType("datetime2");

                    b.Property<int>("TieuChi")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaHangHoa");

                    b.HasIndex("TieuChi");

                    b.ToTable("ReviewHangHoas");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsSystem")
                        .HasColumnType("bit");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.Tag", b =>
                {
                    b.Property<string>("TagKey")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TagValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagKey");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.DonHang", b =>
                {
                    b.HasOne("SBCode_WebPhone.Data.KhachHang", "KhachHang")
                        .WithMany()
                        .HasForeignKey("MaKh");

                    b.Navigation("KhachHang");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.DonHangChiTiet", b =>
                {
                    b.HasOne("SBCode_WebPhone.Data.DonHang", "DonHang")
                        .WithMany()
                        .HasForeignKey("MaDh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SBCode_WebPhone.Data.HangHoa", "HangHoa")
                        .WithMany()
                        .HasForeignKey("MaHh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonHang");

                    b.Navigation("HangHoa");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.HangHoa", b =>
                {
                    b.HasOne("SBCode_WebPhone.Data.Loai", "Loai")
                        .WithMany("HangHoas")
                        .HasForeignKey("MaLoai")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Loai");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.HangHoaTag", b =>
                {
                    b.HasOne("SBCode_WebPhone.Data.HangHoa", "HangHoa")
                        .WithMany("HangHoaTags")
                        .HasForeignKey("MaHangHoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SBCode_WebPhone.Data.Tag", "Tag")
                        .WithMany("HangHoaTags")
                        .HasForeignKey("TagKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HangHoa");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.HinhPhu", b =>
                {
                    b.HasOne("SBCode_WebPhone.Data.HangHoa", "HangHoa")
                        .WithMany("HinhPhus")
                        .HasForeignKey("MaHangHoa");

                    b.Navigation("HangHoa");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.Loai", b =>
                {
                    b.HasOne("SBCode_WebPhone.Data.Loai", "LoaiCha")
                        .WithMany()
                        .HasForeignKey("MaLoaiCha");

                    b.Navigation("LoaiCha");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.ReviewHangHoa", b =>
                {
                    b.HasOne("SBCode_WebPhone.Data.HangHoa", "HangHoa")
                        .WithMany("ReviewHangHoas")
                        .HasForeignKey("MaHangHoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SBCode_WebPhone.Data.Review", "Review")
                        .WithMany("ReviewHangHoas")
                        .HasForeignKey("TieuChi")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HangHoa");

                    b.Navigation("Review");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.UserRole", b =>
                {
                    b.HasOne("SBCode_WebPhone.Data.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SBCode_WebPhone.Data.KhachHang", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.HangHoa", b =>
                {
                    b.Navigation("HangHoaTags");

                    b.Navigation("HinhPhus");

                    b.Navigation("ReviewHangHoas");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.KhachHang", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.Loai", b =>
                {
                    b.Navigation("HangHoas");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.Review", b =>
                {
                    b.Navigation("ReviewHangHoas");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("SBCode_WebPhone.Data.Tag", b =>
                {
                    b.Navigation("HangHoaTags");
                });
#pragma warning restore 612, 618
        }
    }
}
