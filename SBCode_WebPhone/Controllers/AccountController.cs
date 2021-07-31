using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBCode_WebPhone.Data;
using SBCode_WebPhone.Helper;
using SBCode_WebPhone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SBCode_WebPhone.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;
        public AccountController(MyDBContext ctx, IMapper mapper)
        {
            _context = ctx; _mapper = mapper;
        }

       [HttpGet]
        public IActionResult DangKy()
        {

            return View();
        }

        [HttpPost]
        public IActionResult DangKy(DangKyVM model)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var khachHang = _mapper.Map<KhachHang>(model);
                        khachHang.MaNgauNhien = FileTools.GetRandom();
                        khachHang.MatKhau = model.MatKhau.ToSHA512Hash(khachHang.MaNgauNhien);
                        _context.Add(khachHang);
                        _context.SaveChanges();

                        //Add role for user, default Customer
                        var userRole = new UserRole
                        {
                            RoleId = 14,//Khách hàng
                            UserId = khachHang.MaKh
                        };
                        _context.Add(userRole);
                        _context.SaveChanges();

                        transaction.Commit();
                        return RedirectToAction("DangNhap");
                    }
                    catch
                    {
                        transaction.Rollback();
                        return View();
                    }
                }

            }
            return View();
        }

        [HttpGet]
        public IActionResult DangNhap(string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(DangNhapVM model, string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            string thongBaoLoi = string.Empty;
            if (ModelState.IsValid)
            {
                 var  khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.Email == model.Email);
                if (khachHang == null)
                {
                    ViewBag.ThongBaoLoi = "Tài khoản không tồn tại.";
                    return View();
                }
                if (khachHang.DangHoatDong == false)
                {
                    
                    khachHang.DangHoatDong = true ;
                    //ViewBag.ThongBaoLoi = "Tài khoản đang bị khóa.";
                    //return View();
                }
                if (khachHang.MatKhau != model.MatKhau.ToSHA512Hash(khachHang.MaNgauNhien))
                {
                    ViewBag.ThongBaoLoi = "Sai thông tin đăng nhập.";
                    return View();
                }
                //set các claims
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, khachHang.HoTen),
                    new Claim(ClaimTypes.Email, khachHang.Email),
                    new Claim("MaNguoiDung", khachHang.MaKh.ToString())
                };
 
                var roles = _context.UserRoles.Where(r => r.UserId == khachHang.MaKh).Select(ur => ur.Role).ToList();
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                }
                var claimIdentity = new ClaimsIdentity(claims, "login");
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);
                await HttpContext.SignInAsync(claimPrincipal);

                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    //nếu là admin
                    foreach(var pq in roles)
                    {
                        if(pq.RoleId == 11 || pq.RoleId == 12|| pq.RoleId == 13)
                            return Redirect("/admin/HangHoa");
                    }    
                    return RedirectToAction(actionName: "Index", controllerName: "HangHoa");
                }
            }

            ViewBag.ThongBaoLoi = thongBaoLoi;
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            var claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Email);
            var kh = _context.KhachHangs.FirstOrDefault(p => p.Email == claim.Value);
                var user = new DangKyVM
                {
                    Email = kh.Email,
                    DiaChi = kh.DiaChi,
                    HoTen = kh.HoTen,
                    SoDienThoai = kh.SoDienThoai
                };
                return View(user);
        }

        

        [HttpPost]
        public async Task<IActionResult> Profile(DangKyVM user)
        {
            KhachHang user1 = _context.KhachHangs.FirstOrDefault(kh => kh.Email == user.Email);
                user1.DiaChi = user.DiaChi;
                user1.HoTen = user.HoTen;
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user1.HoTen),
                    new Claim(ClaimTypes.Email, user1.Email),
                    new Claim("MaNguoiDung", user1.MaKh.ToString())
                };
                var claimIdentity = new ClaimsIdentity(claims, "login");
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);
                await HttpContext.SignInAsync(claimPrincipal);
                user1.SoDienThoai = user.SoDienThoai;
                _context.KhachHangs.Update(user1);
                _context.SaveChanges();
            return RedirectToAction("Index","HangHoa");
        }

        [Authorize]
        public IActionResult DoiMatKhau()
        {
            DoiMatKhauVM mk = new DoiMatKhauVM() { MatKhauMoi = "", NhapLaiMatKhauMoi = "", XacNhanMatKhauCu = "" };
                return View(mk);
        }
        [HttpPost]
        public  IActionResult DoiMatKhau(DoiMatKhauVM matKhau)
        { 
            var claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Email);
            var khang = _context.KhachHangs.SingleOrDefault(p => p.Email == claim.Value);
            if (khang.MatKhau == matKhau.XacNhanMatKhauCu.ToSHA512Hash(khang.MaNgauNhien))
            {
                khang.MatKhau = matKhau.MatKhauMoi.ToSHA512Hash(khang.MaNgauNhien);
                _context.KhachHangs.Update(khang);
                _context.SaveChanges();
                ViewBag.TrangThai = "Đổi Mật Khẩu Thành Công :)";
              
                return RedirectToAction("DangXuat", "Account"); 
            }
            ViewBag.TrangThai = "Đổi Mật Khẩu Thất Bại :(";
            return View();
        }
        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToAction("DangNhap", "Account");
            
        }

        public IActionResult DonHangDaMua()
        {
            var maKH = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "MaNguoiDung").Value);
            var listdh = _context.DonHangs.Where(dh => dh.MaKh == maKH);
            List<DonHangVM> DonHang = new List<DonHangVM>();
           
            foreach(var dh in listdh)
            {
                DonHangVM item = new DonHangVM();
                item.MaDh = dh.MaDh;
                item.MaKh = dh.MaKh;
                item.NgayDat = dh.NgayDat;
                item.NguoiNhan = dh.NguoiNhan;
                item.DiaChiGiao = dh.DiaChiGiao;
                item.TongTien = dh.TongTien;
                DonHang.Add(item);
        

            }    
            ViewData["DonHang"] = DonHang;
            var ChiTietDonHang = _context.DonHangChiTiets.Select(hh => new ChiTietDonHangVM
            {
                MaHh = hh.MaHh,
                MaDh = hh.MaDh,
                DonGia = hh.DonGia,
                SoLuong = hh.SoLuong,
                TenHh = hh.HangHoa.TenHh,
                Hinh = hh.HangHoa.Hinh
                
            }).ToList();
            return View(ChiTietDonHang);
        }
    }
}
