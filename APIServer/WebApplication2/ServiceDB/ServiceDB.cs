using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication2.Models;


namespace WebApplication2.ServiceDB
{
    public class Service
    {
        SHOPEntities db = new SHOPEntities();



        public bool checkAcc(string _userName, string _passWord)
        {
            var q = (from e in db.ACCOUNTs
                     where e.Username == _userName
                     select new
                     {
                         name = e.Username,
                         pass = e.Password
                     }).ToList();
            if (q.Count == 0)
                return false;
            else
                if (q[0].pass != _passWord)
                return false;
            return true;
        }


        //Cai nay phai dieu chinh lai dataBase de dieu chinh lai khoa ngoai giua KHACHHANG, ADMIN toi ACCOUNT. Thieu mat IDACCOUT
        // lay inforAccount
        public AccInfor getAccInfor(string _userName, string _passWord)
        {
            AccInfor result = new AccInfor();
            if (checkAcc(_userName, _passWord) == false)
                return null;
            #region if have exist account
            else
            {

                var q = (from e in db.ACCOUNTs
                         where e.Username == _userName && e.Password == _passWord
                         select new
                         {
                             useName = e.Username,
                             passWord = e.Password,
                             idType = e.IDType
                         }).ToList();
                result.Username = q[0].useName;
                result.Password = q[0].passWord;
                result.IDType = q[0].idType;
                if (result.IDType == "TYP01")
                {
                    var r = (from e in db.ACCOUNTs
                             join f in db.ACCOUNTADMINs on e.Username equals f.Account
                             where e.Username == _userName && e.Password == _passWord
                             select new
                             {
                                 id = f.ID,
                                 Ten = f.Ten,
                                 Email = f.Email,
                                 SDT = f.SDT
                             }).ToList();
                    result.Ten = r[0].Ten;
                    result.SDT = r[0].SDT;
                    result.Email = r[0].Email;
                    result.ID = r[0].id;
                }
                else
                {
                    var r = (from e in db.ACCOUNTs
                             join f in db.KHACHHANGs on e.Username equals f.IDAccount
                             where e.Username == _userName && e.Password == _passWord
                             select new
                             {
                                 id = f.ID,
                                 Ten = f.Ten,
                                 Email = f.Email,
                                 SDT = f.SDT,
                                 DiaChi = f.DiaChi
                             }).ToList();
                    result.Ten = r[0].Ten;
                    result.SDT = r[0].SDT;
                    result.Email = r[0].Email;
                    result.ID = r[0].id;
                    result.DiaChi = r[0].DiaChi;
                }
            }
            #endregion
            return result;

        }

        //lay danh sach cac loai hang(khong lay chi tiet)

        public List<listMatHang> getListMatHang()
        {
            List<listMatHang> result = new List<listMatHang>();
            var q = (from e in db.MATHANGs
                     join k in db.LOAIHANGs on e.IDLoaiHang equals k.ID
                     select new
                     {
                         id = e.ID,
                         Loai = k.TenLoai,
                         Ten = e.TenMH,
                         url1 = e.URLHinhAnh1,
                         url2 = e.URLHinhAnh2,
                         url3 = e.URLHinhAnh3,

                     }).ToList();
            for (int i = 0; i < q.Count; i++)
            {
                listMatHang item = new listMatHang();
                item.ID = q[i].id;
                item.Loai = q[i].Loai;
                item.Ten = q[i].Ten;
                item.URLHinhAnh1 = q[i].url1;
                item.URLHinhAnh2 = q[i].url2;
                item.URLHinhAnh3 = q[i].url3;
                result.Add(item);
            }

            return result;
        }

        //lay chi tiet 1 mat hang voi id cu the

        public List<itemMatHang> inforItem(string id)
        {
            List<itemMatHang> result = new List<itemMatHang>();
            var q = (from e in db.CHITIETMATHANGs
                     where e.IDMatHang == id
                     select new
                     {
                         id = e.ID,
                         idMH = e.IDMatHang,
                         Loai = e.Loai,
                         Gia = e.Gia,
                         MauSac = e.MauSac,
                         SoLuong = e.SoLuong,
                         Size = e.Size,
                         MoTa = e.MoTa

                     }).ToList();
            for (int i = 0; i < q.Count; i++)
            {
                itemMatHang item = new itemMatHang();
                item.Gia = q[i].Gia;
                item.ID = q[i].id;
                item.IDMatHang = q[i].idMH;
                item.Loai = q[i].Loai;
                item.MauSac = q[i].MauSac;
                item.MoTa = q[i].MoTa;
                item.Size = q[i].Size;
                item.SoLuong = q[i].SoLuong;
                result.Add(item);
            }
            return result;
        }

        //lay danh sach hoa don cua 1 khach hang

