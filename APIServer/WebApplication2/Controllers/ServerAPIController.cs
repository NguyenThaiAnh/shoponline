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

    [RoutePrefix("api/v1")]

    public class ServerAPIController : ApiController
    {
        private SHOPEntities db = new SHOPEntities();
        private ServiceDB.Service Service = new ServiceDB.Service();

        //------------->Quan ly loai va sub loai<-------------//
        #region GET
        /// <summary>
        /// lay danh sach cac loai theo gioi tinh
        /// </summary>
        /// <param name="gioitinh"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Loai")]
        public List<string> getlistloai(string gioitinh)
        {
            try
            {
                return Service.listLoai(gioitinh);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));
            }
        }

        /// <summary>
        /// lay danh sach cac sub loai theo theo loai va gioi tinh
        /// </summary>
        /// <param name="loai, gioitinh"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("SubLoai")]
        public List<string> getlistsubloai(string loai, string gioitinh)
        {
            try
            {
                return Service.listsubLoai(loai, gioitinh);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));
            }
        }
        #endregion
        //----------------------------------------------------//



        //--------------->Quan ly mat hang<-------------------//

        #region GET

        /// <summary>
        /// lay danh sach mat hang (khong lay chi tiet)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("MatHang")]
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
        /// lay danh sach mat hang theo gioi tinh
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("MatHang")]
        public List<listMatHang> listMH(string gioitinh)
        {
            List<listMatHang> result = new List<listMatHang>();
            try
            {
                result = Service.getListMatHang(gioitinh);
                return result;
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString()));
            }
        }

        /// <summary>
        /// lay danh sach mat hang theo loai hang
        /// </summary>
        /// <param name="loai"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("MatHang")]
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
        /// Lay danh sach mat hang theo loai va sub loai
        /// </summary>
        /// <param name="loai"></param>
        /// <param name="subloai"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("MatHang")]
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

        /// <summary>
        /// lay chi tiet 1 mat hang cu the
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("MatHang")]
        public itemMatHang infor(string id)
        {
            itemMatHang result = new itemMatHang();
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

        #endregion

        #region POST

        /// <summary>
        /// Them mat hang
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [Route("MatHang")]
        public bool themMH(itemMatHang input)
        {
            try
            {
                return Service.addItem(input);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));
            }
        }

        #endregion

        #region PUT

        /// <summary>
        /// Sua mat hang(cac truong khac sua nhung khong duoc sua lai id cua mat hang do)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [HttpPut]
        [Route("MatHang")]
        public bool suaMH(itemMatHang input)
        {
            try
            {
                return Service.updateItem(input);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));
            }
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Xoa mat hang
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [HttpDelete]
        [Route("MatHang")]
        public bool xoaMH(string ID)
        {
            try
            {
                return Service.deleteItem(ID);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));
            }
        }

        #endregion

        //----------------------------------------------------//




        //--------------->Quan ly hoa don<--------------------//

        #region GET

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

        #endregion

        #region POST

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

        #endregion

        #region PUT

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

        #endregion

        //----------------------------------------------------//


        //--------------->Quan ly tai khoan<--------------------//

        #region GET

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

        [Authorize(Roles = "Manager")]
        [HttpGet]
        [Route("user")]
        public string inforUser(string username)
        {
            try
            {
                var q = (from e in db.ACCOUNTADMINs
                         where e.Account == username
                         select new
                         {
                             Ten = e.Ten
                         }).FirstOrDefault();
                return q.Ten;
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));

            }

        }

        #endregion

        #region POST

        /// <summary>
        /// them user 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
                kh.IDKHACHHANG = input.ID;
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

        #endregion

        #region

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

        [HttpPost]
        [Route("themtkfb")]
        public bool themtk(string displayName, string fbId, string displayEmail,
                            string Gender, string dateOfBirth, string Phone, string Email)
        {
            try
            {
                Service.taotkfb(displayName, fbId, displayEmail, Gender, dateOfBirth, Phone, Email);
                return true;
            }
            catch(Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));

            }
        }


        #endregion

        //----------------------------------------------------//

        #region api thong ke va tim kiem

        [HttpGet]
        [Route("doanhsomathang")]
        public List<sellingItem> thutumathang()
        {
            try
            {
                return Service.mathangchaynhat();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));

            }
        }

        [HttpGet]
        [Route("timkiem")]
        public List<listMatHang> result(string search)
        {
            try
            {
                return Service.timkiem(search);
            }
            catch(Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));

            }
        }
        #endregion



    }
}
