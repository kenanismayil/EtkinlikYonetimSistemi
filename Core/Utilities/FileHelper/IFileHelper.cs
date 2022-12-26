using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.FileHelper
{
    //IFormFile projemize bir dosya yüklemek için kulanılan yöntemdir, HttpRequest ile gönderilen bir dosyayı temsil eder.
    //FileStream, Stream ana soyut sınıfı kullanılarak genişletilmiş, belirtilen kaynak dosyalar üzerinde okuma/yazma/atlama
    //gibi operasyonları yapmamıza yardımcı olan bir sınıftır
    public interface IFileHelper
    {
        string Upload(IFormFile file, string root);
        void Delete(string filePath);
        string Update(IFormFile file, string filePath, string root);
    }
}
