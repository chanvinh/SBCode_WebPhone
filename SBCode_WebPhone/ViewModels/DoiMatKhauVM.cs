using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace SBCode_WebPhone.ViewModels
{
    public class DoiMatKhauVM
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string MatKhauMoi { get; set; }

        [Compare("MatKhauMoi", ErrorMessage = "Mật khẩu không khớp")]
        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu mới")]
        public string NhapLaiMatKhauMoi { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu cũ")]
        public string XacNhanMatKhauCu { get; set; }
    }
}
