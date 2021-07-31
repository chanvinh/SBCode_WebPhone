using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBCode_WebPhone.ViewModels
{
    public class ChiTietDonHangVM
    {
        public Guid MaDh { get; set; }
        public Guid MaHh { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public string TenHh { get; set; }
        public string Hinh { get; set; }

    }
}
