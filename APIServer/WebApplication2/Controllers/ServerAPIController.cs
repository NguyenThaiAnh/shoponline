using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApplication2.Models;
using WebApplication2.ServiceDB;


namespace WebApplication2.Controllers
{

    [RoutePrefix("api/Server")]

    public class ServerAPIController : ApiController
    {
        private SHOPEntities db = new SHOPEntities();
        private ServiceDB.Service Service = new ServiceDB.Service();

        /// <summary>
        /// Lay chi tiet 1 account cu the
        /// </summary>
        /// <param name="UseName"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("accInfor")]

        public AccInfor getAccInfor(string UseName, string PassWord)
        {

            AccInfor result = Service.getAccInfor(UseName, PassWord);
            if (result != null)
            {
                return result;
            }
            else
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, "invalid account"));
        }

        /// <summary>
        /// lay danh sach mat hang(khong lay chi tiet)
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("listMatHang")]
        public List<listMatHang> listMH()
        {
            List<listMatHang> result = new List<listMatHang>();
            try
            {
                result = Service.getListMatHang();
                return result;
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString()));
            }


        }

        /// <summary>
        /// lay chi tiet 1 mat hang cu the
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize(Roles ="Manager")]
        [HttpGet]
        [Route("inforItem")]

        public List<itemMatHang> infor(string id)
        {
            List<itemMatHang> result = new List<itemMatHang>();
            try
            {
                result = Service.inforItem(id);
                return result;
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString()));
            }
        }

        /// <summary>
        /// lay danh sach hoa don cua 1 khach hang
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("inforHoaDon")]
        public List<HoaDonKH> inforHD(string id)
        {
            List<HoaDonKH> result = new List<HoaDonKH>();
            try
            {
                result = Service.listHoaDon(id);
                return result;
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString()));
            }
        }



        /// <summary>
        /// lay chi tiet cua 1 hoa don cu the
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("chiTietHD")]
        public List<ChiTietHD> chiTiet(string id)
        {
            List<ChiTietHD> result = new List<ChiTietHD>();
            try
            {
                result = Service.chiTietHD(id);
                return result;
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString()));
            }
        }

        /// <summary>
        /// Them hoa don cua 1 khach hang vao
        /// 
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("themHD")]
        public bool themHD(HoaDonKH _input)
        {
            try
            {
                Service.themHD(_input);
                return true;
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));
            }
        }

        /// <summary>
        /// Them chi tiet hoa don cu the
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("themCTHD")]
        public bool themCTHD(ChiTietHD _input)
        {
            try
            {
                Service.themChiTietHD(_input);
                return true;
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));
            }
        }

        /// <summary>
        /// lay danh sach mat hang theo loai hang
        /// </summary>
        /// <param name="loai"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getListLoai")]
        public List<listMatHang> getlist(string loai)
        {
            try
            {
                return Service.listLoaiHang(loai);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));
            }
        }

        /// <summary>
        /// Lay danh sach mat hang tuloai va sub loai
        /// </summary>
        /// <param name="loai"></param>
        /// <param name="subloai"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getListSubLoai")]
        public List<listMatHang> getlist(string loai, string subloai)
        {
            try
            {
                return Service.listSubLoaiHang(loai, subloai);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));
            }
        }

        [HttpPost]
        [Route("themMatHang")]

        public bool themMH(dbMatHang input)
        {
            try
            {
                MATHANG tmp = new MATHANG();
                tmp.ID = input.ID;
                tmp.IDLoaiHang = input.IDLoaiHang;
                tmp.IDSubLoaiHang = input.IDSubLoaiHang;
                tmp.TenMH = input.TenMH;
                tmp.URLHinhAnh1 = input.URLHinhAnh1;
                tmp.URLHinhAnh2 = input.URLHinhAnh2;
                tmp.URLHinhAnh3 = input.URLHinhAnh3;
                db.MATHANGs.Add(tmp);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));
            }

        }

        /// <summary>
        /// Sua mat hang(cac truong khac sua nhung khong duoc sua lai id cua mat hang do)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("SuaMatHang")]

        public bool suaMH(dbMatHang input)
        {
            try
            {
                MATHANG tmp = db.MATHANGs.Find(input.ID);
                tmp.IDLoaiHang = input.IDLoaiHang;
                tmp.IDSubLoaiHang = input.IDSubLoaiHang;
                tmp.TenMH = input.TenMH;
                tmp.URLHinhAnh1 = input.URLHinhAnh1;
                tmp.URLHinhAnh2 = input.URLHinhAnh2;
                tmp.URLHinhAnh3 = input.URLHinhAnh3;

                db.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));
            }
        }

        [Authorize]
        [HttpPut]
        [Route("SuaHoaDon")]
        public bool suaHD(string id, string status)
        {
            try
            {
                HOADON tmp = new HOADON();
                tmp = db.HOADONs.Find(id);
                tmp.TinhTrang = status;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));

            }

        }

        [HttpPost]
        [Route("themUser")]
        public bool themUser(dbUser input)
        {
            try
            {
                ACCOUNT acc = new ACCOUNT();
                KHACHHANG kh = new KHACHHANG();
                acc.Username = input.UserName;
                acc.Password = input.PassWord;
                acc.IDType = "TYP03";
                db.ACCOUNTs.Add(acc);
                db.SaveChanges();
                kh.DiaChi = input.DiaChi;
                kh.Email = input.Email;
                kh.ID = input.ID;
                kh.IDAccount = input.IDAccount;
                kh.SDT = input.SDT;
                kh.Ten = input.Ten;
                db.KHACHHANGs.Add(kh);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));

            }

        }

        /// <summary>
        /// sua user chi sua duoc password, ten dia chi, email
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("suaUser")]
        public bool suaUser(dbUser input)
        {
            try
            {
                ACCOUNT acc = new ACCOUNT();
                KHACHHANG kh = new KHACHHANG();
                db.ACCOUNTs.Find(input.IDAccount);
                db.KHACHHANGs.Find(input.ID);
                acc.Password = input.PassWord;
                db.SaveChanges();
                kh.SDT = input.SDT;
                kh.Ten = input.Ten;
                kh.DiaChi = input.DiaChi;
                kh.Email = input.Email;
                db.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));

            }
        }

    }
}
