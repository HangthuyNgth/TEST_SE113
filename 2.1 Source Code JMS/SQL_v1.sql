CREATE DATABASE QLVBDQ
GO

USE QLVBDQ
GO

CREATE TABLE DVT
(
    MaDVT CHAR(8) CONSTRAINT PK_DVT PRIMARY KEY,
    TenDVT NVARCHAR(20) NOT NULL,
)

CREATE TABLE LOAISANPHAM
(
    MaLSP CHAR(8) CONSTRAINT PK_LSP PRIMARY KEY,
    TenLSP NVARCHAR(20) NOT NULL,
    MaDVT CHAR(8) CONSTRAINT FK_LSP_DVT FOREIGN KEY REFERENCES DVT(MaDVT) NOT NULL,
    PhanTramLoiNhuan INT NOT NULL
)

CREATE TABLE SANPHAM
(
    MaSP CHAR(8) CONSTRAINT PK_SP PRIMARY KEY,
    TenSP NVARCHAR(100) NOT NULL,
    MaLSP CHAR(8) CONSTRAINT FK_SP_LSP FOREIGN KEY REFERENCES LOAISANPHAM(MaLSP) NOT NULL,
    DonGia FLOAT NOT NULL,
    TonKho INT NOT NULL
)

CREATE TABLE NHACUNGCAP
(
    MaNCC CHAR(8) CONSTRAINT PK_NCC PRIMARY KEY,
    TenNCC NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(100) NOT NULL,
    SDT CHAR(10) NOT NULL
)

CREATE TABLE KHACHHANG
(
    MaKH CHAR(8) CONSTRAINT PK_KH PRIMARY KEY,
    TenKH NVARCHAR(20) NOT NULL,
    SDT CHAR(10)
)

CREATE TABLE LOAIDICHVU
(
    MaLDV CHAR(8) CONSTRAINT PK_LDV PRIMARY KEY,
    TenLDV NVARCHAR(100) NOT NULL,
    DonGia FLOAT NOT NULL
)

CREATE TABLE PHIEUBANHANG
(
    SoPhieu CHAR(8) CONSTRAINT PK_PBH PRIMARY KEY,
    NgayLap DATE NOT NULL,
    MaKH CHAR(8) CONSTRAINT FK_PBH_KH FOREIGN KEY REFERENCES KHACHHANG(MaKH) NOT NULL,
    TongTien FLOAT
)

CREATE TABLE CTPBH
(
    SoPhieu CHAR(8) CONSTRAINT FK_CTPBH_PBH FOREIGN KEY REFERENCES PHIEUBANHANG(SoPhieu),
    MaSP CHAR(8) CONSTRAINT FK_CTPBH_SP FOREIGN KEY REFERENCES SANPHAM(MaSP),
    CONSTRAINT PK_CTPBH PRIMARY KEY (SoPhieu,MaSP),
    SoLuong INT NOT NULL,
    DonGiaBH FLOAT,
    ThanhTien FLOAT
)

CREATE TABLE PHIEUMUAHANG
(
    SoPhieu CHAR(8) CONSTRAINT PK_PMH PRIMARY KEY,
    NgayLap DATE NOT NULL,
    MaNCC CHAR(8) CONSTRAINT FK_PMH_NCC FOREIGN KEY REFERENCES NHACUNGCAP(MaNCC) NOT NULL,
    TongTien FLOAT
)

CREATE TABLE CTPMH
(
    SoPhieu CHAR(8) CONSTRAINT FK_CTPMH_PMH FOREIGN KEY REFERENCES PHIEUMUAHANG(SoPhieu),
    MaSP CHAR(8) CONSTRAINT FK_CTPMH_SP FOREIGN KEY REFERENCES SANPHAM(MaSP),
    CONSTRAINT PK_CTPMH PRIMARY KEY (SoPhieu,MaSP),
    SoLuong INT NOT NULL,
    DonGiaMH FLOAT NOT NULL,
    ThanhTien FLOAT
)

CREATE TABLE PHIEUDICHVU
(
    SoPhieu CHAR(8) CONSTRAINT PK_PDV PRIMARY KEY,
    NgayLap DATE NOT NULL,
    MaKH CHAR(8) CONSTRAINT FK_PDV_KH FOREIGN KEY REFERENCES KHACHHANG(MaKH) NOT NULL,
    TongTien FLOAT,
    TongTraTruoc FLOAT,
    TongConLai FLOAT,
    TinhTrang BIT
)

