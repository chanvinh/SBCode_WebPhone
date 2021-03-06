using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SBCode_WebPhone.Data
{
    public class HangHoa
    {
        public Guid MaHangHoa { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập dữ liệu")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Chiều dài không chính xác")]
        [RegularExpression(@"^[^0-9]+.*.$", ErrorMessage = "Ký tự đầu phải là chữ")]

        public string TenHh { get; set; }

        [Range(10000, 10000000000, ErrorMessage = "Đơn giá phải lớn hơn 10000 VND")]
        public double DonGia { get; set; }

        [Range(0, 100, ErrorMessage = "Giảm giá được tính theo %")]
        public byte GiamGia { get; set; }
        [Range(1, 10000000000, ErrorMessage = "Số lượng phải lớn hơn 1")]
        [RegularExpression(@"[0-9]*[0-9]$", ErrorMessage = "Số Lượng không hợp lệ")]
        public int SoLuong { get; set; }
        public string Hinh { get; set; }
        public string ChiTiet { get; set; }
        public string MoTa { get; set; }
        public string ThuongHieu { get; set; }
        public string XuatXu { get; set; }
        public int SoluongBan { get; set; }
        public int? MaLoai { get; set; }
        public Loai Loai { get; set; }
        public virtual ICollection<HangHoaTag> HangHoaTags { get; set; }
        public virtual ICollection<HinhPhu> HinhPhus { get; set; }
        public virtual ICollection<ReviewHangHoa> ReviewHangHoas { get; set; }

        public double? DiemReview { get; set; }
    }
    public class ReviewHangHoa
    {
        public Guid Id { get; set; }
        public DateTime NgayReview { get; set; }
        public byte DiemReview { get; set; }
        public int TieuChi { get; set; }
        public Guid MaHangHoa { get; set; }
        [ForeignKey("TieuChi")]
        public Review Review { get; set; }
        [ForeignKey("MaHangHoa")]
        public HangHoa HangHoa { get; set; }
    }

    public class Review
    {
        public int Id { get; set; }
        public string Criteria { get; set; }
        public bool Active { get; set; }
        public ICollection<ReviewHangHoa> ReviewHangHoas { get; set; }
    }

    public class HinhPhu
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public Guid? MaHangHoa { get; set; }
        public HangHoa HangHoa { get; set; }
    }

    public class HangHoaTag
    {
        public string TagKey { get; set; }
        public Guid MaHangHoa { get; set; }
        public HangHoa HangHoa { get; set; }
        public Tag Tag { get; set; }
    }
    public class Tag
    {
        public virtual ICollection<HangHoaTag> HangHoaTags { get; set; }
        public string TagKey { get; set; }
        public string TagValue { get; set; }
    }
}
