using IlacinGelsinV2.Models;
using IlacinGelsinV2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IlacinGelsinV2.Auth
{
    public class UyeServis
    {
        IlacinGeslsinEntities db = new IlacinGeslsinEntities();

        public UyeModel UyeOturumAc(string kadi, string parola)
        {
            UyeModel uye = db.Uye.Where(d => d.uyeKullaniciAdi == kadi && d.uyeSifre == parola).Select(s => new UyeModel()
            {
                uyeId = s.uyeId,
                uyeKullaniciAdi = s.uyeKullaniciAdi,
                uyeEposta = s.uyeEposta,
                uyeAdsoyad = s.uyeAdsoyad,
                uyeSifre = s.uyeSifre,
                uyeAdmin = s.uyeAdmin
            }).SingleOrDefault();

             return uye;
        }
    }
}