CREATE TABLE CTPDV
(
    SoPhieu CHAR(8) CONSTRAINT FK_CTPDV_PDV FOREIGN KEY REFERENCES PHIEUDICHVU(SoPhieu),
    MaLDV CHAR(8) CONSTRAINT FK_CTPDV_LDV FOREIGN KEY REFERENCES LOAIDICHVU(MaLDV),
    CONSTRAINT PK_CTPDV PRIMARY KEY (SoPhieu,MaLDV),
    ChiPhiRieng FLOAT, --khong nhap coi nhu bang 0
    SoLuong INT NOT NULL,
    DonGia FLOAT,
    ThanhTien FLOAT,
    TraTruoc FLOAT NOT NULL,
    ConLai FLOAT,
	NgayGiao DATE,
    TinhTrang BIT NOT  NULL --mac dinh la false 
	
)

CREATE TABLE THAMSO
(
    TenThamSo NVARCHAR(20) CONSTRAINT PK_TS PRIMARY KEY,
    GiaTri FLOAT NOT NULL
)



CREATE TABLE BCTONKHO
(
    Thang INT,
    Nam INT,
    MaSP CHAR(8) CONSTRAINT FK_BCTK_SP FOREIGN KEY REFERENCES SANPHAM(MaSP),
    CONSTRAINT PK_BCTK PRIMARY KEY (Thang,Nam,MaSP),
    TonDau INT,
    SLMua INT,
    SLBan INT,
    TonCuoi INT    
)

CREATE TABLE NGUOIDUNG
(
    Email VARCHAR(40) CONSTRAINT PK_ND PRIMARY KEY,
    SDT CHAR(10) NOT NULL,
    MatKhau NVARCHAR(200) NOT NULL, 
    HoTen NVARCHAR(200) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh BIT NOT NULL,
    LoaiNguoiDung INT
)


CREATE TABLE CHUCNANG
(
    MaChucNang CHAR(8) CONSTRAINT PK_CN PRIMARY KEY,
    TenChucNang VARCHAR(30) NOT NULL,
    TenManHinhDuocLoad VARCHAR(30) NOT NULL,
)

CREATE TABLE NHOMNGUOIDUNG
(
    MaNhom CHAR(8) CONSTRAINT PK_NND PRIMARY KEY,
    TenNhom VARCHAR(30) NOT NULL,
)

CREATE TABLE PHANQUYEN
(
    MaNhom CHAR(8) CONSTRAINT FK_PQ_NND FOREIGN KEY REFERENCES NHOMNGUOIDUNG(MaNhom),
    MaChucNang CHAR(8) CONSTRAINT FK_PQ_CN FOREIGN KEY REFERENCES CHUCNANG(MaChucNang),
    CONSTRAINT PK_PQ PRIMARY KEY (MaNhom, MaChucNang),
)

insert into THAMSO values(N'Tỉ lệ trả trước', 50)
INSERT INTO NGUOIDUNG(Email, SDT, MatKhau, HoTen, NgaySinh, GioiTinh, LoaiNguoiDung)
VALUES ('0', '0123456789', '0', N'Nguyen Khanh Huy', '1-3-2000', 0, 0)

INSERT INTO NGUOIDUNG(Email, SDT, MatKhau, HoTen, NgaySinh, GioiTinh, LoaiNguoiDung)
VALUES ('1', '0123456789', '1', N'Nguyen Khanh Huy', '1-3-2000', 0, 1)

INSERT INTO NGUOIDUNG(Email, SDT, MatKhau, HoTen, NgaySinh, GioiTinh, LoaiNguoiDung)
VALUES ('2', '0123456789', '2', N'Nguyen Khanh Huy', '1-3-2000', 0, 2)

INSERT INTO DVT (MaDVT, TenDVT) 
VALUES 
('DVT001', N'Gram'),
('DVT002', N'Chiếc'),
('DVT003', N'Bộ'),
('DVT004', N'Hộp'),
('DVT005', N'Cặp');

INSERT INTO LOAISANPHAM (MaLSP, TenLSP, MaDVT, PhanTramLoiNhuan) 
VALUES 
('LSP001', N'Vàng 9999', 'DVT001', 10),
('LSP002', N'Vàng 18K', 'DVT002', 12),
('LSP003', N'Bạc', 'DVT003', 15),
('LSP004', N'Kim cương', 'DVT001', 20),
('LSP005', N'Đá quý Ruby', 'DVT003', 25);

