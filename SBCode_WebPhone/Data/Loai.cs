using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SBCode_WebPhone.Data
{
    public class Loai
    {
        public int MaLoai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập dữ liệu")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Chiều dài không chính xác")]
        [RegularExpression(@"^[^0-9]+.*.$", ErrorMessage = "Ký tự đầu phải là chữ")]
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        public int? MaLoaiCha { get; set; }
        [ForeignKey("MaLoaiCha")]
        public Loai LoaiCha { get; set; }
        public ICollection<HangHoa> HangHoas { get; set; }
    }
}
