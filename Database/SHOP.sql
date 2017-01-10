CREATE DATABASE SHOP;
GO


USE SHOP;
GO

CREATE TABLE CUAHANG(
	ID VARCHAR(3) NOT NULL,
	TenCH NVARCHAR(20) NOT NULL, 
	DiaChi NVARCHAR(50) NOT NULL, 
	DiaChiGMap NVARCHAR(20), 
	SDT VARCHAR(20),
	PRIMARY KEY (ID)
);

INSERT INTO CUAHANG VALUES ('001', N'SHOP QUẦN ÁO RAIN','28 Nhật Tảo, Phường 4, Quận 10, TP HCM', '', '0975187293');

CREATE TABLE LOAIHANG(
	ID INT IDENTITY(1,1) NOT NULL, 
	IDLOAIHANG AS 'L' + RIGHT('0000' + CAST(ID AS VARCHAR(5)), 5) PERSISTED,
	TenLoai NVARCHAR(20) NOT NULL, 
	Gender NVARCHAR(10) NOT NULL,	
	MoTa NVARCHAR(50),
	PRIMARY KEY (IDLOAIHANG)
);

INSERT INTO LOAIHANG VALUES (N'QUẦN', N'Nam', ''),						    
							(N'QUẦN', N'Nữ', ''),
							(N'ÁO', N'Nam',''),
							(N'ÁO', N'Nữ', ''),
							(N'QUẦN SHORT', N'Nam','');


CREATE TABLE SUBLOAIHANG
(
	ID INT IDENTITY(1,1) NOT NULL,
	IDSUBLH AS 'S' + RIGHT('0000' + CAST(ID AS VARCHAR(5)), 5) PERSISTED,
	IDLoaiHang VARCHAR(6),
	TenLoai NVARCHAR(20) not null,
	MoTa NVARCHAR(50),
	PRIMARY KEY(IDSUBLH),
	foreign key(IDLoaiHang) references LOAIHANG(IDLOAIHANG)
);

INSERT INTO SUBLOAIHANG VALUES('L00001', N'QUẦN JEAN',''),
							('L00001', N'QUẦN KAKI',''),
							('L00001', N'QUẦN JOGGER',''),
							('L00005', N'QUẦN SHORT KAKI', ''),
							('L00005', N'QUẦN SHORT JEAN', ''),
							('L00005', N'QUẦN SHORT THUN', ''),
							('L00003', N'ÁO SƠ MI TAY DÀI', ''),
							('L00003', N'ÁO SƠ MI TAY NGẮN', ''),
							('L00003', N'ÁO THUN TAY DÀI', ''),
							('L00003', N'ÁO THUN TAY NGẮN', '');

CREATE TABLE MATHANG(
	ID INT IDENTITY(1,1) NOT NULL, 
	IDMATHANG AS 'MH' + RIGHT('0000' + CAST(ID AS VARCHAR(5)), 5) PERSISTED,
	TenMH NVARCHAR(40) NOT NULL,
	IDLoaiHang VARCHAR(6) NOT NULL,
	IDSubLoaiHang VARCHAR(6),
	URLHinhAnh1 VARCHAR(100),
	URLHinhAnh2 VARCHAR(100),
	URLHinhAnh3 VARCHAR(100),
	MoTa NVARCHAR(100),
	GiaMoi INT,
	GiaCu INT,
	PRIMARY KEY(IDMATHANG),
	FOREIGN KEY (IDLoaiHang) REFERENCES LOAIHANG(IDLOAIHANG),
	FOREIGN KEY (IDSubLoaiHang) REFERENCES SUBLOAIHANG(IDSUBLH)
);