INSERT INTO LOAIDICHVU (MaLDV, TenLDV, DonGia) 
VALUES 
('LDV001', N'Đánh bóng vàng', 500000),
('LDV002', N'Khắc tên', 200000),
('LDV003', N'Bảo hành sản phẩm', 100000),
('LDV004', N'Đo và chỉnh kích cỡ', 300000),
('LDV005', N'Chế tác riêng theo yêu cầu', 1500000);

INSERT INTO NHACUNGCAP (MaNCC, TenNCC, DiaChi, SDT) 
VALUES 
('NCC001', N'Công ty TNHH Vàng Mi Hồng', N'123 Đường ABC, Quận 1, TP.HCM', '0909123456'),
('NCC002', N'Công ty TNHH Bạc Ngọc', N'456 Đường XYZ, Quận 3, TP.HCM', '0909876543'),
('NCC003', N'Công ty TNHH Đá Quý Hoàng Anh', N'789 Đường DEF, Quận 5, TP.HCM', '0909988776'),
('NCC004', N'Công ty TNHH Kim Cương Phú Quý', N'321 Đường GHI, Quận 7, TP.HCM', '0912345678'),
('NCC005', N'Công ty TNHH Vàng Bạc Đỉnh Cao', N'654 Đường JKL, Quận 10, TP.HCM', '0919876543');

INSERT INTO SANPHAM (MaSP, TenSP, MaLSP, DonGia, TonKho) 
VALUES 
('SP001', N'Nhẫn vàng 9999', 'LSP001', 5000000, 100),
('SP002', N'Lắc tay vàng 18K', 'LSP002', 3000000, 50),
('SP003', N'Vòng cổ bạc', 'LSP003', 1500000, 60),
('SP004', N'Nhẫn kim cương', 'LSP004', 20000000, 10),
('SP005', N'Đá quý Ruby đỏ', 'LSP005', 10000000, 20),
('SP006', N'Hoa tai vàng 18K', 'LSP002', 2500000, 80),
('SP007', N'Nhẫn đá quý Sapphire', 'LSP005', 12000000, 15),
('SP008', N'Vòng tay bạc', 'LSP003', 1800000, 40),
('SP009', N'Mặt dây chuyền kim cương', 'LSP004', 15000000, 5),
('SP010', N'Dây chuyền vàng 9999', 'LSP001', 6000000, 30);



CREATE PROCEDURE GetAccountByEmailPassword
@email varchar(40), @password varchar(30)
as
begin
	Select * from NGUOIDUNG
	where Email = @email and MatKhau = @password;
end
go


CREATE PROCEDURE GetAccountByEmail
@email varchar(40)
as
begin
	Select * from NGUOIDUNG
	where Email = @email
end
go



CREATE PROCEDURE InsertAccount
@email varchar(40), @SDT char(10), @tenTK nvarchar(50), @ngaySinh date, @gioiTinh bit, @matKhau nvarchar(30)
as
begin
	INSERT INTO NGUOIDUNG(Email, SDT, MatKhau, HoTen, NgaySinh, GioiTinh, LoaiNguoiDung)
	VALUES (@email, @SDT, @matKhau, @tenTK, @ngaySinh, @gioiTinh, 0)
end
go



create trigger trg_CTPDV_insert on CTPDV after insert as
Begin
	UPDATE PHIEUDICHVU
	set TongTien = TongTien + ( select ThanhTien from inserted where SoPhieu = PHIEUDICHVU.SoPhieu),
		TongTraTruoc = TongTraTruoc + ( select TraTruoc from inserted where SoPhieu = PHIEUDICHVU.SoPhieu),
		TongConLai = TongConLai + ( select ConLai from inserted where SoPhieu = PHIEUDICHVU.SoPhieu)
	from PHIEUDICHVU
	join inserted on PHIEUDICHVU.SoPhieu = inserted.SoPhieu

	declare @sp VARCHAR(100)
	set @sp = (select SoPhieu from inserted)
	Begin
		if ((select count(CTPDV.MaLDV) from CTPDV, PHIEUDICHVU where PHIEUDICHVU.SoPhieu = CTPDV.SoPhieu and CTPDV.SoPhieu = @sp) != 
			(select count(CTPDV.MaLDV) from CTPDV, PHIEUDICHVU where PHIEUDICHVU.SoPhieu = CTPDV.SoPhieu and CTPDV.SoPhieu = @sp and CTPDV.TinhTrang = 1))
			update PHIEUDICHVU set TinhTrang = 0 where SoPhieu = @sp
		else update PHIEUDICHVU set TinhTrang = 1 where SoPhieu = @sp
	end
