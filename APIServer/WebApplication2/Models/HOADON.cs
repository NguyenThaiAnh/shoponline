//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HOADON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOADON()
        {
            this.CHITIETHOADONs = new HashSet<CHITIETHOADON>();
        }
    
        public int ID { get; set; }
        public string IDHOADON { get; set; }
        public string IDKhachHang { get; set; }
        public System.DateTime Ngay { get; set; }
        public int TongTien { get; set; }
        public string TinhTrang { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETHOADON> CHITIETHOADONs { get; set; }
        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}
