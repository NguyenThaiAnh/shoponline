using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ChiTietHD
    {
        public string ID { get; set; }

        public string TenMH { get; set; }
        public string IDChiTietMatHang { get; set; }
        public string IDHoaDon { get; set; }
        public Nullable<int> SoLuong { get; set; }
    }
}