INSERT INTO MATHANG VALUES (N'Quần jean 116001','L00001','S00001' , 'http://totoshop.vn/cdn/store/7136/ps/20161124/jean_nam_32__3__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161109/3486895/JEAN_NAM_116001_(jean_nam_32_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161109/3486895/JEAN_NAM_116001_(jean_nam_32_(2)).jpg', '', 385000, ''),
						   (N'Quần jean 116002', 'L00001', 'S00001', 'http://totoshop.vn/cdn/store/7136/ps/20161128/jan_nam_63__4__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161109/3486896/JEAN_NAM_116002_(jan_nam_63_(2)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161109/3486896/JEAN_NAM_116002_(jan_nam_63_(3)).jpg', '', 385000, ''),
						   (N'Quần jean 116004', 'L00001', 'S00001', 'http://totoshop.vn/cdn/store/7136/ps/20161130/jean_nam_21__3__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161106/3476330/JEAN_NAM_106014_(jean_nam_21_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161106/3476330/JEAN_NAM_106014_(jean_nam_21_(4)).jpg', '', 365000, ''),
						   (N'Quần jean 106010NV', 'L00001','S00002' , 'http://totoshop.vn/cdn/store/7136/ps/20161116/quna_nam_13__4__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161018/3415541/KAKI_NAM_106010NV_(quna_nam_13_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161018/3415541/KAKI_NAM_106010NV_(quna_nam_13_(2)).jpg', '', 365000, ''),
						   (N'Quần kaki 106010GR', 'L00001', 'S00002', 'http://totoshop.vn/cdn/store/7136/ps/20161115/quan_nam_2__4__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161018/3415540/KAKI_NAM_106010GR_(quan_nam_2_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161018/3415540/KAKI_NAM_106010GR_(quan_nam_2_(2)).jpg', '', 365000, ''),
						   (N'Short jean 106005', 'L00005','S00005', 'http://totoshop.vn/cdn/store/7136/ps/20161109/short_nam_7__3__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161029/3445428/SHORT_JEAN_NAM_106005_(short_nam_7_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161029/3445428/SHORT_JEAN_NAM_106005_(short_nam_7_(2)).jpg', '', 275000, ''),
						   (N'Short kaki 096001WH', 'L00005','S00004', 'http://totoshop.vn/cdn/store/7136/ps/20161026/short_nam_35__2__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20160924/3344542/SHORT_KK_096001WH_(short_nam_35_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20160924/3344542/SHORT_KK_096001WH_(short_nam_35_(3)).jpg', '', 225000, ''),
						   (N'Short thun QT07155', 'L00005','S00006', 'http://totoshop.vn/cdn/store/7136/ps/20151025/384212020_500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20150813/1867187/QT07155_(quan_short_nam_20_(2)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20150813/1867187/QT07155_(quan_short_nam_20_(2)).jpg', '', 155000, ''),
						   (N'Áo thun 086014GR', 'L00003','S00009', 'http://totoshop.vn/cdn/store/7136/ps/20161208/thun_nam_20__2__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161111/3495765/THUN_086014GR_(thun_nam_20_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161111/3495765/THUN_086014GR_(thun_nam_20_(3)).jpg', '', 225000, ''),
						   (N'Áo thun 096036GN', 'L00003','S00010', 'http://totoshop.vn/cdn/store/7136/ps/20161103/thun_nam_22__7__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20160930/3366725/THUN_NAM_TN_096036GN_(thun_nam_22_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20160930/3366725/THUN_NAM_TN_096036GN_(thun_nam_22_(2)).jpg', '', 225000, ''),
						   (N'Áo sơ mi 116004WH', 'L00003','S00007', 'http://totoshop.vn/cdn/store/7136/ps/20161208/somi_nam_15__3__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161110/3492112/SO_MI_TD_116004WH_(somi_nam_15_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161110/3492112/SO_MI_TD_116004WH_(somi_nam_15_(2)).jpg', '', 245000, ''),
						   (N'Áo sơ mi 116005BL', 'L00003','S00008', 'http://totoshop.vn/cdn/store/7136/ps/20161125/somi_nam_51__5__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161110/3492113/SO_MI_TN_116005BL_(somi_nam_51_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161110/3492113/SO_MI_TN_116005BL_(somi_nam_51_(2)).jpg', '', 245000, '');

CREATE TABLE CHITIETMATHANG(
	ID INT IDENTITY(1,1) NOT NULL,
	IDCTMH AS 'CTMH' + RIGHT('0000' + CAST(ID AS VARCHAR(5)), 5) PERSISTED,
	IDMatHang VARCHAR(7) NOT NULL,
	Size VARCHAR(3),
	Gia INT,
	SoLuong INT,
	PRIMARY KEY (IDCTMH),
	FOREIGN KEY (IDMatHang) REFERENCES MATHANG(IDMATHANG),
);

INSERT INTO CHITIETMATHANG VALUES ('MH00001', '29', 385000, 5),
								  ('MH00001', '30', 385000, 6),
								  ('MH00001', '31', 385000, 7),
								  ('MH00002', '28', 385000, 8),
								  ('MH00002', '29', 385000, 5),
								  ('MH00002', '30', 385000, 4),
								  ('MH00002', '31', 385000, 3),
								  ('MH00003', '28', 365000, 7),
								  ('MH00003', '29', 365000, 9),
								  ('MH00003', '30', 365000, 7),
								  ('MH00003', '31', 365000, 7),
								  ('MH00004', '29', 365000, 5),
								  ('MH00004', '30', 365000, 6),
								  ('MH00004', '31', 365000, 5), 
								  ('MH00005', '30', 365000, 3),
								  ('MH00005', '31', 365000, 2),
								  ('MH00005', '32', 365000, 1),
								  ('MH00006', '28', 275000, 3),
								  ('MH00006', '29', 275000, 2),
								  ('MH00006', '30', 275000, 2),
								  ('MH00007', '29', 225000, 5),
								  ('MH00007', '30', 225000, 3), 
								  ('MH00008', '30', 155000, 2),
								  ('MH00009', 'M', 225000,  3),
								  ('MH00009', 'L', 225000, 4),
								  ('MH00010', 'M', 225000, 5),
								  ('MH00010', 'L', 225000, 5),
								  ('MH00011', 'M', 245000, 5),
								  ('MH00011', 'L', 245000, 5),
								  ('MH00012', 'M', 225000, 5),
								  ('MH00012', 'L', 225000, 5);