        public List<HoaDonKH> listHoaDon(string id)
        {
            List<HoaDonKH> result = new List<HoaDonKH>();
            var q = (from e in db.HOADONs
                     where e.IDKhachHang == id
                     select new
                     {
                         Id = e.ID,
                         Ngay = e.Ngay,
                         TinhTrang = e.TinhTrang,
                         TongTien = e.TongTien
                     }).ToList();
            for (int i = 0; i < q.Count; i++)
            {
                HoaDonKH item = new HoaDonKH();
                item.ID = q[i].Id;
                item.IDKhachHang = id;
                item.Ngay = q[i].Ngay;
                item.TinhTrang = q[i].TinhTrang;
                item.TongTien = q[i].TongTien;
                result.Add(item);
            }
            return result;
        }

        //lay chi tiet cua 1 hoa don

        public List<ChiTietHD> chiTietHD(string idHD)
        {
            List<ChiTietHD> result = new List<ChiTietHD>();
            var q = (from e in db.CHITIETHOADONs
                     join k in db.CHITIETMATHANGs on e.IDChiTietMatHang equals k.ID
                     join l in db.MATHANGs on k.IDMatHang equals l.ID
                     where e.IDHoaDon == idHD
                     select new
                     {
                         id = e.ID,
                         IdChiTietMH = e.IDChiTietMatHang,
                         tenMatHang = l.TenMH,
                         idHD = e.IDHoaDon,
                         soLuong = e.SoLuong
                     }).ToList();
            for (int i = 0; i < q.Count; i++)
            {
                ChiTietHD item = new ChiTietHD();
                item.ID = q[i].id;
                item.IDChiTietMatHang = q[i].IdChiTietMH;
                item.IDHoaDon = q[i].idHD;
                item.SoLuong = q[i].soLuong;
                item.TenMH = q[i].tenMatHang;
                result.Add(item);
            }
            return result;
        }

        //them hoa don
        public bool themHD(HoaDonKH _hd)
        {
            try
            {
                HOADON hd = new HOADON();
                hd.ID = _hd.ID;
                hd.IDKhachHang = _hd.IDKhachHang;
                hd.Ngay = _hd.Ngay;
                hd.TinhTrang = _hd.TinhTrang;
                hd.TongTien = _hd.TongTien;
                db.HOADONs.Add(hd);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //them chi tiet hoa don
        public bool themChiTietHD(ChiTietHD _CTHD)
        {
            CHITIETHOADON ct = new CHITIETHOADON();
            ct.ID = _CTHD.ID;
            ct.IDChiTietMatHang = _CTHD.IDChiTietMatHang;
            ct.IDHoaDon = _CTHD.IDHoaDon;
            ct.SoLuong = _CTHD.SoLuong;
            try
            {
                db.CHITIETHOADONs.Add(ct);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //get list mat hang voi loai xac dinh(VD: Quan)

        public List<listMatHang> listLoaiHang(string loai)
        {
            List<listMatHang> result = new List<listMatHang>();
            var q = (from e in db.MATHANGs
                     join m in db.LOAIHANGs on e.IDLoaiHang equals m.ID
                     join k in db.SUBLOAIHANGs on e.IDSubLoaiHang equals k.ID
                     where m.TenLoai == loai
                     select new
                     {
                         id = e.ID,
                         loai = m.TenLoai,
                         SubLoai = k.TenLoai,
                         Ten = e.TenMH,
                         url1 = e.URLHinhAnh1,
                         url2 = e.URLHinhAnh2,
                         url3 = e.URLHinhAnh3
                     }).ToList();
            for(int i = 0; i < q.Count; i++)
            {
                listMatHang tmp = new listMatHang();
                tmp.ID = q[i].id;
                tmp.Loai = q[i].loai;
                tmp.SubLoai = q[i].SubLoai;
                tmp.Ten = q[i].Ten;
                tmp.URLHinhAnh1 = q[i].url1;
                tmp.URLHinhAnh2 = q[i].url2;
                tmp.URLHinhAnh3 = q[i].url3;
                result.Add(tmp);
            }
            return result;
        }

        //get list mat hang voi loai va subloai duoc chon(VIDU: Quan => Quan caro)
        public List<listMatHang> listSubLoaiHang(string loai, string subloai)
        {
            List<listMatHang> result = new List<listMatHang>();
            var q = (from e in db.MATHANGs
                     join m in db.LOAIHANGs on e.IDLoaiHang equals m.ID
                     join k in db.SUBLOAIHANGs on e.IDSubLoaiHang equals k.ID
                     where m.TenLoai == loai && k.TenLoai == subloai
                     select new
                     {
                         id = e.ID,
                         loai = m.TenLoai,
                         SubLoai = k.TenLoai,
                         Ten = e.TenMH,
                         url1 = e.URLHinhAnh1,
                         url2 = e.URLHinhAnh2,
                         url3 = e.URLHinhAnh3
                     }).ToList();
            for (int i = 0; i < q.Count; i++)
            {
                listMatHang tmp = new listMatHang();
                tmp.ID = q[i].id;
                tmp.Loai = q[i].loai;
                tmp.SubLoai = q[i].SubLoai;
                tmp.Ten = q[i].Ten;
                tmp.URLHinhAnh1 = q[i].url1;
                tmp.URLHinhAnh2 = q[i].url2;
                tmp.URLHinhAnh3 = q[i].url3;
                result.Add(tmp);
            }
            return result;
        }


    }
}