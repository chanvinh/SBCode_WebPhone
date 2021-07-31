using SBCode_WebPhone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBCode_WebPhone.ViewModels
{
    public class HangHoaVM
    {
        public Guid MaHh { get; set; }
        public string TenHh { get; set; }
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }
        public double GiaBan => DonGia * (100 - GiamGia) / 100;
        public int SoLuong { get; set; }

        public string ThuongHieu { get; set; }

        public string XuatXu { get; set; }
        public int SoLuongBan { get; set; }
        public string Hinh { get; set; }
        public string MoTa { get; set; }

        
        public string TenLoai { get; set; }
    }
}
