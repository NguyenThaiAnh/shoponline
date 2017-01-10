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


        //Kiem tra tai khoan 
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
                                 id = f.IDKHACHHANG,
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

        //lay danh sach cac loai hang (khong lay chi tiet)
        public List<listMatHang> getListMatHang()
        {
            List<listMatHang> result = new List<listMatHang>();
            var q = (from e in db.MATHANGs
                     join k in db.LOAIHANGs on e.IDLoaiHang equals k.IDLOAIHANG
                     join m in db.SUBLOAIHANGs on e.IDSubLoaiHang equals m.IDSUBLH
                     select new
                     {
                         id = e.IDMATHANG,
                         Loai = k.TenLoai,
                         SubLoai = m.TenLoai,
                         Ten = e.TenMH,
                         url1 = e.URLHinhAnh1,
                         url2 = e.URLHinhAnh2,
                         url3 = e.URLHinhAnh3,
                         giacu = e.GiaCu,
                         giamoi = e.GiaMoi
                     }).ToList();
            for (int i = 0; i < q.Count; i++)
            {
                listMatHang item = new listMatHang();
                item.ID = q[i].id;
                item.Loai = q[i].Loai;
                item.SubLoai = q[i].SubLoai;
                item.Ten = q[i].Ten;
                item.URLHinhAnh1 = q[i].url1;
                item.URLHinhAnh2 = q[i].url2;
                item.URLHinhAnh3 = q[i].url3;
                item.GiaCu = q[i].giacu.ToString();
                item.GiaMoi = q[i].giamoi.ToString();
                result.Add(item);
            }

            return result;
        }

        //lay danh sach cac loai hang theo gioi tinh (khong lay chi tiet)
        public List<listMatHang> getListMatHang(string gioitinh)
        {
            List<listMatHang> result = new List<listMatHang>();
            var q = (from e in db.MATHANGs
                     join k in db.LOAIHANGs on e.IDLoaiHang equals k.IDLOAIHANG
                     join m in db.SUBLOAIHANGs on e.IDSubLoaiHang equals m.IDSUBLH
                     where e.LOAIHANG.Gender == gioitinh
                     select new
                     {
                         Id = e.IDMATHANG,
                         Loai = k.TenLoai,
                         SubLoai = m.TenLoai,
                         Ten = e.TenMH,
                         url1 = e.URLHinhAnh1,
                         url2 = e.URLHinhAnh2,
                         url3 = e.URLHinhAnh3,
                         MoTa = e.MoTa,
                         GiaCu = e.GiaCu,
                         GiaMoi = e.GiaMoi
                     }).ToList();
            for (int i = 0; i < q.Count; i++)
            {
                listMatHang item = new listMatHang();
                item.ID = q[i].Id;
                item.Loai = q[i].Loai;
                item.SubLoai = q[i].SubLoai;
                item.Ten = q[i].Ten;
                item.URLHinhAnh1 = q[i].url1;
                item.URLHinhAnh2 = q[i].url2;
                item.URLHinhAnh3 = q[i].url3;
                item.MoTa = q[i].MoTa;
                if (q[i].GiaCu.ToString() == "0")
                {
                    item.GiaCu = "";
                }
                else
                {
                    item.GiaCu = q[i].GiaCu.ToString();
                }
                item.GiaMoi = q[i].GiaMoi.ToString();
                result.Add(item);
            }

            return result;
        }

        //xoa mat hang
        public bool deleteItem(string ID)
        {
            //Sao chep du lieu vao BACKLOG                          
            if (db.MATHANGs.Find(ID) != null)
            {
                MATHANG tmp1 = db.MATHANGs.Find(ID);
                MATHANG_BACKLOG tmp2 = new MATHANG_BACKLOG();

                tmp2.IDMATHANG = ID;
                tmp2.TenMH = tmp1.TenMH;
                tmp2.MoTa = tmp1.MoTa;
                tmp2.IDLoaiHang = tmp1.IDLoaiHang;
                tmp2.IDSubLoaiHang = tmp1.IDSubLoaiHang;
                tmp2.URLHinhAnh1 = tmp1.URLHinhAnh1;
                tmp2.URLHinhAnh2 = tmp1.URLHinhAnh2;
                tmp2.URLHinhAnh3 = tmp1.URLHinhAnh3;
                tmp2.GiaMoi = tmp1.GiaMoi;
                tmp2.GiaCu = tmp1.GiaCu;

                db.MATHANG_BACKLOG.Add(tmp2);
                db.SaveChanges();
            }

            var q = (from e in db.CHITIETMATHANGs
                     where ID == e.IDMatHang
                     select new
                     {
                         IDCTMH = e.IDCTMH,
                     }).ToList();
            for (int i=0; i<q.Count; i++)
            {
                if (db.CHITIETMATHANGs.Find(q[i].IDCTMH) != null)
                {
                    CHITIETMATHANG tmp = db.CHITIETMATHANGs.Find(q[i].IDCTMH);
                    CHITIETMATHANG_BACKLOG tmp2 = new CHITIETMATHANG_BACKLOG();
                    tmp2.IDMatHang = ID;
                    tmp2.IDCTMH = tmp.IDCTMH;
                    tmp2.Size = tmp.Size;
                    tmp2.Gia = tmp.Gia;
                    tmp2.SoLuong = tmp.SoLuong;

                    db.CHITIETMATHANG_BACKLOG.Add(tmp2);
                    db.SaveChanges();

                    db.CHITIETMATHANGs.Remove(tmp);
                    db.SaveChanges();
                }
                
            }

            if (db.MATHANGs.Find(ID) != null)
            {
                MATHANG tmp = db.MATHANGs.Find(ID);
                db.MATHANGs.Remove(tmp);
                db.SaveChanges();
            }

            return true;
        }

        //lay danh sach tat ca hoa don
        internal List<HoaDonKH> listallHoaDon()
        {
            List<HoaDonKH> tmp = new List<HoaDonKH>();
            var q = (from e in db.HOADONs
                     join k in db.KHACHHANGs on e.IDKhachHang equals k.IDKHACHHANG
                     select new
                     {
                         ID = e.IDHOADON,
                         IDKhachHang = e.IDKhachHang,
                         TenKH = k.Ten,
                         Ngay = e.Ngay,
                         TongTien = e.TongTien,
                         TinhTrang = e.TinhTrang
                     }).ToList();
            
            for (int i=0; i<q.Count; i++)
            {
                HoaDonKH tmp2 = new HoaDonKH();
                tmp2.ID = q[i].ID;
                tmp2.IDKhachHang = q[i].IDKhachHang;
                tmp2.TenKH = q[i].TenKH;
                tmp2.TongTien = q[i].TongTien;
                tmp2.TinhTrang = q[i].TinhTrang;
                tmp.Add(tmp2);
            }

            return tmp;
        }

        //cap nhat mat hang bao gom cap nhat chi tiet 
        public bool updateItem(itemMatHang input)
        {
            var q = (from e in db.LOAIHANGs
                        join k in db.SUBLOAIHANGs on e.IDLOAIHANG equals k.IDLoaiHang
                        where input.Loai == e.TenLoai && input.SubLoai == k.TenLoai && input.GioiTinh == e.Gender
                        select new
                        {
                            IDLH = e.IDLOAIHANG,
                            IDSUBLH = k.IDSUBLH
                        }).FirstOrDefault();

            MATHANG tmp = db.MATHANGs.Find(input.ID);
            tmp.IDLoaiHang = q.IDLH;
            tmp.IDSubLoaiHang = q.IDSUBLH;
            tmp.TenMH = input.Ten;
            tmp.URLHinhAnh1 = input.URLHinhAnh1;
            tmp.URLHinhAnh2 = input.URLHinhAnh2;
            tmp.URLHinhAnh3 = input.URLHinhAnh3;
            tmp.GiaCu = int.Parse(input.GiaCu);
            tmp.GiaMoi = int.Parse(input.GiaMoi);

            db.SaveChanges();

            for (int i = 0; i < input.Items.Count; i++)
            {
                CHITIETMATHANG tmp1 = db.CHITIETMATHANGs.Find(input.Items[i].ID);
                tmp1.Size = input.Items[i].Size;
                tmp1.Gia = int.Parse(input.Items[i].Gia);
                tmp1.SoLuong = int.Parse(input.Items[i].SoLuong);

                db.SaveChanges();

            }

            return true;
        }

        //them mat hang bao gom them chi tiet 
        public bool addItem(itemMatHang input)
        {
            string idMatHang;
            var q = (from e in db.LOAIHANGs
                     join k in db.SUBLOAIHANGs on e.IDLOAIHANG equals k.IDLoaiHang
                     where input.Loai == e.TenLoai && input.SubLoai == k.TenLoai && input.GioiTinh == e.Gender
                     select new
                     {
                         IDLH = e.IDLOAIHANG,
                         IDSUBLH = k.IDSUBLH
                     }).FirstOrDefault();

            MATHANG tmp = new MATHANG();
            tmp.IDLoaiHang = q.IDLH;
            tmp.IDSubLoaiHang = q.IDSUBLH;
            tmp.TenMH = input.Ten;
            tmp.URLHinhAnh1 = input.URLHinhAnh1;
            tmp.URLHinhAnh2 = input.URLHinhAnh2;
            tmp.URLHinhAnh3 = input.URLHinhAnh3;
            tmp.GiaCu = int.Parse(input.GiaCu);
            tmp.GiaMoi = int.Parse(input.GiaMoi);
            
            db.MATHANGs.Add(tmp);
            db.SaveChanges();
            db.Entry(tmp).GetDatabaseValues();
            idMatHang = tmp.IDMATHANG;

            for (int i = 0; i < input.Items.Count; i++)
            {
                CHITIETMATHANG tmp1 = new CHITIETMATHANG();
                tmp1.IDMatHang = idMatHang;
                tmp1.Size = input.Items[i].Size;
                tmp1.Gia = int.Parse(input.Items[i].Gia);
                tmp1.SoLuong = int.Parse(input.Items[i].SoLuong);

            
                db.CHITIETMATHANGs.Add(tmp1);
                db.SaveChanges();
            }
            return true;
        }

        public List<dbUser> getlistCusInfor()
        {
            List<dbUser> result = new List<dbUser>();
            var q = (from e in db.KHACHHANGs
                     //join k in db.LOAIHANGs on e.IDLoaiHang equals k.IDLOAIHANG
                     //join m in db.SUBLOAIHANGs on e.IDSubLoaiHang equals m.IDSUBLH
                     select new
                     {
                         id = e.IDKHACHHANG,
                         user = e.IDAccount,
                         ten = e.Ten,
                         email = e.Email,
                         diachi = e.DiaChi,
                         sdt = e.SDT
                     }).ToList();
            for (int i = 0; i < q.Count; i++)
            {
                dbUser item = new dbUser();
                item.ID = q[i].id;
                item.UserName = q[i].user;
                item.Ten = q[i].ten;
                item.Email = q[i].email;
                item.DiaChi = q[i].diachi;
                item.SDT = q[i].sdt;
                result.Add(item);
            }

            return result;
        }

        //lay chi tiet 1 mat hang voi id cu the
        public itemMatHang inforItem(string id)
        {
            itemMatHang result = new itemMatHang();
            result.Items = new List<subItemMatHang>();

            var q = (from e in db.MATHANGs
                     where e.IDMATHANG == id
                     join k in db.LOAIHANGs on e.IDLoaiHang equals k.IDLOAIHANG
                     join m in db.SUBLOAIHANGs on e.IDSubLoaiHang equals m.IDSUBLH
                     select new
                     {
                         ID = e.IDMATHANG,
                         Loai = k.TenLoai,
                         SubLoai = m.TenLoai,
                         Ten = e.TenMH,
                         GioiTinh = k.Gender,
                         URLHinhAnh1 = e.URLHinhAnh1,
                         URLHinhAnh2 = e.URLHinhAnh2,
                         URLHinhAnh3 = e.URLHinhAnh3,
                         MoTa = e.MoTa,
                         GiaCu = e.GiaCu,
                         GiaMoi = e.GiaMoi
                    }).ToList();

            var q2 = (from e in db.CHITIETMATHANGs
                      join k in db.MATHANGs on e.IDMatHang equals k.IDMATHANG
                      join m in db.LOAIHANGs on k.IDLoaiHang equals m.IDLOAIHANG
                      where e.IDMatHang == id
                      select new
                      {
                          ID = e.IDCTMH,
                          IDMH = e.IDMatHang,
                          Gia = e.Gia,
                          SoLuong = e.SoLuong,
                          Size = e.Size,
                          Loai = m.TenLoai
                     }).ToList();

            if (q != null)
            {
                result.ID = q[0].ID;
                result.Ten = q[0].Ten;
                result.Loai = q[0].Loai;
                result.SubLoai = q[0].SubLoai;
                result.MoTa = q[0].MoTa;
                result.URLHinhAnh1 = q[0].URLHinhAnh1;
                result.URLHinhAnh2 = q[0].URLHinhAnh2;
                result.URLHinhAnh3 = q[0].URLHinhAnh3;
                result.GiaCu = q[0].GiaCu.ToString();
                result.GiaMoi = q[0].GiaMoi.ToString();
                result.GioiTinh = q[0].GioiTinh;
            }
            for (int i = 0; i < q2.Count; i++)
            {
                subItemMatHang item = new subItemMatHang();
                
                item.ID = q2[i].ID;
                item.IDMatHang = q2[i].IDMH;
                item.Size = q2[i].Size;
                item.Gia = q2[i].Gia.ToString();
                item.SoLuong = q2[i].SoLuong.ToString();
                result.Items.Add(item);
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
                         Id = e.IDHOADON,
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
        public List<CTHD> chiTietHD(string idHD)
        {
            List<CTHD> result = new List<CTHD>();
            var q = (from e in db.CHITIETHOADONs
                     join k in db.CHITIETMATHANGs on e.IDChiTietMatHang equals k.IDCTMH
                     join l in db.MATHANGs on k.IDMatHang equals l.IDMATHANG
                     join m in db.HOADONs on e.IDHoaDon equals m.IDHOADON
                     join n in db.KHACHHANGs on m.IDKhachHang equals n.IDKHACHHANG
                     where e.IDHoaDon == idHD
                     select new
                     {
                         id = e.IDCTHD,
                         idKhachHang = n.IDKHACHHANG,
                         tenMatHang = l.TenMH,
                         size = k.Size,
                         soluong = e.SoLuong,
                         dongia = k.Gia,
                         ngay = m.Ngay,
                         tenKH = n.Ten,
                         tongtien = m.TongTien,
                         tinhtrang = m.TinhTrang
                     }).ToList();
            for (int i = 0; i < q.Count; i++)
            {
                CTHD item = new CTHD();
                item.ID = q[i].id;
                item.IDKhachHang = q[i].idKhachHang;
                item.TenMH = q[i].tenMatHang;
                item.Size = q[i].size;
                item.SoLuong = q[i].soluong.ToString();
                item.DonGia = q[i].dongia.ToString();
                item.Ngay = q[i].ngay;
                item.TenKH = q[i].tenKH;
                item.TongTien = q[i].tongtien.ToString();
                item.TinhTrang = q[i].tinhtrang;
                result.Add(item);
            }
            return result;
        }

        //them hoa don bao gom them chi tiet hoa don
        public bool themHD(itemHoaDon _hd)
        {
            HOADON hd = new HOADON();
            hd.IDKhachHang = _hd.IDKhachHang;
            hd.Ngay = _hd.Ngay;
            hd.TinhTrang = _hd.TinhTrang;
            hd.TongTien = _hd.TongTien;

            db.HOADONs.Add(hd);
            db.SaveChanges();
            db.Entry(hd).GetDatabaseValues();
            string idHoaDon = hd.IDHOADON;
                
            for(int i=0; i<_hd.Items.Count; i++)
            {
                CHITIETHOADON tmp = new CHITIETHOADON();
                tmp.IDHoaDon = idHoaDon;
                tmp.SoLuong = int.Parse(_hd.Items[i].SoLuong);
                tmp.IDChiTietMatHang = _hd.Items[i].IDChiTietMatHang;
                db.CHITIETHOADONs.Add(tmp);
                db.SaveChanges();
            }

            return true;
            
        }

        //Lay list loai theo gioi tinh
        public List<string> listLoai(string gioitinh)
        {
            List<string> tmp = new List<string>();
            var q = (from e in db.LOAIHANGs
                     where e.Gender == gioitinh
                     select new
                     {
                         Tenloai = e.TenLoai
                     }).ToList();
            for (int i=0; i<q.Count; i++)
            {
                tmp.Add(q[i].Tenloai);
            }

            return tmp;
        }

        //Lay list loai theo gioi tinh
        internal List<string> listsubLoai(string loai, string gioitinh)
        {
            List<string> tmp = new List<string>();
            var q = (from e in db.SUBLOAIHANGs
                     join k in db.LOAIHANGs on e.IDLoaiHang equals k.IDLOAIHANG
                     where k.TenLoai == loai && k.Gender == gioitinh
                     select new
                     {
                         Tenloai = e.TenLoai
                     }).ToList();
            for (int i = 0; i < q.Count; i++)
            {
                tmp.Add(q[i].Tenloai);
            }

            return tmp;
        }

        //them chi tiet hoa don
        public bool themChiTietHD(ChiTietHD _CTHD)
        {
            CHITIETHOADON ct = new CHITIETHOADON();
            ct.IDCTHD = _CTHD.ID;
            ct.IDChiTietMatHang = _CTHD.IDChiTietMatHang;
            ct.IDHoaDon = _CTHD.IDHoaDon;
            ct.SoLuong = int.Parse(_CTHD.SoLuong);
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
                     join m in db.LOAIHANGs on e.IDLoaiHang equals m.IDLOAIHANG
                     join k in db.SUBLOAIHANGs on e.IDSubLoaiHang equals k.IDSUBLH
                     where m.TenLoai == loai
                     select new
                     {
                         id = e.IDMATHANG,
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
                     join m in db.LOAIHANGs on e.IDLoaiHang equals m.IDLOAIHANG
                     join k in db.SUBLOAIHANGs on e.IDSubLoaiHang equals k.IDSUBLH
                     where m.TenLoai == loai && k.TenLoai == subloai
                     select new
                     {
                         id = e.IDMATHANG,
                         loai = m.TenLoai,
                         SubLoai = k.TenLoai,
                         Ten = e.TenMH,
                         url1 = e.URLHinhAnh1,
                         url2 = e.URLHinhAnh2,
                         url3 = e.URLHinhAnh3,
                         MoTa = e.MoTa,
                         GiaCu = e.GiaCu,
                         GiaMoi = e.GiaMoi
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
                tmp.MoTa = q[i].MoTa;
                if (q[i].GiaCu.ToString() == "0")
                {
                    tmp.GiaCu = "";
                }
                else
                {
                    tmp.GiaCu = q[i].GiaCu.ToString();
                }
                tmp.GiaMoi = q[i].GiaMoi.ToString();
                result.Add(tmp);
            }
            return result;

        }

        //Lay danh sach cac mat hang theo thu tu ban chay nhat
        public List<sellingItem> mathangchaynhat()
        {
            List<sellingItem> result = new List<sellingItem>();

            var q = (from e in db.CHITIETHOADONs
                     join m in db.CHITIETMATHANGs on e.IDChiTietMatHang equals m.IDCTMH
                     join k in db.MATHANGs on m.IDMatHang equals k.IDMATHANG
                     select new
                     {
                         id = k.IDMATHANG,
                         loai = k.LOAIHANG,
                         subloai = k.SUBLOAIHANG,
                         ten = k.TenMH,
                         url1 = k.URLHinhAnh1,
                         url2 = k.URLHinhAnh2,
                         url3 = k.URLHinhAnh3,
                         soluong = e.SoLuong
                     }).ToList();
            var h = q.GroupBy(m => m.id)
                    .Select(m => new { id = m.Key, total = m.Sum(v => v.soluong) }).ToList()
                    ;
            for (int i = 0; i < h.Count; i++)
            {
                sellingItem tmp = new sellingItem();
                tmp.ID = h[i].id;
                tmp.soluong = Convert.ToInt32(h[i].total);
                var k = (from e in db.MATHANGs
                         where e.IDMATHANG == tmp.ID
                         select new
                         {
                             loai = e.IDLoaiHang,
                             subloai = e.IDSubLoaiHang,
                             ten = e.TenMH,
                             url1 = e.URLHinhAnh1,
                             url2 = e.URLHinhAnh2,
                             url3 = e.URLHinhAnh3,
                             giamoi = e.GiaMoi,
                             giacu = e.GiaCu
                         }).FirstOrDefault();

                tmp.Loai = k.loai;
                tmp.SubLoai = k.subloai;
                tmp.Ten = k.ten;
                tmp.URLHinhAnh1 = k.url1;
                tmp.URLHinhAnh2 = k.url2;
                tmp.URLHinhAnh3 = k.url3;
                tmp.GiaMoi = k.giamoi.ToString();
                tmp.GiaCu = k.giacu.ToString();
                result.Add(tmp);
            }
            result.OrderByDescending(m => m.soluong);
            return result;
        }

        //tim kiem mat hang 
        public List<listMatHang> timkiem(string search)
        {
            string lower = search;
            lower = string.Join(" ", lower.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            List<listMatHang> result = new List<listMatHang>();

            var q = (from e in db.MATHANGs

                     select new
                     {
                         id = e.IDMATHANG,
                         ten = e.TenMH,
                         url1 = e.URLHinhAnh1,
                         url2 = e.URLHinhAnh2,
                         url3 = e.URLHinhAnh3,
                         giacu = e.GiaCu,
                         giamoi = e.GiaMoi,
                         Mota = e.MoTa
                     }).ToList();

            var m = q.Where(x => x.ten.Contains(lower)).ToList();
            for (int i = 0; i < m.Count; i++)
            {
                listMatHang tmp = new listMatHang();
                tmp.ID = m[i].id;
                tmp.Ten = q[i].ten;
                tmp.URLHinhAnh1 = q[i].url1;
                tmp.URLHinhAnh2 = q[i].url2;
                tmp.URLHinhAnh3 = q[i].url3;
                tmp.MoTa = q[i].Mota;
                if (q[i].giacu.ToString() == "0")
                {
                    tmp.GiaCu = "";
                }
                else
                {
                    tmp.GiaCu = q[i].giacu.ToString();
                }
                tmp.GiaMoi = q[i].giamoi.ToString();
                result.Add(tmp);
            }
            return result;
        }

    }
}