CREATE TABLE ACCOUNTTYPE(
	ID VARCHAR(5) NOT NULL,
	TenLoai NVARCHAR(20) NOT NULL,
	PRIMARY KEY (ID)
);

INSERT INTO ACCOUNTTYPE VALUES ('TYP01', N'ACCOUNT ADMIN'),
							   ('TYP02', N'ACCOUNT VIP'), 
							   ('TYP03', N'ACCOUNT THƯỜNG');

CREATE TABLE ACCOUNT(
	Username VARCHAR(20) NOT NULL,
	Password VARCHAR(20) NOT NULL, 
	IDType VARCHAR(5) NOT NULL,
	PRIMARY KEY (Username),
	FOREIGN KEY (IDType) REFERENCES ACCOUNTTYPE(ID)
);

INSERT INTO ACCOUNT VALUES ('admin', 'admin', 'TYP01'),
						   ('admin2', 'admin2', 'TYP01'),
						   ('admin3', 'admin3', 'TYP01'),
						   ('user01', '12345', 'TYP02'),
						   ('user02', '12345', 'TYP03');

CREATE TABLE ACCOUNTADMIN(
	ID VARCHAR(5) NOT NULL,
	Ten NVARCHAR(20) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	SDT INT NOT NULL,
	Account VARCHAR(20) NOT NULL,
	PRIMARY KEY (ID),
	FOREIGN KEY (Account) REFERENCES ACCOUNT(Username)
);

INSERT INTO ACCOUNTADMIN VALUES ('AD001', N'Thịnh', 'cuongthinhtuan2006@gmail.com',0975187293, 'admin'),
								('AD002', N'Ngọc', 'hkthienngoc1@gmail.com',01663524124, 'admin2'),
								('AD003', N'Hoàng', 'hathehoanghayen@gmail.com',0963225084, 'admin3');

CREATE TABLE KHACHHANG(
	ID INT IDENTITY(1,1) NOT NULL,
	IDKHACHHANG AS 'KH' + RIGHT('000' + CAST(ID AS VARCHAR(4)), 4) PERSISTED,
	Ten NVARCHAR(20) NOT NULL,
	Email VARCHAR(30), 
	SDT INT NOT NULL,
	DiaChi NVARCHAR(50),
	IDAccount VARCHAR(20),
	PRIMARY KEY (IDKHACHHANG),
);

INSERT INTO KHACHHANG VALUES (N'Thảo','nguyenchauthao@gmail.com',012244333, N'172/12 Trần Phú, Q5, TP HCM', 'user01'),
							 (N'Hoài','vovanhoai@gmail.com',012255666, N'30/10 Trần Hưng Đạo, Q1, TP HCM', 'user02');	

CREATE TABLE HOADON(
	ID INT IDENTITY(1,1) NOT NULL,
	IDHOADON AS 'HD' + RIGHT('00000' + CAST(ID AS VARCHAR(6)), 6) PERSISTED,
	IDKhachHang VARCHAR (6) NOT NULL,
	Ngay DATE NOT NULL,
	TongTien INT NOT NULL,
	TinhTrang NVARCHAR(20) NOT NULL,
	PRIMARY KEY (IDHOADON),
	FOREIGN KEY (IDKhachHang) REFERENCES KHACHHANG(IDKHACHHANG)
);

INSERT INTO HOADON VALUES ('KH0001', '11/12/2016', '385000', N'Đã đặt hàng'),
						  ('KH0002', '12/12/2016', '385000', N'Đã thanh toán');

CREATE TABLE CHITIETHOADON(
	ID INT IDENTITY(1,1) NOT NULL,
	IDCTHD AS 'CTHD' + RIGHT('000000' + CAST(ID AS VARCHAR(7)), 7) PERSISTED,
	IDChiTietMatHang VARCHAR(9) NOT NULL,
	IDHoaDon VARCHAR(8) NOT NULL,
	SoLuong INT,
	PRIMARY KEY (IDCTHD),
	FOREIGN KEY (IDHoaDon) REFERENCES HOADON(IDHOADON)
);

INSERT INTO CHITIETHOADON VALUES ('CTMH00001', 'HD000001', 1),
								 ('CTMH00002', 'HD000002', 1);						 

CREATE TABLE MATHANG_BACKLOG(
	IDMATHANG VARCHAR(7),
	TenMH NVARCHAR(40) NOT NULL,
	IDLoaiHang VARCHAR(6) NOT NULL,
	IDSubLoaiHang VARCHAR(6),
	URLHinhAnh1 VARCHAR(100),
	URLHinhAnh2 VARCHAR(100),
	URLHinhAnh3 VARCHAR(100),
	MoTa NVARCHAR(100),
	GiaMoi INT,
	GiaCu INT,
	PRIMARY KEY(IDMATHANG),
);

CREATE TABLE CHITIETMATHANG_BACKLOG(
	IDCTMH VARCHAR(9),
	IDMatHang VARCHAR(7) NOT NULL,
	Size VARCHAR(3),
	Gia INT,
	SoLuong INT,
	PRIMARY KEY (IDCTMH),
	FOREIGN KEY (IDMatHang) REFERENCES MATHANG_BACKLOG(IDMATHANG),
);