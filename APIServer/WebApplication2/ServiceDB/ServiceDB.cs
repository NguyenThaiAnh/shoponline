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
using Twilio;

namespace WebApplication2.ServiceDB
{
    public class Service
    {
        SHOPEntities db = new SHOPEntities();
        private string AccountSid = "ACb38aaa20d08ac4a9bfcd15614e4d7a7f";
        private string AuthToken = "66f0883bcabaa29356549da6788f86bb";


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

        public List<ChiTietHD> chiTietHD(string idHD)
        {
            List<ChiTietHD> result = new List<ChiTietHD>();
            var q = (from e in db.CHITIETHOADONs
                     join k in db.CHITIETMATHANGs on e.IDChiTietMatHang equals k.IDCTMH
                     join l in db.MATHANGs on k.IDMatHang equals l.IDMATHANG
                     where e.IDHoaDon == idHD
                     select new
                     {
                         id = e.IDCTHD,
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
                hd.IDHOADON = _hd.ID;
                hd.IDKhachHang = _hd.IDKhachHang;
                hd.Ngay = _hd.Ngay;
                hd.TinhTrang = _hd.TinhTrang;
                hd.TongTien = _hd.TongTien;
                db.HOADONs.Add(hd);
                db.SaveChanges();
                var twilio = new TwilioRestClient(AccountSid, AuthToken);

                twilio.SendMessage("+13473345984", "+84963225084", "KH:" + _hd.IDKhachHang + "tong tien:" + _hd.TongTien);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
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


        //Tao tk hoac dang nhap tu thong tin tk fb
        public int taotkfb(string displayName, string fbId, string displayEmail,
                            string Gender, string dateOfBirth, string Phone, string Email)
        {
            
            ACCOUNT acc = new ACCOUNT();
            acc = db.ACCOUNTs.Find(fbId);
            if (acc != null)
                return 2;
            else
            {
                acc = new ACCOUNT();
                acc.Username = fbId;
                acc.Password = fbId;
                acc.IDType = "TYP03";
                db.ACCOUNTs.Add(acc);
                db.SaveChanges();
                KHACHHANG kh = new KHACHHANG();
                kh.IDAccount = fbId;
                kh.Ten = displayName;
                kh.Email = Email;
                kh.SDT = Convert.ToInt32(Phone);
                db.KHACHHANGs.Add(kh);
                db.SaveChanges();
                return 1;
            }
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
                         id = k.ID,
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
                tmp.ID = h[i].id.ToString();
                tmp.soluong = Convert.ToInt32(h[i].total);
                var k = (from e in db.MATHANGs
                         where e.ID.ToString() == tmp.ID
                         select new
                         {
                             loai = e.IDLoaiHang,
                             subloai = e.IDSubLoaiHang,
                             ten = e.TenMH,
                             url1 = e.URLHinhAnh1,
                             url2 = e.URLHinhAnh2,
                             url3 = e.URLHinhAnh3
                         }).ToList();

                tmp.Loai = k[0].loai;
                tmp.SubLoai = k[0].subloai;
                tmp.Ten = k[0].ten;
                tmp.URLHinhAnh1 = k[0].url1;
                tmp.URLHinhAnh2 = k[0].url2;
                tmp.URLHinhAnh3 = k[0].url3;
                result.Add(tmp);
            }
            result.OrderByDescending(m => m.soluong);
            return result;
        }

        public List<listMatHang> timkiem(string search)
        {
            string lower = search.ToLower();
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
            for(int i = 0; i < m.Count; i++)
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