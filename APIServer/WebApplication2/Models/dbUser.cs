using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class dbUser
    {
        public string ID { get; set; }
        public string Ten { get; set; }
        public string Email { get; set; }
        public int SDT { get; set; }
        public string DiaChi { get; set; }
        public string IDAccount { get; set; }

        public string UserName { get; set; }
        public string PassWord { get; set; }

    }
}