using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
                katAdi = s.katAdi,
                urunSayisi = s.Ilac.Count
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
                katAdi = s.katAdi,
                urunSayisi = s.Ilac.Count
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
                ilacFiyat = s.ilacFiyat,
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
                uyeAdmin = s.uyeAdmin,
                uyeFoto = s.uyeFoto
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
                uyeAdmin = s.uyeAdmin,
                uyeFoto = s.uyeFoto
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
            kayit.uyeFoto = model.uyeFoto;
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
            kayit.uyeFoto = model.uyeFoto;
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

        [HttpPost]
        [Route("api/uyefotoguncelle")]
        public SonucModel UyeFotoGuncelle(uyeFotoModel model)
        {
            Uye kayit = db.Uye.Where(d => d.uyeId == model.uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadı!";
                return sonuc;
            }
            if (kayit.uyeFoto != "profil.jpg")
            {
                string yol = System.Web.Hosting.HostingEnvironment.MapPath("~/Dosyalar/"+kayit.uyeFoto);
                if (File.Exists(yol))
                {
                    File.Delete(yol);
                }
            }

            string data = model.fotoData;
            string base64 = data.Substring(data.IndexOf(',') + 1);
            base64 = base64.Trim('\0');
            byte[] imgbytes = Convert.FromBase64String(base64);
            string dosyaAdi = kayit.uyeId + model.fotoUzanti.Replace("image/", ".");
            using (var ms= new MemoryStream(imgbytes,0,imgbytes.Length))
            {
                Image img = Image.FromStream(ms,true);
                img.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/Dosyalar/" + dosyaAdi));
            }
            kayit.uyeFoto = dosyaAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Fotoğraf Güncellendi";
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

        [HttpGet]
        [Route("api/sepeturunsayisi/{uyeId}")]
        public int SepetUrunSayisi(int uyeId)
        {
            int toplam = db.Sepet.Count(d => d.uyeId == uyeId);
            return toplam;
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

        #region Yorum
        [HttpGet]
        [Route("api/yorumlistebyilacid/{ilacId}")]
        public List<YorumModel> YorumListeByIlacId(int ilacId)
        {
            List<YorumModel> liste = db.Yorum.Where(d => d.ilacId == ilacId).Select(s => new YorumModel()
            {
                yorumId = s.yorumId,
                ilacId = s.ilacId,
                uyeId = s.uyeId,
                Tarih = s.Tarih,
                uyeKullaniciAdi = s.Uye.uyeKullaniciAdi,
                yorumIcerik = s.yorumIcerik
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/yorumlistebyuyeid/{uyeId}")]
        public List<YorumModel> YorumListeByUyeId(int uyeId)
        {
            List<YorumModel> liste = db.Yorum.Where(d => d.uyeId == uyeId).Select(s => new YorumModel()
            {
                yorumId = s.yorumId,
                ilacId = s.ilacId,
                uyeId = s.uyeId,
                Tarih = s.Tarih,
                uyeKullaniciAdi = s.Uye.uyeKullaniciAdi,
                yorumIcerik = s.yorumIcerik
            }).ToList();
            return liste;
        }
        [HttpPost]
        [Route("api/yorumekle")]
        public SonucModel YorumEkle(YorumModel model)
        {
            if (db.Yorum.Count(d=> d.ilacId == model.ilacId && d.yorumIcerik == model.yorumIcerik) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Aynı Yorumu Aynı Ürüne sadece bir kez yapabilirsiniz!";
                return sonuc;
            }
            Yorum kayit = new Yorum();
            kayit.ilacId = model.ilacId;
            kayit.uyeId = model.uyeId;
            kayit.Tarih = model.Tarih;
            kayit.yorumIcerik = model.yorumIcerik;
            db.Yorum.Add(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Yorum Eklendi!";
            return sonuc;
        }
        [HttpPut]
        [Route("api/yorumduzenle")]
        public SonucModel YorumDuzenle(YorumModel model)
        {
            Yorum kayit = db.Yorum.Where(d => d.yorumId == model.yorumId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Yorum Bulunamadı!";
                return sonuc;
            }
            kayit.yorumId = model.yorumId;
            kayit.yorumIcerik = model.yorumIcerik;
            kayit.Tarih = model.Tarih;
            kayit.ilacId = model.ilacId;
            kayit.uyeId = model.uyeId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Yorum Düzenlendi!";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/yorumsil/{yorumId}")]
        public SonucModel YorumSil(int yorumId)
        {
            Yorum kayit = db.Yorum.Where(d => d.yorumId == yorumId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Yorum Bulunamadı!";
                return sonuc;
            }
            db.Yorum.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Yorum Silindi!";
            return sonuc;
        }
        #endregion

        #region Begen
        [HttpGet]
        [Route("api/begenlistebyuyeid/{uyeId}")]
        public List<BegenModel> BegenListeByUyeId(int uyeId)
        {
            List<BegenModel> liste = db.Begen.Where(d => d.uyeId == uyeId).Select(s => new BegenModel()
            {
                begenId = s.begenId,
                uyeId = s.uyeId,
                ilacId = s.ilacId
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.ilacBilgi = IlacById(kayit.ilacId);
            }
            return liste;
        }
        [HttpPost]
        [Route("api/begenkontrol")]
        public bool BegenKontrol(BegenModel model)
        {
            if (db.Begen.Count(d => d.uyeId == model.uyeId && d.ilacId == model.ilacId) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/begenekle")]
        public SonucModel BegenEkle(BegenModel model)
        {
            if (db.Begen.Count(d => d.uyeId == model.uyeId && d.ilacId == model.ilacId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "İlacı Zaten Beğendiniz!";
                return sonuc;
            }
            Begen kayit = new Begen();
            kayit.ilacId = model.ilacId;
            kayit.uyeId = model.uyeId;
            db.Begen.Add(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Beğendiniz!";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/begentemizle/{ilacId}")]
        public SonucModel BegenTemizle(int ilacId)
        {
            List<Begen> liste = db.Begen.Where(d => d.ilacId == ilacId).ToList();

            foreach (var item in liste)
            {
                db.Begen.Remove(item);
            }
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Beğeniler temizlendi!";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/begensil/{begenId}")]
        public SonucModel BegenSil(int begenId)
        {
            Begen kayit = db.Begen.Where(d => d.begenId == begenId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "İlaç Beğendiklerinizde Bulunamadı!";
                return sonuc;
            }
            db.Begen.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "İlaç Beğendiklerinizden Silindi!";
            return sonuc;
        }
        #endregion
    }
}
