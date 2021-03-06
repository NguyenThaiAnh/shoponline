﻿using System;
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

        /// <summary>
        /// lay danh sach cac mat hang ban chay
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("MatHangBanChay")]
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

        /// <summary>
        /// lay danh sach cac mat hang theo tu khoa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("MatHang")]
        public List<listMatHang> tukhoa(string search)
        {
            try
            {
                return Service.timkiem(search);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));

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
        /// lay danh sach tat ca hoa don
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Manager")]
        [Route("HoaDon")]
        public List<HoaDonKH> listHD()
        {
            List<HoaDonKH> result = new List<HoaDonKH>();
            try
            {
                result = Service.listallHoaDon();
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
        [Route("HoaDon")]
        public List<HoaDonKH> inforHD(string idkhachhang)
        {
            List<HoaDonKH> result = new List<HoaDonKH>();
            try
            {
                result = Service.listHoaDon(idkhachhang);
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
        [Authorize(Roles = "Manager")]
        [HttpGet]
        [Route("HoaDon")]
        public List<CTHD> chiTiet(string id)
        {
            List<CTHD> result = new List<CTHD>();
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
        [Route("HoaDon")]
        public bool themHD(itemHoaDon _input)
        {
            try
            {
                Service.themHD(_input);
                return true;
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, false));
            }
        }

        #endregion

        #region PUT

        [Authorize(Roles = "Manager")]
        [HttpPut]
        [Route("HoaDon")]
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
        /// Lay danh sach khach hang
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("NguoiDung")]
        public List<dbUser> getlistCusInfor()
        {
            List <dbUser> result = Service.getlistCusInfor();
            if (result != null)
            {
                return result;
            }
            else
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, "no user"));
        }

        /// <summary>
        /// Lay chi tiet 1 account cu the
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("NguoiDung")]
        public AccInfor getAccInfor(string Username, string Password)
        {
            AccInfor result = Service.getAccInfor(Username, Password);
            if (result != null)
            {
                return result;
            }
            else
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, "invalid account"));
        }

        //Lay ten cua nguoi dung
        [HttpGet]
        [Route("NguoiDung")]
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
        [Route("NguoiDung")]
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
                kh.IDAccount = input.UserName;
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
        /// sua user chi sua duoc password, ten dia chi, email, dia chi, sdt
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("NguoiDung")]
        public bool suaUser(dbUser input)
        {
            try
            {
                ACCOUNT acc = new ACCOUNT();
                KHACHHANG kh = new KHACHHANG();
                db.ACCOUNTs.Find(input.UserName);
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

        #endregion

        //----------------------------------------------------//


    }
}