end



create trigger trg_CTPDV_delete on CTPDV after delete as
Begin
	UPDATE PHIEUDICHVU
	set TongTien = TongTien - ( select ThanhTien from deleted where SoPhieu = PHIEUDICHVU.SoPhieu),
		TongTraTruoc = TongTraTruoc - ( select TraTruoc from deleted where SoPhieu = PHIEUDICHVU.SoPhieu),
		TongConLai = TongConLai - ( select ConLai from deleted where SoPhieu = PHIEUDICHVU.SoPhieu)
	from PHIEUDICHVU
	join deleted on PHIEUDICHVU.SoPhieu = deleted.SoPhieu

	declare @sp VARCHAR(100)
	set @sp = (select SoPhieu from deleted)
	Begin
		if ((select count(CTPDV.MaLDV) from CTPDV, PHIEUDICHVU where PHIEUDICHVU.SoPhieu = CTPDV.SoPhieu and CTPDV.SoPhieu = @sp) != 
			(select count(CTPDV.MaLDV) from CTPDV, PHIEUDICHVU where PHIEUDICHVU.SoPhieu = CTPDV.SoPhieu and CTPDV.SoPhieu = @sp and CTPDV.TinhTrang = 1))
			update PHIEUDICHVU set TinhTrang = 0 where SoPhieu = @sp
		else update PHIEUDICHVU set TinhTrang = 1 where SoPhieu = @sp
	end
end



create trigger trg_CTPDV_update on CTPDV after update as
begin
	--update PHIEUDICHVU
	declare @sp VARCHAR(100)
	set @sp = (select SoPhieu from inserted)
	Begin
		if ((select count(CTPDV.MaLDV) from CTPDV, PHIEUDICHVU where PHIEUDICHVU.SoPhieu = CTPDV.SoPhieu and CTPDV.SoPhieu = @sp) != 
			(select count(CTPDV.MaLDV) from CTPDV, PHIEUDICHVU where PHIEUDICHVU.SoPhieu = CTPDV.SoPhieu and CTPDV.SoPhieu = @sp and CTPDV.TinhTrang = 1))
			update PHIEUDICHVU set TinhTrang = 0 where SoPhieu = @sp
		else update PHIEUDICHVU set TinhTrang = 1 where SoPhieu = @sp
	end
end


-------------------
create trigger trg_CTPBH_insert on CTPBH after insert as
Begin
	UPDATE SANPHAM
	set TonKho = TonKho - (select SoLuong from inserted)
	where SANPHAM.MaSP = (select MaSP from inserted)
	UPDATE PHIEUBANHANG
	set TongTien = TongTien + (select ThanhTien from inserted)
	where SoPhieu = (select SoPhieu from inserted)
	
end


create trigger trg_CTPBH_delete on CTPBH after delete as
Begin
	UPDATE SANPHAM
	set TonKho = TonKho + (select SoLuong from deleted)
	where SANPHAM.MaSP = (select MaSP from deleted)
	UPDATE PHIEUBANHANG
	set TongTien = TongTien - (select ThanhTien from deleted)
	where SoPhieu = (select SoPhieu from deleted)
end

---------------------------------------

create trigger trg_CTPMH_insert on CTPMH after insert as
Begin
	UPDATE SANPHAM
	set TonKho = TonKho + (select SoLuong from inserted)
	where SANPHAM.MaSP = (select MaSP from inserted)
	UPDATE PHIEUMUAHANG
	set TongTien = TongTien + (select ThanhTien from inserted)
	where SoPhieu = (select SoPhieu from inserted)
end


create trigger trg_CTPMH_delete on CTPMH after delete as
Begin
	UPDATE SANPHAM
	set TonKho = TonKho - (select SoLuong from deleted)
	where SANPHAM.MaSP = (select MaSP from deleted)
	UPDATE PHIEUMUAHANG
	set TongTien = TongTien - (select ThanhTien from deleted)
	where SoPhieu = (select SoPhieu from deleted)
end

