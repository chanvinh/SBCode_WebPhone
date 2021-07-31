
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using SBCode_WebPhone.Data;
using SBCode_WebPhone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SBCode_WebPhone.Controllers
{
    public class HangHoaController : Controller
    {

        private readonly MyDBContext _dbcontext;

        public HangHoaController(MyDBContext ctx)
        {
            _dbcontext = ctx;

        }
        public IActionResult Index(int? mucloai, int? page, int? sapxep, string timkiem)
        {//Phan trang va lấy loại
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNum = (page ?? 1);
            IList<Loai> dsLoai = _dbcontext.Loais.ToList();
            ViewData["dsLoai"] = dsLoai;
            ViewBag.mucloaihientai = mucloai;
            ViewBag.sapxep = sapxep;
            //SapXep
            List<HangHoaVM> dsHangHoa;
            List<HangHoa> data;
            //Tìm Kiếm
            if (timkiem != null && timkiem != "")
                data = _dbcontext.HangHoas.Where(p => p.TenHh.ToLower().Contains(timkiem.ToLower()) || p.Loai.TenLoai.ToLower().Contains(timkiem.ToLower()) || p.XuatXu.ToLower().Contains(timkiem.ToLower())).ToList();
            else
                data = _dbcontext.HangHoas.ToList();

            //Mục Loại
            if (mucloai.HasValue)
            {
                data = data.Where(l => l.MaLoai == mucloai || l.Loai.MaLoaiCha == mucloai).ToList();
            }
            dsHangHoa = data.Select(hh => new HangHoaVM
            {
                MaHh = hh.MaHangHoa,
                TenHh = hh.TenHh,
                DonGia = hh.DonGia,
                GiamGia = hh.GiamGia,
                Hinh = hh.Hinh,
                ThuongHieu = hh.ThuongHieu,
                XuatXu = hh.XuatXu,
                SoLuongBan = hh.SoluongBan,
                MoTa = hh.MoTa,
                SoLuong = hh.SoLuong,
                TenLoai = hh.Loai.TenLoai,
            }).ToList();


            dsHangHoa = SapXep(dsHangHoa,sapxep);

            return View(dsHangHoa.ToPagedList(pageNum, pageSize));
        }

        public List<HangHoaVM> SapXep(List<HangHoaVM> dsHangHoa, int? sapxep)
        {
            //SapXep
            if (sapxep == -1)
                return dsHangHoa.OrderByDescending(ds => ds.GiaBan).ToList();
            else if (sapxep == 0)
                return dsHangHoa.OrderBy(ds => ds.GiaBan).ToList();
            else if (sapxep == 1 || sapxep == 3)
                return dsHangHoa.OrderByDescending(ds => ds.SoLuongBan).ToList();
            else
                return dsHangHoa.ToList();
        }
        public IActionResult Detail(Guid id)
        {
            ViewBag.NoiGui = "Không Rõ";
            ViewBag.Tenadmin = "";
            var Admin = _dbcontext.UserRoles.FirstOrDefault(kh => kh.RoleId == 11).UserId;
            if (Admin != null)
            {
                ViewBag.NoiGui = _dbcontext.KhachHangs.FirstOrDefault(kh => kh.MaKh == Admin).DiaChi;
                ViewBag.Tenadmin = _dbcontext.KhachHangs.FirstOrDefault(kh => kh.MaKh == Admin).HoTen;
            }
            ViewBag.getdiachi = "Không Rõ";
            
            if((this.User.FindFirstValue(ClaimTypes.Email)) != null) { ViewBag.getdiachi = _dbcontext.KhachHangs.FirstOrDefault(kh => kh.Email == (this.User.FindFirstValue(ClaimTypes.Email))).DiaChi; }
            var HangHoa = _dbcontext.HangHoas
                .Include(hh=>hh.Loai)
                .Include(hh =>hh.Loai.LoaiCha)
                .FirstOrDefault(hh => hh.MaHangHoa == id);
            List<HangHoa> _4sanPhamCungLoai = _dbcontext.HangHoas.Where(a => a.MaHangHoa != id && a.MaLoai == HangHoa.MaLoai).Take(4).ToList();
            ViewData["spcungloai"] = _4sanPhamCungLoai;
            return View(HangHoa);
        }
      

    }
}
