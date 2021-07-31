using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SBCode_WebPhone.Data;
using SBCode_WebPhone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBCode_WebPhone.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaiController : Controller
    {
        private readonly MyDBContext _dbcontext;
        private readonly ILogger _logger;

        public LoaiController(MyDBContext ctx, ILogger<LoaiController> logger)
        {
            _dbcontext = ctx;
            _logger = logger;
        }

       
        public IActionResult Index()
        {
            var data = _dbcontext.Loais.Include(lc => lc.LoaiCha).ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.DanhSachLoaiCha = new LoaiDropDownVM(_dbcontext.Loais, "MaLoai", "TenLoai", "MaLoaiCha");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Loai loai)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Add(loai);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DanhSachLoaiCha = new LoaiDropDownVM(_dbcontext.Loais, "MaLoai", "TenLoai", "MaLoaiCha");
            return View();
        }

        public IActionResult Edit(int id)
        {
            var ml = _dbcontext.Loais.FirstOrDefault(l => l.MaLoai == id);
            ViewBag.DanhSachLoaiCha = new LoaiDropDownVM(_dbcontext.Loais, "MaLoai", "TenLoai", "MaLoaiCha",ml.MaLoai);
            return View(ml);
        }

        [HttpPost]
        public IActionResult Edit(Loai loai)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var editloai = _dbcontext.Loais.FirstOrDefault(l => l.MaLoai == loai.MaLoai);
                    editloai.TenLoai = loai.TenLoai;
                    editloai.MoTa = loai.MoTa;
                    editloai.MaLoaiCha = loai.MaLoaiCha;
                    _dbcontext.Update(editloai);
                    _dbcontext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Loi: {ex.Message}");

                    ViewBag.ThongBaoLoi = "Có lỗi";
                    ViewBag.DanhSachLoaiCha = new LoaiDropDownVM(_dbcontext.Loais, "MaLoai", "TenLoai", "MaLoaiCha");
                    return View();
                }
            }
            ViewBag.DanhSachLoaiCha = new LoaiDropDownVM(_dbcontext.Loais, "MaLoai", "TenLoai", "MaLoaiCha");
            return View();
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var hh = _dbcontext.Loais.FirstOrDefault(h => h.MaLoai == id);
                _dbcontext.Remove(hh);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Loi: {ex.Message}");
                ViewBag.ThongBaoLoi = "Có lỗi";
                return RedirectToAction("Index");

            }

        }
    }
}
