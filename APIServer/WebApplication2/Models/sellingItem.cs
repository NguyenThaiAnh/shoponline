using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class sellingItem
    {
        public string ID { get; set; }
        public string Loai { get; set; }
        public string SubLoai { get; set; }
        public string Ten { get; set; }
        public string URLHinhAnh1 { get; set; }
        public string URLHinhAnh2 { get; set; }
        public string URLHinhAnh3 { get; set; }
        public string GiaMoi { get; set; }
        public string GiaCu { get; set; }
        public int soluong { get; set; }
    }
}