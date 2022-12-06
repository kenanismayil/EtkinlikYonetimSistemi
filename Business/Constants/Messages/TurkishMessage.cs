using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants.Messages
{
    public class TurkishMessage
    {
        //Common magic string
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ErrorMessage = "Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.";
        public static string SuccessMessage = "İşlem başarılı";
        public static string ForeignKeyMessage = "Bu veri bir yabancı anahtar olarak kullanılıyor.";
        public static string BusinessRuleError = "İş kurallarına uymayan işlem var.";
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string UserRegistered = "Kayıt gerçekleştirildi";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Parola hatalı";
        public static string SuccessfulLogin = "Başarılı giriş";
        public static string UserAlreadyExists = "Kullanıcı mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";

        //Activity magic string
        public static string ActivityAdded = "Aktivite eklendi";
        public static string ActivityDeleted = "Aktivite silindi";
        public static string ActivityUpdated = "Aktivite guncellendi";
        public static string ActivitiesListed = "Aktiviteler listelendi";
        public static string ActivityNameInvalid = "Aktivite ismi gecersiz";
        public static string ActivityCountOfTypeError = "Bir aktivite tipinde en fazla 10 aktivite olabilir";
        public static string ActivityNameAlreadyExists = "Bu isimde zaten başka bir aktivite vardır";
        public static string ActivityTypeLimitExceded = "Aktivite tipinin limiti aşıldığı için yeni aktivite eklenemiyor";

        //ActivityType magic string
        public static string ActivityTypeAdded = "Aktivite tipi eklendi";
        public static string ActivityTypeDeleted = "Aktivite tipi silindi";
        public static string ActivityTypeUpdated = "Aktivite tipi guncellendi";
        public static string ActivityTypesListed = "Aktivite tipleri listelendi";
        public static string ActivityTypeNameInvalid = "Aktivite tipi ismi gecersiz";
        public static string ActivityTypeNameAlreadyExists = "Bu isime sahip zaten bir aktivite tipi vardır";

        //User magic string
        public static string UserAdded = "Kullanıcı eklendi";
        public static string UserDeleted = "Kullanıcı silindi";
        public static string UserUpdated = "Kullanıcı guncellendi";
        public static string UsersListed = "Kullanıcılar listelendi";
        public static string EmailsListed = "Emailler listelendi";
        public static string FirstNameInvalid = "Kullanıcı ismi gecersiz";
        public static string LastNameInvalid = "Kullanıcı soyismi gecersiz";
        public static string UserInfoListed = "Kullanıcı bilgisi listelendi";
        public static string UserDetailListed = "Kullanıcı detayı listelendi";


        //Moderator magic string
        //public static string ModeratorAdded = "Moderator eklendi";
        //public static string ModeratorDeleted = "Moderator silindi";
        //public static string ModeratorUpdated = "Moderator guncellendi";
        //public static string ModeratorsListed = "Moderator listelendi";
        //public static string ModeratorNamesListed = "Moderator isimleri listelendi";
        //public static string ModeratorNameInvalid = "Moderator ismi gecersiz";

        //Certificate magic string
        public static string CertificateAdded = "Sertifika yapildi";
        public static string CertificateDeleted = "Sertifika silindi";
        public static string CertificateUpdate = "Sertifika güncellendi";
        public static string CertificateNameInvalid = "Sertifika ismi gecersiz";
        public static string CertificateListed = "Sertifikalar listelendi";
        public static string GivenDateListed = "Verilme tarihleri listelendi";
        public static string ExpiryDateListed = "Son gecerlilik tarihleri listelendi";


        //Registration magic string
        public static string RegistrationAdded = "Kayıt yapildi";
        public static string RegistrationDeleted = "Kayıt silindi";
        public static string RegistrationUpdated = "Kayıt güncellendi";
        public static string RegistrationNameInvalid = "Kayıt ismi gecersiz";
        public static string RegistrationListed = "Kayıtlar listelendi";
        public static string RegistrationDateListed = "Kayıt tarihleri listelendi";

        //City magic string
        public static string CityAdded = "Şehir eklendi";
        public static string CityDeleted = "Şehir silindi";
        public static string CityUpdated = "Şehir güncellendi";
        public static string CitiesListed = "Şehirler listelendi";
        public static string CityNameInvalid = "Şehir ismi gecersiz";
        public static string CityNameAlreadyExists = "Bu isime sahip zaten bir şehir vardır";


        //Country magic string
        public static string CountryAdded = "Ülke eklendi";
        public static string CountryDeleted = "Ülke silindi";
        public static string CountryUpdated = "Şehir güncellendi";
        public static string CountriesListed = "Ülkeler listelendi";
        public static string CountryNameInvalid = "Ülke ismi gecersiz";
        public static string CountryNameAlreadyExists = "Bu isime sahip zaten bir ülke vardır";


        //Comment magic string
        public static string CommentAdded = "Yorum eklendi";
        public static string CommentDeleted = "Yorum silindi";
        public static string CommentUpdated = "Yorum güncellendi";
        public static string CommentsListed = "Yorumlar listelendi";
        public static string CommentNameInvalid = "Yorum ismi gecersiz";

        //RoleType magic string
        public static string RoleTypeAdded = "Rol türü eklendi";
        public static string RoleTypeUpdated = "Rol türü güncellendi";
        public static string RoleTypeDeleted = "Rol türüsilindi";
        public static string RoleTypeListed = "Rol türü listelendi";
        public static string RoleTypeInvalid = "Rol türü gecersiz";


        //ActivityImage magic string
        public static string ActivityImageAdded = "Aktivite resmi eklendi";
        public static string ActivityImageUpdated = "Aktivite resmi güncellendi";
        public static string ActivityImageDeleted = "Aktivite resmi silindi";
        public static string ActivityImageListed = "Aktivite resimleri listelendi";
        public static string ActivityImageInvalid = "Aktivite resmi gecersiz";
        public static string ActivityImageLimitExceded = "Bir aktiviteye maksimum 5 resim eklenebilir";


        //Location magic string
        public static string LocationAdded = "Lokasyon eklendi";
        public static string LocationUpdated = "Lokasyon güncellendi";
        public static string LocationDeleted = "Lokasyon silindi";
        public static string LocationsListed = "Lokasyonlar listelendi";
        public static string LocationNameAlreadyExists = "Bu isimde zaten başka bir lokasyon ismi vardır";
    }
}
