using SBCode_WebPhone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBCode_WebPhone.ViewModels
{
    public class GioHangVM
    {
        public Guid MaHangHoa { get; set; }
        public string TenHh { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }

      

        public byte GiamGia { get; set; }
        public double GiaBan => DonGia * (100 - GiamGia) / 100;
        public string MoTa { get; set; }
        public double ThanhTien => GiaBan * SoLuong;
    }
}
