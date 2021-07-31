using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SBCode_WebPhone.Data;
using SBCode_WebPhone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBCode_WebPhone.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhanQuyenController : Controller
    {
        private readonly MyDBContext _dbcontext;

        public PhanQuyenController(MyDBContext db)
        {
            _dbcontext = db;
        }
        public IActionResult Index()
        {
            if (User.IsInRole("Quản trị Hệ thống"))
            {

            }
            return View();
        }

        public IActionResult PhanQuyen()
        {

            var data = _dbcontext.KhachHangs.Include(kh => kh.UserRoles)
                .Select(kh => new PhanQuyenVM
                {
                    MaKh = kh.MaKh,
                    HoTen = kh.HoTen,
                    QuanTri = KiemtraPQ(kh, 11),
                    BanHang = KiemtraPQ(kh, 12),
                    ThuKho = KiemtraPQ(kh, 13),
                    KhachHang = KiemtraPQ(kh, 14),
                });
            return View(data);
        }



        private static bool KiemtraPQ(object phanQuyen, int a)
        {
            var check = ((KhachHang)phanQuyen).UserRoles.FirstOrDefault(x => x.RoleId == a);
            if (check != null)
                return true;
            else
                return false;
        }


        [HttpPost]
        public IActionResult UpdatePhanQuyen(List<int> MaKh, List<bool> QuanTri, List<bool> ThuKho, List<bool> BanHang, List<bool> KhachHang)
        {
            for (int i = 0; i < MaKh.Count; i++)
            {
                Save(MaKh[i],QuanTri[i],11);
                Save(MaKh[i], BanHang[i], 12);
                Save(MaKh[i], ThuKho[i], 13);
                Save(MaKh[i], KhachHang[i], 14);
            }
            return RedirectToAction("PhanQuyen");
        }

        public void Save(int MaKh, bool check, int chucvu)
        {
            UserRole phanquyen = _dbcontext.UserRoles.FirstOrDefault(p => p.UserId == MaKh && p.RoleId == chucvu);
             if(check == true && phanquyen == null)
            {
                UserRole phanquyen1 = new UserRole()
                {
                    UserId = MaKh,
                    RoleId = chucvu
                };
                _dbcontext.Add(phanquyen1);
                _dbcontext.SaveChanges();
            }
            else if (check == false && phanquyen != null)
            {
                phanquyen.UserId = MaKh;
                phanquyen.RoleId = chucvu;
                _dbcontext.Remove(phanquyen);
                _dbcontext.SaveChanges();
            }
        }
    }
}
