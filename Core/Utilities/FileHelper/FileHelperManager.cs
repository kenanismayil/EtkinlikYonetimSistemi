using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.FileHelper
{
    public class FileHelperManager : IFileHelper
    {
        public void Delete(string filePath) //Burada ki string filePath, 'Image'dan gelen dosyamın kaydedildiği adres ve adı 
        {
            if (File.Exists(filePath)) //parametrede gelen adreste öyle bir dosya var mı diye kontrol ediliyor.
            {
                File.Delete(filePath); //Eğer dosya var ise dosya bulunduğu yerden siliniyor.
            }
        }

        public string Update(IFormFile file, string filePath, string root) //Dosya güncellemek için ise gelen parametreye baktığımızda;
//Güncellenecek yeni dosya, eski dosyamızın kayıt dizini ve yeni bir kayıt dizini
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            // Eski dosya silindikten sonra yerine geçecek yeni dosyaiçin alttaki *Upload* metoduna
            // yeni dosya ve kayıt edileceği adres parametre olarak döndürülüyor.
            return Upload(file, root);
        }

        public string Upload(IFormFile file, string root)
        {
            //file.Length=>Dosya uzunluğunu bayt olarak alır. burada Dosya gönderildi mi gönderilmemiş diye test işlemi yapıldı.
            if (file.Length > 0)
            {
                //Directory=>System.IO'nın bir class'ı. burada ki işlem tam olarak şu. Bu Upload metodumun parametresi olan
                //string root Image'dan gelmekte
                if (!Directory.Exists(root))
                {
                    //Manager içerisine girdiğinizde buraya parametre olarak *PathConstants.ImagesPath* böyle bir şey gönderilidğini görürsünüz.
                    //PathConstants clası içerisine girdiğinizde string bir ifadeyle bir dizin adresi var
                    //O adres bizim Yükleyeceğimiz dosyaların kayıt edileceği adres burada *Check if a directory Exists* ifadesi şunu belirtiyor
                    //dosyanın kaydedileceği adres dizini var mı? varsa if yapısının kod bloğundan ayrılır eğer yoksa içinde ki kodda dosyaların kayıt edilecek dizini oluşturur.
                    Directory.CreateDirectory(root);
                }

                //Path.GetExtension(file.FileName)=>> Seçmiş olduğumuz dosyanın uzantısını elde ediyoruz.
                string extension = Path.GetExtension(file.FileName);
                //Core.Utilities.GuidHelper klasürünün içinde ki GuidManager klasörüne giderseniz
                //burada satırda ne yaptığımızı anlayacaksınız
                string fileName = Guid.NewGuid().ToString();
                //Dosyanın oluşturduğum adını ve uzantısını yan yana getiriyorum. Mesela metin dosyası ise
                //.txt gibi bu projemizde resim yükyeceğimiz için .jpg olacak uzantılar 
                string filePath = String.Concat(fileName, extension);

                string fileFullPath = Path.Combine(root, filePath);
                //Dosyanın oluşturduğum adını ve uzantısını yan yana getiriyorum.
                //Mesela metin dosyası ise .txt gibi bu projemizde resim yükyeceğimiz için .jpg olacak uzantılar 
                using (FileStream fileStream = File.Create(fileFullPath))
                {
                    //Dosyanın oluşturduğum adını ve uzantısını yan yana getiriyorum. Mesela metin dosyası ise
                    //.txt gibi bu projemizde resim yükyeceğimiz için .jpg olacak uzantılar 
                    file.CopyTo(fileStream);
                    //arabellekten siler.
                    fileStream.Flush();
                    //burada dosyamızın tam adını geri gönderiyoruz sebebide sql servere dosya eklenirken adı ile eklenmesi için.
                    return filePath;
                }
            }
            return null;
        }
        
    }
}
