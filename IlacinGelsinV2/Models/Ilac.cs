//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IlacinGelsinV2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ilac
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ilac()
        {
            this.Sepet = new HashSet<Sepet>();
        }
    
        public int ilacId { get; set; }
        public string ilacAdi { get; set; }
        public string ilacAciklama { get; set; }
        public Nullable<int> ilacFiyat { get; set; }
        public string ilacFoto { get; set; }
        public int ilacKatId { get; set; }
    
        public virtual Kategori Kategori { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sepet> Sepet { get; set; }
    }
}
