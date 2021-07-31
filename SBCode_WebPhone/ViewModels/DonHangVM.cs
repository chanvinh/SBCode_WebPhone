using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBCode_WebPhone.ViewModels
{
    public class DonHangVM
    {
        public Guid MaDh { get; set; }
        public DateTime NgayDat { get; set; }
        public int? MaKh { get; set; }
        public string NguoiNhan { get; set; }
        public string DiaChiGiao { get; set; }
        public double TongTien { get; set; }

    }
}
