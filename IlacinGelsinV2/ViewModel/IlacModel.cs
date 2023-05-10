using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IlacinGelsinV2.ViewModel
{
    public class IlacModel
    {
        public int ilacId { get; set; }
        public string ilacAdi { get; set; }
        public string ilacAciklama { get; set; }
        public Nullable<int> ilacFiyat { get; set; }
        public string ilacFoto { get; set; }
        public int ilacKatId { get; set; }
    }
}