using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class HoaDonKH
    {
        public string ID { get; set; }
        public string IDKhachHang { get; set; }
        public System.DateTime Ngay { get; set; }
        public int TongTien { get; set; }
        public string TinhTrang { get; set; }
    }
}