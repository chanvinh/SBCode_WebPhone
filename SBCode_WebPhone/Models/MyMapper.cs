using AutoMapper;
using SBCode_WebPhone.Data;
using SBCode_WebPhone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBCode_WebPhone.Models
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<HangHoa, GioHangVM>();
            CreateMap<DangKyVM, KhachHang>();
        }
    }
}
