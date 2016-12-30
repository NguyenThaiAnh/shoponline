using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class dbMatHang
    {
        public string ID { get; set; }
        public string TenMH { get; set; }
        public string IDLoaiHang { get; set; }
        public string IDSubLoaiHang { get; set; }
        public string URLHinhAnh1 { get; set; }
        public string URLHinhAnh2 { get; set; }
        public string URLHinhAnh3 { get; set; }
    }
}