using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SBCode_WebPhone.Data;
using SBCode_WebPhone.Helper;
using SBCode_WebPhone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PayPal.Core;
using PayPal.v1.Payments;
using BraintreeHttp;

namespace SBCode_WebPhone.Controllers
{
    public class GioHangController : Controller
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;
        private readonly string _clientId;
        private readonly string _secretKey;

        public double TyGiaUSD = 20000;//store in Database
        public GioHangController(MyDBContext ctx, IMapper mapper, IConfiguration config)
        {
            _context = ctx;
            _mapper = mapper;
            _clientId = config["PaypalSettings:ClientId"];
            _secretKey = config["PaypalSettings:SecretKey"];
        }

        public List<GioHangVM> GioHang
        {
            get
            {
                var donghang = HttpContext.Session.Get<List<GioHangVM>>("GioHang");
                if (donghang == null)
                {
                    donghang = new List<GioHangVM>();
                }
                return donghang;
            }
        }

        public IActionResult Index()
        {
            var dsHangHoa = _context.HangHoas.Select(hh => new HangHoaVM
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
            }).OrderByDescending(hh => hh.SoLuongBan);
            ViewData["ListHangHoa"] = dsHangHoa.ToList();
            return View(dsHangHoa);
        }

        public IActionResult ThemVaoGio(Guid id, string addType, int soluong = 1)
        {
            //lấy giỏ hàng hiện tại
            var gioHang = GioHang;
            //kiểm tra hàng đã có trong giỏ
            var item = gioHang.SingleOrDefault(it => it.MaHangHoa == id);
            if (item != null)//đã có
            {
                int slkho = _context.HangHoas.Find(id).SoLuong;
                if ((soluong + item.SoLuong) <= slkho)
                    item.SoLuong += soluong;
                else
                {
                    item.SoLuong = slkho;
                }
            }
            else
            {
                var hh = _context.HangHoas.FirstOrDefault(p => p.MaHangHoa == id);
                item = _mapper.Map<GioHangVM>(hh);
                item.DonGia = hh.DonGia;
                if (soluong <= hh.SoLuong)
                    item.SoLuong = soluong;
                else
                    item.SoLuong = hh.SoLuong;
                gioHang.Add(item);
            }
            HttpContext.Session.Set("GioHang", gioHang);

            if (addType == "ajax")
                return PartialView("_Layout_GioHang");

            return RedirectToAction("Index");
        }

        public IActionResult RemoveCartItem(Guid id, bool xoasl = false /*bool isAjaxCall = false*/)
        {

            //lấy giỏ hàng hiện tại
            var gioHang = GioHang;
            var item = gioHang.SingleOrDefault(it => it.MaHangHoa == id);
            if (item != null)
            {
                
                if (xoasl == true)
                {
                    if (item.SoLuong > 1)
                    {
                        item.SoLuong -= 1;
                        HttpContext.Session.Set("GioHang", gioHang);
                    }

                }
                if ( xoasl == false)
                {
                    gioHang.Remove(item);
                    HttpContext.Session.Set("GioHang", gioHang);
                }
            }
            else
            {
                HttpContext.Session.Remove("GioHang");
            }





                //kiểm tra hàng đã có trong giỏ


                //if (isAjaxCall) { }

                return RedirectToAction("Index");
        }


        [Authorize, HttpGet]
        public IActionResult ThanhToan()
        {
            if ((this.User.FindFirstValue(ClaimTypes.Email)) != null) { ViewBag.getdiachi = _context.KhachHangs.FirstOrDefault(kh => kh.Email == (this.User.FindFirstValue(ClaimTypes.Email))).DiaChi; };
            return View();
        }

        //[Authorize, HttpPost]
        //public IActionResult ThanhToan(ThanhToanVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var emailKh = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
        //        var maKH = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "MaNguoiDung").Value);
        //        using (var trans = _context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                var donHang = new DonHang
        //                {
        //                    MaDh = Guid.NewGuid(),
        //                    MaKh = maKH,
        //                    NgayDat = DateTime.UtcNow,
        //                    TinhTrangDonHang = TinhTrangDonHang.MoiDatHang,
        //                    DiaChiGiao = model.DiaChiGiao,
        //                    NguoiNhan = model.NguoiNhan
        //                };
        //                _context.Add(donHang);
        //                foreach (var item in GioHang)
        //                {
        //                    _context.Add(new DonHangChiTiet
        //                    {
        //                        MaDh = donHang.MaDh,
        //                        MaHh = item.MaHangHoa,
        //                        SoLuong = item.SoLuong,
        //                        DonGia = item.DonGia
        //                    });
        //                }
        //                _context.SaveChanges();
        //                trans.Commit();
        //                HttpContext.Session.Remove("GioHang");
        //                //return Redirect("/KhachHang/HangDaMua");
        //            }
        //            catch (Exception ex)
        //            {
        //                //log
        //                trans.Rollback();
        //                return View();
        //            }
        //        }

        //    }
        //    return View();
        //}

        [Authorize]
        public async System.Threading.Tasks.Task<IActionResult> PaypalCheckout(ThanhToanVM model)
        {
            var environment = new SandboxEnvironment(_clientId, _secretKey);
            var client = new PayPalHttpClient(environment);

            #region Create Paypal Order
            var itemList = new ItemList()
            {
                Items = new List<Item>()
            };
            var total = (Math.Round(GioHang.Sum(p => p.ThanhTien) / TyGiaUSD, 0));
            foreach (var item in GioHang)
            {
                itemList.Items.Add(new Item()
                {
                    Name = item.TenHh,
                    Currency = "USD",
                    Price = (Math.Round(item.GiaBan / TyGiaUSD, 0)).ToString(),
                    Quantity = item.SoLuong.ToString(),
                    Sku = "sku",
                    Tax = "0",
                    
                });
            }
            #endregion

            var paypalOrderId = DateTime.Now.Ticks;
            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = total.ToString(),
                            Currency = "USD",
                            Details = new AmountDetails
                            {
                                Tax = "0",
                                Shipping = "0",
                                Subtotal = total.ToString()
                            }

                        },
                        ItemList = itemList,
                        Description = $"Invoice #{paypalOrderId}",
                        InvoiceNumber = paypalOrderId.ToString()

                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    CancelUrl = $"{hostname}/GioHang/CheckoutFail",
                    ReturnUrl = $"{hostname}/GioHang/CheckoutSuccess/?DiaChiGiao=" + Chuyenlink(model.DiaChiGiao) + "&NguoiNhan=" + Chuyenlink(model.NguoiNhan),
                },
            
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };

            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);
            

            try
            {
                var response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment  
                        paypalRedirectUrl = lnk.Href;
                    }
                }

                return Redirect(paypalRedirectUrl);
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                //Process when Checkout with Paypal fails
                return Redirect("/GioHang/CheckoutFail");
            }
        }

        public IActionResult CheckoutFail()
        {
            //Tạo đơn hàng trong database với trạng thái thanh toán là "Chưa thanh toán"
            //Xóa session
            return View();
        }

        public string Chuyenlink(string chuoi)
        {
            string href = "";
            string[] link = chuoi.Split(" ");
            foreach(string c in link)
            {
                href += c + "+";
            }
            return href;
        }

        public IActionResult CheckoutSuccess(string DiaChiGiao, string NguoiNhan)
        {

            if (ModelState.IsValid)
            {
                var emailKh = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                var maKH = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "MaNguoiDung").Value);
                using (var trans = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var donHang = new DonHang
                        {
                            MaDh = Guid.NewGuid(),
                            MaKh = maKH,
                            NgayDat = DateTime.Now,
                            TinhTrangDonHang = TinhTrangDonHang.MoiDatHang,
                            TongTien = GioHang.Sum(p => p.ThanhTien),
                            DiaChiGiao = DiaChiGiao,
                            NguoiNhan = NguoiNhan
                        };
                        _context.Add(donHang);
                        _context.SaveChanges();
                        foreach (var item in GioHang)
                        {
                            _context.Add(new DonHangChiTiet
                            {
                                MaDh = donHang.MaDh,
                                MaHh = item.MaHangHoa,
                                SoLuong = item.SoLuong,
                                DonGia = item.GiaBan
                            });

                            _context.HangHoas.FirstOrDefault(hh => hh.MaHangHoa == item.MaHangHoa).SoluongBan += item.SoLuong;

                            _context.HangHoas.FirstOrDefault(hh => hh.MaHangHoa == item.MaHangHoa).SoLuong -= item.SoLuong;
                        }
                        _context.SaveChanges();
                        trans.Commit();
                        HttpContext.Session.Remove("GioHang");

                    }
                    catch (Exception ex)
                    {
                        //log
                        trans.Rollback();
                        return View();
                    }
                }

            }
            return View();
        }
    }
}

