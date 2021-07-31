using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SBCode_WebPhone.ViewModels
{
    public class DangNhapVM
    {
        [MaxLength(100)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
    }
}
