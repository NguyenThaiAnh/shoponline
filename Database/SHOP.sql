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

CREATE TABLE LOAIHANG(
	ID VARCHAR(5) NOT NULL, 
	TenLoai NVARCHAR(20) NOT NULL, 
	
	MoTa NVARCHAR(50),
	
	PRIMARY KEY (ID)
);

CREATE TABLE SUBLOAIHANG
(
ID VARCHAR(5),
IDLoaiHang VARCHAR(5),
TenLoai NVARCHAR(20) not null,
MoTa NVARCHAR(50),
PRIMARY KEY(ID),
foreign key(IDLoaiHang) references LOAIHANG(ID)
);

CREATE TABLE MATHANG(
	ID VARCHAR(6) NOT NULL, 
	TenMH NVARCHAR(40) NOT NULL,
	IDLoaiHang VARCHAR(5) NOT NULL,
	IDSubLoaiHang VARCHAR(5),
	URLHinhAnh1 VARCHAR(100),
	URLHinhAnh2 VARCHAR(100),
	URLHinhAnh3 VARCHAR(100),
	PRIMARY KEY(ID),
	FOREIGN KEY (IDLoaiHang) REFERENCES LOAIHANG(ID),
	foreign key(IDSubLoaiHang) references SUBLOAIHANG(ID)
);

CREATE TABLE CHITIETMATHANG(
	ID VARCHAR(6) NOT NULL,
	IDMatHang VARCHAR(6) NOT NULL,
	MoTa NVARCHAR(100),
	MauSac NVARCHAR(20),
	Size VARCHAR(3),
	Gia INT,
	Loai VARCHAR(3),
	SoLuong INT,
	PRIMARY KEY (ID),
	FOREIGN KEY (IDMatHang) REFERENCES MATHANG(ID),
);

CREATE TABLE ACCOUNTTYPE(
	ID VARCHAR(5) NOT NULL,
	TenLoai NVARCHAR(20) NOT NULL,
	PRIMARY KEY (ID)
);

CREATE TABLE ACCOUNT(
	Username VARCHAR(20) NOT NULL,
	Password VARCHAR(20) NOT NULL, 
	IDType VARCHAR(5) NOT NULL,
	PRIMARY KEY (Username),
	FOREIGN KEY (IDType) REFERENCES ACCOUNTTYPE(ID)
);

CREATE TABLE ACCOUNTADMIN(
	ID VARCHAR(5) NOT NULL,
	Ten NVARCHAR(20) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	SDT INT NOT NULL,
	Account VARCHAR(20) NOT NULL,
	PRIMARY KEY (ID),
	FOREIGN KEY (Account) REFERENCES ACCOUNT(Username)
);

CREATE TABLE KHACHHANG(
	ID VARCHAR(7) NOT NULL,
	Ten NVARCHAR(20) NOT NULL,
	Email VARCHAR(30), 
	SDT INT NOT NULL,
	DiaChi NVARCHAR(50),
	IDAccount VARCHAR(20),
	PRIMARY KEY (ID),
	FOREIGN KEY (IDAccount) REFERENCES ACCOUNT(Username)
);

CREATE TABLE HOADON(
	ID VARCHAR(5) NOT NULL,
	IDKhachHang VARCHAR (7) NOT NULL,
	Ngay DATE NOT NULL,
	TongTien INT NOT NULL,
	TinhTrang NVARCHAR(20) NOT NULL,
	PRIMARY KEY (ID),
	FOREIGN KEY (IDKhachHang) REFERENCES KHACHHANG(ID)
);

CREATE TABLE CHITIETHOADON(
	ID VARCHAR(6) NOT NULL,
	IDChiTietMatHang VARCHAR(6) NOT NULL,
	IDHoaDon VARCHAR(5) NOT NULL,
	SoLuong INT,
	PRIMARY KEY (ID),
	FOREIGN KEY (IDChiTietMatHang) REFERENCES CHITIETMATHANG(ID),
	FOREIGN KEY (IDHoaDon) REFERENCES HOADON(ID)
);



INSERT INTO CUAHANG VALUES ('001', 'SHOP QUẦN ÁO RAIN','28 Nhật Tảo, Phường 4, Quận 10, TP HCM', '', '0975187293');

INSERT INTO LOAIHANG VALUES ('Q0001',N'QUẦN', ''),						    
							('A0001', N'ÁO', '');
							
INSERT INTO SUBLOAIHANG VALUES('SQ001', 'Q0001', N'QUẦN JEAN',''),
							('SQ002', 'Q0001', N'QUẦN KAKI',''),
							('SQ003', 'Q0001', N'QUẦN NGỐ', ''),
							('SA001', 'A0001', N'ÁO SƠ MI TÀY DÀI', ''),
							('SA002', 'A0001', N'ÁO SƠ MI TAY NGẮN', '');

