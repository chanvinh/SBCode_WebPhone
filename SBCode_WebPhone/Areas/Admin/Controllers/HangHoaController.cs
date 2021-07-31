using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SBCode_WebPhone.Data;
using SBCode_WebPhone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBCode_WebPhone.Helper;
using Microsoft.AspNetCore.Authorization;

namespace SBCode_WebPhone.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Quản Trị Hệ Thống,Thủ Kho,Bán Hàng")]
    public class HangHoaController : Controller
    {
        private readonly ILogger _logger;
        private readonly MyDBContext _dbcontext;

        public HangHoaController(MyDBContext ctx, ILogger<HangHoaController> logger)
        {
            _dbcontext = ctx;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        { 
            var data = await _dbcontext.HangHoas
                .Include(hh => hh.Loai)
                .ToListAsync();
            return View(data);
            
        }

        public IActionResult Create()
        {
            ViewBag.DanhSachLoai = new LoaiDropDownVM(_dbcontext.Loais, "MaLoai", "TenLoai", "MaLoai");
            return View();
        }

        [HttpPost]
        public IActionResult Create(HangHoa hh, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var urlHinh = FileUpAnh.UploadFileToFolder(hh.Hinh,Hinh, "HangHoa");
                    hh.Hinh = urlHinh;
                    _dbcontext.Add(hh);
                    _dbcontext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Loi: {ex.Message}");

                    ViewBag.ThongBaoLoi = "Có lỗi";
                    ViewBag.DanhSachLoai = new LoaiDropDownVM(_dbcontext.Loais, "MaLoai", "TenLoai", "MaLoai");
                    return View();
                }
            }

            ViewBag.DanhSachLoai = new LoaiDropDownVM(_dbcontext.Loais, "MaLoai", "TenLoai", "MaLoai");
            return View();
        }

        public IActionResult Edit(Guid id)
        {
            var hh = _dbcontext.HangHoas.FirstOrDefault(h => h.MaHangHoa == id);

            ViewBag.DanhSachLoai = new LoaiDropDownVM(_dbcontext.Loais, "MaLoai", "TenLoai", "MaLoai", hh.MaLoai);
            return View(hh);
        }

        [HttpPost]
        public IActionResult Edit(HangHoa hangHoa, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var edithh = _dbcontext.HangHoas.FirstOrDefault(h => h.MaHangHoa == hangHoa.MaHangHoa);
                    var urlHinh = FileUpAnh.UploadFileToFolder(hangHoa.Hinh,Hinh, "HangHoa");
                    edithh.Hinh = urlHinh;
                    edithh.TenHh = hangHoa.TenHh;
                    edithh.MaLoai = hangHoa.MaLoai;
                    edithh.DonGia = hangHoa.DonGia;
                    edithh.GiamGia = hangHoa.GiamGia;

                    edithh.MoTa = hangHoa.MoTa;
                    edithh.ThuongHieu = hangHoa.ThuongHieu;
                    edithh.XuatXu = hangHoa.XuatXu;
                    edithh.SoluongBan = hangHoa.SoluongBan;
                    edithh.SoLuong = hangHoa.SoLuong;
                    edithh.DiemReview = hangHoa.DiemReview;
                    edithh.ChiTiet = hangHoa.ChiTiet;
                    _dbcontext.Update(edithh);
                    _dbcontext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Loi: {ex.Message}");

                    ViewBag.ThongBaoLoi = "Có lỗi";
                    ViewBag.DanhSachLoai = new LoaiDropDownVM(_dbcontext.Loais, "MaLoai", "TenLoai", "MaLoai");
                    return View();
                }
            }

            ViewBag.DanhSachLoai = new LoaiDropDownVM(_dbcontext.Loais, "MaLoai", "TenLoai", "MaLoai");
            return View();
        }
        
        public IActionResult Delete(Guid id)
        {
            var hh = _dbcontext.HangHoas.FirstOrDefault(h => h.MaHangHoa == id);
            _dbcontext.Remove(hh);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
