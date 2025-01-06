using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class Messages
    {
        public static string ProductAdded = "Ürün Eklendi.";
        public static string ProductNameInvalid = "Ürün İsmi Geçersiz.";
        public static string MaintenanceTime="Sistem bakım zamanı";
        public static string ProductedListed="Ürünler Listelendi.";

        public static string UnitPriceInvalid = "Ürün fiyatı geçersiz";
        public static string ProductCountOfCategoryError="Aynı kategorideki ürün kotası aşıldığı için eklenememektedir.";
        public static string ProductNameAlreadyExists = "Aynı adla ürün eklenemez!";
        public static string CategoryLimitExcided="En fazla 15 kategoride ürün eklenebilir";
        public static string AuthorizationDenied="Yetkiniz yok";
       
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";

    }
}
