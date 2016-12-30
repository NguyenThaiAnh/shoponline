using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class itemMatHang
    {
        public string ID { get; set; }
        public string IDMatHang { get; set; }
        public string MoTa { get; set; }
        public string MauSac { get; set; }
        public string Size { get; set; }
        public Nullable<int> Gia { get; set; }
        public string Loai { get; set; }

        public string SubLoai { get; set; }

        public Nullable<int> SoLuong { get; set; }

    }
}