INSERT INTO MATHANG VALUES ('MHQ001', 'QUẦN JEAN 116001','Q0001','SQ001' , 'http://totoshop.vn/cdn/store/7136/ps/20161124/jean_nam_32__3__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161109/3486895/JEAN_NAM_116001_(jean_nam_32_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161109/3486895/JEAN_NAM_116001_(jean_nam_32_(2)).jpg'),
						   ('MHQ002', 'QUẦN JEAN 116002', 'Q0001', 'SQ001', 'http://totoshop.vn/cdn/store/7136/ps/20161128/jan_nam_63__4__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161109/3486896/JEAN_NAM_116002_(jan_nam_63_(2)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161109/3486896/JEAN_NAM_116002_(jan_nam_63_(3)).jpg'),
						   ('MHQ003', 'QUẦN JEAN 116004', 'Q0001', 'SQ001', 'http://totoshop.vn/cdn/store/7136/ps/20161130/jean_nam_21__3__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161106/3476330/JEAN_NAM_106014_(jean_nam_21_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161106/3476330/JEAN_NAM_106014_(jean_nam_21_(4)).jpg'),
						   ('MHQ004', 'QUẦN KAKI 106010NV', 'Q0001','SQ002' , 'http://totoshop.vn/cdn/store/7136/ps/20161116/quna_nam_13__4__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161018/3415541/KAKI_NAM_106010NV_(quna_nam_13_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161018/3415541/KAKI_NAM_106010NV_(quna_nam_13_(2)).jpg'),
						   ('MHQ005', 'QUẦN KAKI 106010GR', 'Q0001', 'SQ002', 'http://totoshop.vn/cdn/store/7136/ps/20161115/quan_nam_2__4__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161018/3415540/KAKI_NAM_106010GR_(quan_nam_2_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161018/3415540/KAKI_NAM_106010GR_(quan_nam_2_(2)).jpg'),
						   ('MHS001', 'QUẦN SHORT JEAN 106005', 'Q0001','SQ001', 'http://totoshop.vn/cdn/store/7136/ps/20161109/short_nam_7__3__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161029/3445428/SHORT_JEAN_NAM_106005_(short_nam_7_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161029/3445428/SHORT_JEAN_NAM_106005_(short_nam_7_(2)).jpg'),
						   ('MHS002', 'QUẦN SHORT KAKI 096001WH', 'Q0001','SQ001', 'http://totoshop.vn/cdn/store/7136/ps/20161026/short_nam_35__2__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20160924/3344542/SHORT_KK_096001WH_(short_nam_35_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20160924/3344542/SHORT_KK_096001WH_(short_nam_35_(3)).jpg'),
						   ('MHS003', 'QUẦN SHORT THUN QT07155', 'Q0001','SQ001', 'http://totoshop.vn/cdn/store/7136/ps/20151025/384212020_500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20150813/1867187/QT07155_(quan_short_nam_20_(2)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20150813/1867187/QT07155_(quan_short_nam_20_(2)).jpg'),
						   ('MHA001', 'ÁO THUN 086014GR', 'A0001','SA001', 'http://totoshop.vn/cdn/store/7136/ps/20161208/thun_nam_20__2__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161111/3495765/THUN_086014GR_(thun_nam_20_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161111/3495765/THUN_086014GR_(thun_nam_20_(3)).jpg'),
						   ('MHA002', 'ÁO THUN 096036GN', 'A0001','SA001', 'http://totoshop.vn/cdn/store/7136/ps/20161103/thun_nam_22__7__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20160930/3366725/THUN_NAM_TN_096036GN_(thun_nam_22_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20160930/3366725/THUN_NAM_TN_096036GN_(thun_nam_22_(2)).jpg'),
						   ('MHA003', 'ÁO SƠ MI 116004WH', 'A0001','SA001', 'http://totoshop.vn/cdn/store/7136/ps/20161208/somi_nam_15__3__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161110/3492112/SO_MI_TD_116004WH_(somi_nam_15_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161110/3492112/SO_MI_TD_116004WH_(somi_nam_15_(2)).jpg'),
						   ('MHA004', 'ÁO SƠ MI 116005BL', 'A0001','SA001', 'http://totoshop.vn/cdn/store/7136/ps/20161125/somi_nam_51__5__500x750.jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161110/3492113/SO_MI_TN_116005BL_(somi_nam_51_(1)).jpg', 'http://totoshop.vn/cdn/store/7136/psCT/20161110/3492113/SO_MI_TN_116005BL_(somi_nam_51_(2)).jpg');
						   

