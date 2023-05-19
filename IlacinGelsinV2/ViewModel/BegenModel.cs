using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IlacinGelsinV2.ViewModel
{
    public class BegenModel
    {
        public int begenId { get; set; }
        public int uyeId { get; set; }
        public int ilacId { get; set; }
        public IlacModel ilacBilgi { get; set; }
    }
}