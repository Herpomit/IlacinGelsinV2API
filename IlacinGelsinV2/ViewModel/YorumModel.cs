using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IlacinGelsinV2.ViewModel
{
    public class YorumModel
    {
        public int yorumId { get; set; }
        public string yorumIcerik { get; set; }
        public int uyeId { get; set; }
        public int ilacId { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public string uyeKullaniciAdi { get; set; }
    }
}