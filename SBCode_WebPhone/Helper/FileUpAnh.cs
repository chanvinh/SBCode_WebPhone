using Microsoft.AspNetCore.Http;
using SBCode_WebPhone.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SBCode_WebPhone.Helper
{
    public class FileUpAnh
    {
        public static string UploadFileToFolder(String HinhHienTai,IFormFile file, string folderName)
        {
            
            try
            {
                
                if (file != null)
                {
                    var fileName = $"{DateTime.Now.Ticks}_{file.FileName}";
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folderName, fileName);
                    using (var myFile = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(myFile);
                    }
                     return fileName;
                }

                return HinhHienTai;

            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
