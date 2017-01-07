using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class itemMatHang:listMatHang
    {
        public List<subItemMatHang> Items { get; set; }
    }
}