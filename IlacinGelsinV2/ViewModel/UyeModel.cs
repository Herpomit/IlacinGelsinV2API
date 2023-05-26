using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IlacinGelsinV2.ViewModel
{
    public class UyeModel
    {
        public int uyeId { get; set; }
        public string uyeAdsoyad { get; set; }
        public string uyeKullaniciAdi { get; set; }
        public string uyeEposta { get; set; }
        public string uyeSifre { get; set; }
        public Nullable<int> uyeAdmin { get; set; }
        public string uyeFoto { get; set; }
    }
}