INSERT INTO CHITIETMATHANG VALUES ('CTQ001', 'MHQ001', '','XANH', '29', 385000, 'NAM', 5),
								  ('CTQ002', 'MHQ001', '','XANH', '30', 385000, 'NAM', 6),
								  ('CTQ003', 'MHQ001', '','XANH', '31', 385000, 'NAM', 7),
								  ('CTQ004', 'MHQ002', '','XANH', '28', 385000, 'NAM', 8),
								  ('CTQ005', 'MHQ002', '','XANH', '29', 385000, 'NAM', 5),
								  ('CTQ006', 'MHQ002', '','XANH', '30', 385000, 'NAM', 4),
								  ('CTQ007', 'MHQ002', '','XANH', '31', 385000, 'NAM', 3),
								  ('CTQ008', 'MHQ003', '','XANH', '28', 365000, 'NAM', 7),
								  ('CTQ009', 'MHQ003', '','XANH', '29', 365000, 'NAM', 9),
								  ('CTQ010', 'MHQ003', '','XANH', '30', 365000, 'NAM', 7),
								  ('CTQ011', 'MHQ003', '','XANH', '31', 365000, 'NAM', 7),
								  ('CTQ013', 'MHQ004', '','ĐEN', '29', 365000, 'NAM', 5),
								  ('CTQ014', 'MHQ004', '','ĐEN', '30', 365000, 'NAM', 6),
								  ('CTQ015', 'MHQ004', '','ĐEN', '31', 365000, 'NAM', 5), 
								  ('CTQ016', 'MHQ005', '', 'ĐEN', '30', 365000, 'NAM', 3),
								  ('CTQ017', 'MHQ005', '', 'ĐEN', '31', 365000, 'NAM', 2),
								  ('CTQ018', 'MHQ005', '', 'ĐEN', '32', 365000, 'NAM', 1),
								  ('CTQ019', 'MHS001', '', 'XANH', '28', 275000, 'NAM', 3),
								  ('CTQ020', 'MHS001', '', 'XANH', '29', 275000, 'NAM', 2),
								  ('CTQ021', 'MHS001', '', 'XANH', '30', 275000, 'NAM', 2),
								  ('CTQ022', 'MHS002', '', 'TRẮNG', '29', 225000, 'NAM', 5),
								  ('CTQ023', 'MHS002', '', 'TRẮNG', '30', 225000, 'NAM', 3), 
								  ('CTQ024', 'MHS003', '', 'XANH', '30', 155000, 'NAM', 2),
								  ('CTA001', 'MHA001', '', 'XÁM ĐỐM TRẮNG', 'M', 225000, 'NAM', 3),
								  ('CTA002', 'MHA001', '', 'XÁM ĐỐM TRẮNG', 'L', 225000, 'NAM', 4),
								  ('CTA003', 'MHA002', '', 'TRẮNG VÀ XANH', 'M', 225000, 'NAM', 5),
								  ('CTA004', 'MHA002', '', 'TRẮNG VÀ XANH', 'L', 225000, 'NAM', 5),
								  ('CTA005', 'MHA003', '', 'TRẮNG', 'M', 245000, 'NAM', 5),
								  ('CTA006', 'MHA003', '', 'TRẮNG', 'L', 245000, 'NAM', 5),
								  ('CTA007', 'MHA004', '', 'ĐEN', 'M', 225000, 'NAM', 5),
								  ('CTA008', 'MHA004', '', 'ĐEN', 'L', 225000, 'NAM', 5);

INSERT INTO ACCOUNTTYPE VALUES ('TYP01', 'ACCOUNT ADMIN'),
							   ('TYP02', 'ACCOUNT VIP'), 
							   ('TYP03', 'ACCOUNT THƯỜNG');

INSERT INTO ACCOUNT VALUES ('admin', 'admin', 'TYP01'),
						   ('admin2', 'admin2', 'TYP01'),
						   ('admin3', 'admin3', 'TYP01'),
						   ('user01', '12345', 'TYP02'),
						   ('user02', '12345', 'TYP03');

INSERT INTO ACCOUNTADMIN VALUES ('AD001', 'Thịnh', 'cuongthinhtuan2006@gmail.com',0975187293, 'admin'),
								('AD002', 'Ngọc', 'hkthienngoc1@gmail.com',01663524124, 'admin2'),
								('AD003', 'Hoàng', 'hathehoanghayen@gmail.com',0963225084, 'admin3');

INSERT INTO KHACHHANG VALUES ('KH001','Thảo','nguyenchauthao@gmail.com',012244333, '172/12 Trần Phú, Q5, TP HCM', 'user01'),
							 ('KH002','Hoài','vovanhoai@gmail.com',012255666, '30/10 Trần Hưng Đạo, Q1, TP HCM', 'user02');	

INSERT INTO HOADON VALUES ('HD001','KH001', '11/12/2016', '385000', 'Đã đặt hàng'),
						  ('HD002','KH002', '12/12/2016', '385000', 'Đã thanh toán');

INSERT INTO CHITIETHOADON VALUES ('CHD001', 'CTQ002', 'HD001', 1),
								 ('CHD002', 'CTQ001', 'HD002', 1);
								 