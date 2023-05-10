using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IlacinGelsinV2.Models;
using IlacinGelsinV2.ViewModel;

namespace IlacinGelsinV2.Controllers
{
    public class ServisController : ApiController
    {
        IlacinGeslsinEntities db = new IlacinGeslsinEntities();
        SonucModel sonuc = new SonucModel();

        #region Kategori
        [HttpGet]
        [Route("api/kategoriliste")]
        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(s => new KategoriModel()
            {
                katId = s.katId,
                katAdi = s.katAdi
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/kategoribyid/{katId}")]
        public KategoriModel KategoriById(int katId)
        {
            KategoriModel kayit = db.Kategori.Where(d => d.katId == katId).Select(s => new KategoriModel()
            {
                katId = s.katId,
                katAdi = s.katAdi
            }).SingleOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(d => d.katAdi == model.katAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Zaten Bulunmakta!";
                return sonuc;
            }
            Kategori kayit = new Kategori();
            kayit.katAdi = model.katAdi;
            db.Kategori.Add(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi!";
            return sonuc;
        }

        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(d => d.katId == model.katId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadi!";
                return sonuc;
            }
            kayit.katAdi = model.katAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi!";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/kategorisil/{katId}")]
        public SonucModel KategoriSil(int katId)
        {
            Kategori kayit = db.Kategori.Where(d => d.katId == katId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadi!";
                return sonuc;
            }
            db.Kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi!";
            return sonuc;
        }
        #endregion

        #region Ilac
        [HttpGet]
        [Route("api/ilacliste")]
        public List<IlacModel> IlacListe()
        {
            List<IlacModel> liste = db.Ilac.Select(s => new IlacModel()
            {
                ilacId = s.ilacId,
                ilacAdi = s.ilacAdi,
                ilacFoto = s.ilacFoto,
                ilacKatId = s.ilacKatId,
                ilacAciklama = s.ilacAciklama,
                ilacFiyat = s.ilacFiyat
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/ilacsoneklenenler/{x}")]
        public List<IlacModel> IlacListeSonEklenenler(int x)
        {
            List<IlacModel> liste = db.Ilac.OrderByDescending(o => o.ilacId).Take(x).Select(s => new IlacModel()
            {
                ilacId = s.ilacId,
                ilacAdi = s.ilacAdi,
                ilacFoto = s.ilacFoto,
                ilacKatId = s.ilacKatId,
                ilacAciklama = s.ilacAciklama,
                ilacFiyat = s.ilacFiyat
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/ilaclistebykatid/{katId}")]
        public List<IlacModel> IlacListe(int katId)
        {
            List<IlacModel> liste = db.Ilac.Where(d=>d.ilacKatId == katId).Select(s => new IlacModel()
            {
                ilacId = s.ilacId,
                ilacAdi = s.ilacAdi,
                ilacFoto = s.ilacFoto,
                ilacKatId = s.ilacKatId,
                ilacAciklama = s.ilacAciklama,
                ilacFiyat = s.ilacFiyat
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/ilacbyid/{ilacId}")]
        public IlacModel IlacById(int ilacId)
        {
            IlacModel kayit = db.Ilac.Where(d => d.ilacId == ilacId).Select(s => new IlacModel()
            {
                ilacId = s.ilacId,
                ilacAdi = s.ilacAdi,
                ilacFoto = s.ilacFoto,
                ilacKatId = s.ilacKatId,
                ilacAciklama = s.ilacAciklama,
                ilacFiyat = s.ilacFiyat
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/ilacekle")]
        public SonucModel IlacEkle(IlacModel model)
        {
            if (db.Ilac.Count(d => d.ilacAdi == model.ilacAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "İlaç Zaten Ekli Durumda!";
                return sonuc;
            }
            Ilac kayit = new Ilac();
            kayit.ilacAdi = model.ilacAdi;
            kayit.ilacAciklama = model.ilacAciklama;
            kayit.ilacFiyat = model.ilacFiyat;
            kayit.ilacFoto = model.ilacFoto;
            kayit.ilacKatId = model.ilacKatId;
            db.Ilac.Add(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "İlaç Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/ilacduzenle")]
        public SonucModel IlacDuzenle(IlacModel model)
        {
            Ilac kayit = db.Ilac.Where(d => d.ilacId == model.ilacId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "İlaç Bulunamadi!";
                return sonuc;
            }
            kayit.ilacAdi = model.ilacAdi;
            kayit.ilacAciklama = model.ilacAciklama;
            kayit.ilacFiyat = model.ilacFiyat;
            kayit.ilacFoto = model.ilacFoto;
            kayit.ilacKatId = model.ilacKatId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "İlaç Düzenlendi!";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/ilacsil/{ilacId}")]
        public SonucModel IlacSil(int ilacId)
        {
            Ilac kayit = db.Ilac.Where(d => d.ilacId == ilacId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "İlaç Bulunamadi!";
                return sonuc;
            }
            db.Ilac.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "İlaç Silindi!";
            return sonuc;
        }
        #endregion

        #region Uye
        [HttpGet]
        [Route("api/uyeliste")]
        public List<UyeModel> UyeListe()
        {
            List<UyeModel> liste = db.Uye.Select(s => new UyeModel()
            {
                uyeId = s.uyeId,
                uyeKullaniciAdi = s.uyeKullaniciAdi,
                uyeEposta = s.uyeEposta,
                uyeAdsoyad = s.uyeAdsoyad,
                uyeSifre = s.uyeSifre,
                uyeAdmin = s.uyeAdmin
            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyeModel UyeById(int uyeId)
        {
            UyeModel kayit = db.Uye.Where(d => d.uyeId == uyeId).Select(s => new UyeModel()
            {
                uyeId = s.uyeId,
                uyeKullaniciAdi = s.uyeKullaniciAdi,
                uyeEposta = s.uyeEposta,
                uyeAdsoyad = s.uyeAdsoyad,
                uyeSifre = s.uyeSifre,
                uyeAdmin = s.uyeAdmin
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/uyekayit")]
        public SonucModel UyeKayit(UyeModel model)
        {
            if (db.Uye.Count(s => s.uyeKullaniciAdi == model.uyeKullaniciAdi || s.uyeEposta == model.uyeEposta) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanıcı Adı ve ya Email Kayıtlı!";
                return sonuc;
            }
            Uye kayit = new Uye();
            kayit.uyeKullaniciAdi = model.uyeKullaniciAdi;
            kayit.uyeEposta = model.uyeEposta;
            kayit.uyeAdsoyad = model.uyeAdsoyad;
            kayit.uyeSifre = model.uyeSifre;
            kayit.uyeAdmin = model.uyeAdmin;
            db.Uye.Add(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Başarıyla Kayıt Oldunuz!";
            return sonuc;
        }

        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyeModel model)
        {
            if (db.Uye.Count(d => d.uyeKullaniciAdi == model.uyeKullaniciAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Zaten Bulunmakta!";
                return sonuc;
            }
            Uye kayit = new Uye();
            kayit.uyeKullaniciAdi = model.uyeKullaniciAdi;
            kayit.uyeEposta = model.uyeEposta;
            kayit.uyeAdsoyad = model.uyeAdsoyad;
            kayit.uyeSifre = model.uyeSifre;
            kayit.uyeAdmin = model.uyeAdmin;
            db.Uye.Add(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Üye Eklendi!";
            return sonuc;
        }
        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(UyeModel model)
        {
            Uye kayit = db.Uye.Where(d => d.uyeId == model.uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadi!";
                return sonuc;
            }
            kayit.uyeKullaniciAdi = model.uyeKullaniciAdi;
            kayit.uyeEposta = model.uyeEposta;
            kayit.uyeAdsoyad = model.uyeAdsoyad;
            kayit.uyeSifre = model.uyeSifre;
            kayit.uyeAdmin = model.uyeAdmin;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Üye Düzenlendi!";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public SonucModel UyeSil(int uyeId)
        {
            Uye kayit = db.Uye.Where(d => d.uyeId == uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadi!";
                return sonuc;
            }
            db.Uye.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi!";
            return sonuc;
        }
        #endregion

        #region Sepet
        [HttpGet]
        [Route("api/sepetbyuyeid/{uyeId}")]
        public List<SepetModel> SepetByUyeId(int uyeId)
        {
            List<SepetModel> liste = db.Sepet.Where(d => d.uyeId == uyeId).Select(s => new SepetModel()
            {
                sepetId = s.sepetId,
                ilacId = s.ilacId,
                uyeId = s.uyeId,
                sepetAdet = s.sepetAdet
            }).ToList();
            foreach (var item in liste)
            {
                item.ilacBilgi = IlacById(item.ilacId);
            }

            return liste;
        }
        [HttpPost]
        [Route("api/sepetekle")]
        public SonucModel SepetEkle(SepetModel model)
        {
            Sepet kayit = new Sepet();
            kayit.ilacId = model.ilacId;
            kayit.uyeId = model.uyeId;
            kayit.sepetAdet = model.sepetAdet;
            db.Sepet.Add(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Sepete Eklendi!";
            return sonuc;
        }
        [HttpPut]
        [Route("api/sepetduzenle")]
        public SonucModel SepetDuzenle(SepetModel model)
        {
            Sepet kayit = db.Sepet.Where(d => d.sepetId == model.sepetId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün Bulunamadi!";
                return sonuc;
            }
            kayit.sepetAdet = model.sepetAdet;
            kayit.ilacId = model.ilacId;
            kayit.uyeId = model.uyeId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Sepet Güncellendi!";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/sepettemizle/{uyeId}")]
        public SonucModel SepetTemizle(int uyeId)
        {
            List<Sepet> liste = db.Sepet.Where(d => d.uyeId == uyeId).ToList();

            foreach (var item in liste)
            {
                db.Sepet.Remove(item);
            }
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Sepet temizlendi!";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/sepetsil/{sepetId}")]
        public SonucModel SepetSil(int sepetId)
        {
            Sepet kayit = db.Sepet.Where(d => d.sepetId == sepetId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün Bulunamadı!";
                return sonuc;
            }

            db.Sepet.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ürün Silindi!!";
            return sonuc;
        }
        #endregion
    }
}
