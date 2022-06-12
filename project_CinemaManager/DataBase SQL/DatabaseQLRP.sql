CREATE DATABASE QLRP
GO

USE QLRP
GO

SET DATEFORMAT DMY
GO


CREATE TABLE LoaiManHinh
(
	id VARCHAR(50) PRIMARY KEY,
	TenMH NVARCHAR(100) --2D || 3D || IMax
)
GO

CREATE TABLE PhongChieu
(
	id VARCHAR(50) PRIMARY KEY,
	TenPhong NVARCHAR(100) NOT NULL,
	idManHinh VARCHAR(50),
	SoChoNgoi INT NOT NULL,
	TinhTrang INT NOT NULL DEFAULT 1, -- 0:không hoạt động || 1:đang hoạt động
	SoHangGhe int not null,
	SoGheMotHang int not null,

	FOREIGN KEY (idManHinh) REFERENCES dbo.LoaiManHinh(id)
)
GO

CREATE TABLE Messages
(
  id_msg INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  body nvarchar(4000) NOT NULL,
  user_from nvarchar(255) NOT NULL,
  user_to nvarchar(255) NOT NULL,
  date_sent DATETIME NOT NULL
) 
GO


CREATE TABLE Phim
(
	id varchar(50) PRIMARY KEY,
	TenPhim nvarchar(100) NOT NULL,
	MoTa nvarchar(1000) NULL,
	ThoiLuong float NOT NULL,
	NgayKhoiChieu date NOT NULL,
	NgayKetThuc date NOT NULL,
	SanXuat nvarchar(50) NOT NULL,
	DaoDien nvarchar(100) NULL,
	NamSX int NOT NULL,
	ApPhich image
)
GO

CREATE TABLE DinhDangPhim --Phim có hỗ trợ màn hình 3D hay IMax không?
(
	id varchar(50) NOT NULL primary key,
	idPhim VARCHAR(50) NOT NULL,
	idLoaiManHinh VARCHAR(50) NOT NULL,

	FOREIGN KEY (idPhim) REFERENCES dbo.Phim(id),
	FOREIGN KEY (idLoaiManHinh) REFERENCES dbo.LoaiManHinh,
)
GO

CREATE TABLE TheLoai
(
	id VARCHAR(50) PRIMARY KEY,
	TenTheLoai NVARCHAR(100) NOT NULL,
	MoTa NVARCHAR(100)
)
GO

CREATE TABLE PhanLoaiPhim --Quan hệ giữa Phim và LoaiPhim là quan hệ n-n
(
	idPhim VARCHAR(50) NOT NULL,
	idTheLoai VARCHAR(50) NOT NULL,

	FOREIGN KEY (idPhim) REFERENCES dbo.Phim(id),
	FOREIGN KEY (idTheLoai) REFERENCES dbo.TheLoai(id),

	CONSTRAINT PK_PhanLoaiPhim PRIMARY KEY(idPhim,idTheLoai)
)
GO

CREATE TABLE LichChieu
(
	id VARCHAR(50) PRIMARY KEY,
	ThoiGianChieu DATETIME NOT NULL,
	idPhong VARCHAR(50) NOT NULL,
	idDinhDang VARCHAR(50) NOT NULL,
	GiaVe Money NOT NULL,
	TrangThai INT NOT NULL DEFAULT '0', --0: Chưa tạo vé cho lịch chiếu || 1: Đã tạo vé

	FOREIGN KEY (idPhong) REFERENCES dbo.PhongChieu(id),
	FOREIGN KEY (idDinhDang) REFERENCES dbo.DinhDangPhim(id),

)
GO

CREATE TABLE KhachHang
(
	id VARCHAR(50) PRIMARY KEY,
	HoTen NVARCHAR(100) NOT NULL,
	NgaySinh DATE NOT NULL,
	DiaChi NVARCHAR(100),
	SDT VARCHAR(100),
	CMND INT NOT NULL Unique
)
GO


CREATE TABLE TaiKhoan
(
	UserName NVARCHAR(100) Unique NOT NULL,
	Pass VARCHAR(1000) NOT NULL,
	LoaiTK INT NOT NULL DEFAULT 2, -- 1:admin || 2:Customer
	id VARCHAR(50) NOT NULL,
	FOREIGN KEY (id) REFERENCES dbo.KhachHang(id),
	PRIMARY KEY(id)
)
GO


CREATE TABLE Ve
(
	id int identity(1,1) PRIMARY KEY,
	LoaiVe INT  DEFAULT '0', --0: Vé người lớn || 1: Vé học sinh - sinh viên || 2: vé trẻ em
	idLichChieu VARCHAR(50),
	MaGheNgoi VARCHAR(50),
	idKhachHang VARCHAR(50),
	TrangThai INT NOT NULL DEFAULT '0', --0: 'Chưa Bán' || 1: 'Đã Bán'
	TienBanVe MONEY DEFAULT '0'

	FOREIGN KEY (idLichChieu) REFERENCES dbo.LichChieu(id),
	FOREIGN KEY (idKhachHang) REFERENCES dbo.KhachHang(id)
)
GO

CREATE TABLE HoaDon
(
	id int identity(1,1) PRIMARY KEY,
 
	idLichChieu VARCHAR(50),
	MaGheNgoi VARCHAR(50),
	idKhachHang VARCHAR(50),
	TrangThai INT NOT NULL DEFAULT '0', --0: 'Chưa Thanh Toán' || 1: 'Đã Thanh Toán'
	TongTien MONEY DEFAULT '0'

	FOREIGN KEY (id) REFERENCES dbo.Ve(id)
)
GO


--Trigger
CREATE TRIGGER UTG_INSERT_CheckDateLichChieu
ON dbo.LichChieu
FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idDinhDang VARCHAR(50), @ThoiGianChieu DATE, @NgayKhoiChieu DATE, @NgayKetThuc DATE

	SELECT @idDinhDang = idDinhDang, @ThoiGianChieu = CONVERT(DATE, ThoiGianChieu) from INSERTED

	SELECT @NgayKhoiChieu = P.NgayKhoiChieu, @NgayKetThuc = P.NgayKetThuc
	FROM dbo.Phim P, dbo.DinhDangPhim DD
	WHERE @idDinhDang = DD.id AND DD.idPhim = P.id

	IF ( @ThoiGianChieu > @NgayKetThuc or @ThoiGianChieu < @NgayKhoiChieu)
	BEGIN
		ROLLBACK TRAN
		Raiserror('Lịch Chiếu lớn hơn hoặc bằng Ngày Khởi Chiếu và nhỏ hơn hoặc bằng Ngày Kết Thúc',16,1)
		Return
    END
END
GO

CREATE TRIGGER UTG_CheckTimeLichChieu
ON dbo.LichChieu
FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @count INT = 0, @count2 INT = 0, @ThoiGianChieu DATETIME, @idPhong VARCHAR(50), @idDinhDang VARCHAR(50)

	SELECT @idPhong = idPhong, @ThoiGianChieu = ThoiGianChieu, @idDinhDang = Inserted.idDinhDang from INSERTED

	SELECT @count = COUNT(*)
	FROM dbo.LichChieu LC, dbo.DinhDangPhim DD, dbo.Phim P
	WHERE LC.idPhong = @idPhong AND LC.idDinhDang = DD.id AND DD.idPhim = P.id AND (@ThoiGianChieu >= LC.ThoiGianChieu AND @ThoiGianChieu <= DATEADD(MINUTE, P.ThoiLuong, LC.ThoiGianChieu))

	SELECT @count2 = COUNT(*)
	FROM dbo.LichChieu LC, dbo.DinhDangPhim DD, dbo.Phim P
	WHERE @idPhong = LC.idPhong AND @idDinhDang = DD.id AND DD.idPhim = P.id AND (LC.ThoiGianChieu >= @ThoiGianChieu AND LC.ThoiGianChieu <= DATEADD(MINUTE, P.ThoiLuong, @ThoiGianChieu))

	IF (@count > 1 OR @count2 > 1)
	BEGIN
		ROLLBACK TRAN
		Raiserror('Thời Gian Chiếu đã trùng với một lịch chiếu khác cùng Phòng Chiếu',16,1)
		Return
	END
END
GO

--Stored Procedures
--TÀI KHOẢN (Đổi mật khẩu & đăng nhập)
CREATE PROC USP_UpdatePasswordForAccount
@username NVARCHAR(100), @pass VARCHAR(1000), @newPass VARCHAR(1000)
AS
BEGIN
	DECLARE @isRightPass INT = 0
	SELECT @isRightPass = COUNT(*) FROM dbo.TaiKhoan WHERE UserName = @username AND Pass = @pass

	IF (@isRightPass = 1)
	BEGIN
		UPDATE dbo.TaiKhoan SET Pass = @newPass WHERE UserName = @username
    END
END
GO

CREATE PROC USP_Login
@userName NVARCHAR(1000), @pass VARCHAR(1000)
AS
BEGIN
	SELECT * FROM dbo.TaiKhoan WHERE UserName = @userName AND Pass = @pass
END
GO

--TÀI KHOẢN (frmAdmin)
--DROP PROC USP_GetAccountList
CREATE PROC USP_GetAccountList
AS
BEGIN
	SELECT UserName AS [Username],Pass AS [Pass], LoaiTK AS [Loại tài khoản], KhachHang.id AS [ID],HoTen AS [Họ Tên]
	FROM TaiKhoan LEFT JOIN KhachHang ON TaiKhoan.id = KhachHang.id
END
GO

--DROP PROC USP_GetAccountByID
CREATE PROC USP_GetAccountByID @ID VARCHAR(50)
AS
BEGIN
	SELECT TK.UserName AS [Username],TK.Pass AS Pass, TK.LoaiTK AS [LoaiTK], id AS [id]
	FROM dbo.TaiKhoan TK
	WHERE @ID = TK.id
END
GO


CREATE PROC USP_InsertAccount @username NVARCHAR(100), @Pass VARCHAR(100), @loaiTK INT, @id VARCHAR(100)
AS
BEGIN
	INSERT dbo.TaiKhoan ( UserName, Pass, LoaiTK, id )
	VALUES ( @username, @Pass, @loaiTK, @id )
END
GO

CREATE PROC USP_UpdateAccount
@username NVARCHAR(100), @loaiTK INT
AS
BEGIN
	UPDATE dbo.TaiKhoan 
	SET LoaiTK = @loaiTK
	WHERE UserName = @username
END
GO

CREATE PROC USP_ResetPasswordtAccount
@username NVARCHAR(100)
AS
BEGIN
	UPDATE dbo.TaiKhoan 
	SET Pass = '5512317111114510840231031535810616566202691' 
	WHERE UserName = @username
END
GO

--CREATE PROC USP_UpdatePasswordAccount
--@username NVARCHAR(100),
-- @inputpass NVARCHAR(100),
-- @newPass NVARCHAR(100),
-- @oldPass NVARCHAR(100)
--AS

--BEGIN
--	SELECT @oldPass = Pass
--	FROM TaiKhoan
--	WHERE UserName = @username
--	IF @oldPass = @inputpass
--		UPDATE dbo.TaiKhoan 
--		SET Pass = '5512317111114510840231031535810616566202691' 
--		WHERE UserName = @username	 	
--END
--GO
--DROP PROC USP_SearchAccount
CREATE PROC USP_SearchAccount
@name NVARCHAR(100)
AS
BEGIN
	SELECT TK.UserName AS [Username], TK.LoaiTK AS [Loại tài khoản], KH.id AS [ID], KH.HoTen AS [Họ và Tên]
	FROM dbo.TaiKhoan TK, dbo.KhachHang KH
	WHERE KH.id = TK.id AND dbo.fuConvertToUnsign1(KH.HoTen) LIKE N'%' + dbo.fuConvertToUnsign1(@name) + N'%'
END
GO


--DOANH THU
CREATE PROC USP_GetRevenueByMovieAndDate
@idMovie VARCHAR(50), @fromDate date, @toDate date
AS
BEGIN
	SELECT P.TenPhim AS [Tên phim], CONVERT(DATE, LC.ThoiGianChieu) AS [Ngày chiếu], CONVERT(TIME(0),LC.ThoiGianChieu) AS [Giờ chiếu], COUNT(V.id) AS [Số vé đã bán], SUM(TienBanVe) AS [Tiền vé]
	FROM dbo.Ve AS V, dbo.LichChieu AS LC, dbo.DinhDangPhim AS DDP, Phim AS P
	WHERE V.idLichChieu = LC.id AND LC.idDinhDang = DDP.id AND DDP.idPhim = P.id AND V.TrangThai = 1 AND P.id = @idMovie AND LC.ThoiGianChieu >= @fromDate AND LC.ThoiGianChieu <= @toDate
	GROUP BY idLichChieu, P.TenPhim, LC.ThoiGianChieu
END
GO

CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
GO

CREATE PROC USP_GetReportRevenueByMovieAndDate
@idMovie VARCHAR(50), @fromDate date, @toDate date
AS
BEGIN
	SELECT P.TenPhim, CONVERT(DATE, LC.ThoiGianChieu) AS NgayChieu, CONVERT(TIME(0),LC.ThoiGianChieu) AS GioChieu, COUNT(V.id) AS SoVeDaBan, SUM(TienBanVe) AS TienBanVe
	FROM dbo.Ve AS V, dbo.LichChieu AS LC, dbo.DinhDangPhim AS DDP, Phim AS P
	WHERE V.idLichChieu = LC.id AND LC.idDinhDang = DDP.id AND DDP.idPhim = P.id AND V.TrangThai = 1 AND P.id = @idMovie AND LC.ThoiGianChieu >= @fromDate AND LC.ThoiGianChieu <= @toDate
	GROUP BY idLichChieu, P.TenPhim, LC.ThoiGianChieu
END
GO

--KHÁCH HÀNG
CREATE PROC USP_GetCustomer
AS
BEGIN
	SELECT id AS [Mã khách hàng], HoTen AS [Họ tên], NgaySinh AS [Ngày sinh], DiaChi AS [Địa chỉ], SDT AS [SĐT], CMND AS [CMND]
	FROM dbo.KhachHang
END
GO

CREATE PROC USP_InsertCustomer
@idCus VARCHAR(50), @hoTen NVARCHAR(100), @ngaySinh date, @diaChi NVARCHAR(100), @sdt VARCHAR(100), @cmnd INT
AS
BEGIN
	INSERT dbo.KhachHang (id, HoTen, NgaySinh, DiaChi, SDT, CMND)
	VALUES (@idCus, @hoTen, @ngaySinh, @diaChi, @sdt, @cmnd)
END
GO

CREATE PROC USP_SearchCustomer
@ID NVARCHAR(100)
AS
BEGIN
	SELECT id AS [Mã khách hàng], HoTen AS [Họ tên], NgaySinh AS [Ngày sinh], DiaChi AS [Địa chỉ], SDT AS [SĐT], CMND AS [CMND]
	FROM dbo.KhachHang
	WHERE @ID = id
END
GO

--THỂ LOẠI
CREATE PROC USP_InsertGenre
@idGenre VARCHAR(50), @ten NVARCHAR(100), @moTa NVARCHAR(100)
AS
BEGIN
	INSERT dbo.TheLoai (id, TenTheLoai, MoTa)
	VALUES  (@idGenre, @ten, @moTa)
END
GO

--LOẠI MÀN HÌNH
CREATE PROC USP_InsertScreenType
@idScreenType VARCHAR(50), @ten NVARCHAR(100)
AS
BEGIN
	INSERT dbo.LoaiManHinh ( id, TenMH )
	VALUES  (@idScreenType, @ten)
END
GO

--PHIM

--Drop PROC USP_GetMovie
CREATE PROC USP_GetMovie
AS
BEGIN
	SELECT Phim.id AS [Mã phim], TenPhim AS [Tên phim], MoTa AS [Mô tả], 
	 ThoiLuong AS [Thời lượng], NgayKhoiChieu AS [Ngày khởi chiếu], 
	 NgayKetThuc AS [Ngày kết thúc], SanXuat AS [Sản xuất], 
	 DaoDien AS [Đạo diễn], NamSX AS [Năm SX], ApPhich AS [Áp Phích]
	FROM Phim	
END
GO

--Drop PROC USP_GetMovieHaveShowTime
CREATE PROC USP_GetMovieHaveShowTime
AS
BEGIN
	SELECT Phim.id AS [Mã phim], TenPhim AS [Tên phim], MoTa AS [Mô tả], 
	 ThoiLuong AS [Thời lượng], NgayKhoiChieu AS [Ngày khởi chiếu], 
	 NgayKetThuc AS [Ngày kết thúc], SanXuat AS [Sản xuất], 
	 DaoDien AS [Đạo diễn], NamSX AS [Năm SX], ApPhich AS [Áp Phích]
	FROM Phim
	WHERE Phim.id IN (SELECT  (Phim.id)
						FROM (Phim JOIN DinhDangPhim ON Phim.id = DinhDangPhim.idPhim) JOIN LichChieu ON DinhDangPhim.id = LichChieu.idDinhDang 
						WHERE LichChieu.TrangThai = 1)
END
GO

CREATE PROC USP_GetListGenreByMovie
@idPhim VARCHAR(50)
AS
BEGIN
	SELECT TL.id, TenTheLoai, TL.MoTa
	FROM dbo.PhanLoaiPhim PL, dbo.TheLoai TL
	WHERE idPhim = @idPhim AND PL.idTheLoai = TL.id
END
GO

CREATE PROC USP_InsertMovie
@id VARCHAR(50), @tenPhim NVARCHAR(100), @moTa NVARCHAR(1000), @thoiLuong FLOAT, @ngayKhoiChieu DATE, @ngayKetThuc DATE, @sanXuat NVARCHAR(50), @daoDien NVARCHAR(100), @namSX INT, @apPhich IMAGE
AS
BEGIN
	INSERT dbo.Phim (id , TenPhim , MoTa , ThoiLuong , NgayKhoiChieu , NgayKetThuc , SanXuat , DaoDien , NamSX, ApPhich)
	VALUES (@id , @tenPhim , @moTa , @thoiLuong , @ngayKhoiChieu , @ngayKetThuc , @sanXuat , @daoDien , @namSX, @apPhich)
END
GO

CREATE PROC USP_UpdateMovie
@id VARCHAR(50), @tenPhim NVARCHAR(100), @moTa NVARCHAR(1000), @thoiLuong FLOAT, @ngayKhoiChieu DATE, @ngayKetThuc DATE, @sanXuat NVARCHAR(50), @daoDien NVARCHAR(100), @namSX INT , @apPhich IMAGE
AS
BEGIN
	UPDATE dbo.Phim SET TenPhim = @tenPhim, MoTa = @moTa, ThoiLuong = @thoiLuong, NgayKhoiChieu = @ngayKhoiChieu, NgayKetThuc = @ngayKetThuc, SanXuat = @sanXuat, DaoDien = @daoDien, NamSX = @namSX, ApPhich = @apPhich WHERE id = @id
END
GO

--ĐỊNH DẠNG PHIM
CREATE PROC USP_GetListFormatMovie
AS
BEGIN
	SELECT DD.id AS [Mã định dạng], P.id AS [Mã phim], P.TenPhim AS [Tên phim], MH.id AS [Mã MH], MH.TenMH AS [Tên MH]
	FROM dbo.DinhDangPhim DD, Phim P, dbo.LoaiManHinh MH
	WHERE DD.idPhim = P.id AND DD.idLoaiManHinh = MH.id
END
GO

CREATE PROC USP_InsertFormatMovie
@id VARCHAR(50), @idPhim VARCHAR(50), @idLoaiManHinh VARCHAR(50)
AS
BEGIN
	INSERT dbo.DinhDangPhim ( id, idPhim, idLoaiManHinh )
	VALUES  ( @id, @idPhim, @idLoaiManHinh )
END
GO


--LỊCH CHIẾU
CREATE PROC USP_GetListShowTimesByFormatMovie @ID varchar(50), @Date Datetime
AS
BEGIN
	select l.id, pc.TenPhong, p.TenPhim, l.ThoiGianChieu, d.id as idDinhDang, l.GiaVe, l.TrangThai
	from Phim p ,DinhDangPhim d, LichChieu l, PhongChieu pc
	where p.id = d.idPhim and d.id = l.idDinhDang and l.idPhong = pc.id
	and d.id = @ID and CONVERT(DATE, @Date) = CONVERT(DATE, l.ThoiGianChieu)
	order by l.ThoiGianChieu
END
GO

--DROP PROC USP_GetShowtimeByIDMovie
CREATE PROC USP_GetShowtimeByIDMovie @ID varchar(50)
AS
BEGIN
	SELECT TenPhim AS [Tên phim],LichChieu.id AS [Mã lịch chiếu], ThoiGianChieu AS [Thời gian chiếu], GiaVe AS [Giá vé], LichChieu.idPhong AS [ID Phòng Chiếu],PhongChieu.TenPhong AS [Tên Phòng Chiếu]
	FROM Phim ,DinhDangPhim ,LichChieu,PhongChieu
	WHERE Phim.id = @ID AND 
			DinhDangPhim.idPhim = @ID AND 
			LichChieu.idDinhDang = DinhDangPhim.id AND
			LichChieu.idPhong = PhongChieu.id AND
			LichChieu.TrangThai = 1
END
GO


--DROP PROC USP_GetShowtime
CREATE PROC USP_GetShowtime
AS
BEGIN
	SELECT LC.id AS [Mã lịch chiếu], LC.idPhong AS [Mã phòng], P.TenPhim AS [Tên phim], MH.TenMH AS [Màn hình], LC.ThoiGianChieu AS [Thời gian chiếu], LC.GiaVe AS [Giá vé]
	FROM dbo.LichChieu AS LC, dbo.DinhDangPhim AS DD, Phim AS P, dbo.LoaiManHinh AS MH
	WHERE LC.idDinhDang = DD.id AND DD.idPhim = P.id AND DD.idLoaiManHinh = MH.id
END
GO


--DROP PROC USP_GetShowtimeByIDShowTimeAndIDMovie
CREATE PROC USP_GetShowtimeByIDShowTimeAndIDMovie @IDShowTime varchar(50), @IDMovie varchar(50)
AS
BEGIN
	SELECT  LichChieu.id, LichChieu.idPhong AS [TenPhong],TenPhim, ThoiGianChieu,LichChieu.idDinhDang, GiaVe,TrangThai
	FROM Phim ,DinhDangPhim ,LichChieu
	WHERE Phim.id = @IDMovie AND 
			DinhDangPhim.idPhim = @IDMovie AND 
			LichChieu.idDinhDang = DinhDangPhim.id AND
			@IDShowTime = LichChieu.id
END
GO


--DROP PROC USP_GetShowtimeByIDShowTime
CREATE PROC USP_GetShowtimeByIDShowTime @ID varchar(50)
AS
BEGIN
	SELECT LichChieu.id AS [Mã lịch chiếu], idPhong AS [Mã phòng],  ThoiGianChieu AS [Thời gian chiếu], GiaVe AS [Giá vé]
	FROM LichChieu
	WHERE LichChieu.id = @ID  			
END
GO

CREATE PROC USP_InsertShowtime
@id VARCHAR(50), @idPhong VARCHAR(50), @idDinhDang VARCHAR(50), @thoiGianChieu DATETIME, @giaVe FLOAT
AS
BEGIN
	INSERT dbo.LichChieu ( id , idPhong , idDinhDang, ThoiGianChieu  , GiaVe , TrangThai )
	VALUES  ( @id , @idPhong , @idDinhDang, @thoiGianChieu  , @giaVe , 0 )
END
GO

CREATE PROC USP_UpdateShowtime
@id VARCHAR(50), @idPhong VARCHAR(50), @idDinhDang VARCHAR(50), @thoiGianChieu DATETIME, @giaVe FLOAT
AS
BEGIN
	UPDATE dbo.LichChieu 
	SET idPhong = @idPhong, idDinhDang = @idDinhDang, ThoiGianChieu = @thoiGianChieu , GiaVe = @giaVe
	WHERE id = @id
END
GO

CREATE PROC USP_SearchShowtimeByMovieName
@tenPhim NVARCHAR(100)
AS
BEGIN
	SELECT LC.id AS [Mã lịch chiếu], LC.idPhong AS [Mã phòng], P.TenPhim AS [Tên phim], MH.TenMH AS [Màn hình], LC.ThoiGianChieu AS [Thời gian chiếu], LC.GiaVe AS [Giá vé]
	FROM dbo.LichChieu AS LC, dbo.DinhDangPhim AS DD, Phim AS P, dbo.LoaiManHinh AS MH
	WHERE LC.idDinhDang = DD.id AND DD.idPhim = P.id AND DD.idLoaiManHinh = MH.id AND dbo.fuConvertToUnsign1(P.TenPhim) LIKE N'%' + dbo.fuConvertToUnsign1(@tenPhim) + N'%'
END
GO

CREATE PROC USP_GetAllListShowTimes
AS
BEGIN
	select l.id, pc.TenPhong, p.TenPhim, l.ThoiGianChieu, d.id as idDinhDang, l.GiaVe, l.TrangThai
	from Phim p ,DinhDangPhim d, LichChieu l, PhongChieu pc
	where p.id = d.idPhim and d.id = l.idDinhDang and l.idPhong = pc.id
	order by l.ThoiGianChieu
END
GO

CREATE PROC USP_GetListShowTimesNotCreateTickets
AS
BEGIN
	select l.id, pc.TenPhong, p.TenPhim, l.ThoiGianChieu, d.id as idDinhDang, l.GiaVe, l.TrangThai
	from Phim p ,DinhDangPhim d, LichChieu l, PhongChieu pc
	where p.id = d.idPhim and d.id = l.idDinhDang and l.idPhong = pc.id and l.TrangThai = 0
	order by l.ThoiGianChieu
END
GO



CREATE PROC USP_UpdateStatusShowTimes
@idLichChieu NVARCHAR(50), @status int
AS
BEGIN
	UPDATE dbo.LichChieu
	SET TrangThai = @status
	WHERE id = @idLichChieu
END
GO


--PHÒNG CHIẾU
CREATE PROC USP_GetCinema
AS
BEGIN
	SELECT PC.id AS [Mã phòng], TenPhong AS [Tên phòng], TenMH AS [Tên màn hình], PC.SoChoNgoi AS [Số chỗ ngồi], PC.TinhTrang AS [Tình trạng], PC.SoHangGhe AS [Số hàng ghế], PC.SoGheMotHang AS [Ghế mỗi hàng]
	FROM dbo.PhongChieu AS PC, dbo.LoaiManHinh AS MH
	WHERE PC.idManHinh = MH.id
END
GO

CREATE PROC USP_InsertCinema
@idCinema VARCHAR(50), @tenPhong NVARCHAR(100), @idMH VARCHAR(50), @soChoNgoi INT, @tinhTrang INT, @soHangGhe INT, @soGheMotHang INT
AS
BEGIN
	INSERT dbo.PhongChieu ( id , TenPhong , idManHinh , SoChoNgoi , TinhTrang , SoHangGhe , SoGheMotHang)
	VALUES (@idCinema , @tenPhong , @idMH , @soChoNgoi , @tinhTrang , @soHangGhe , @soGheMotHang)
END
GO


--VÉ
CREATE PROC USP_InsertTicketByShowTimes
@idlichChieu VARCHAR(50), @maGheNgoi VARCHAR(50)
AS
BEGIN
	INSERT INTO dbo.Ve
	(
		idLichChieu,
		MaGheNgoi,
		idKhachHang
	)
	VALUES
	(
		@idlichChieu,
		@maGheNgoi,
		NULL
	)
END
GO

CREATE PROC USP_DeleteTicketsByShowTimes
@idlichChieu VARCHAR(50)
AS
BEGIN
	DELETE FROM dbo.Ve
	WHERE idLichChieu = @idlichChieu
END
GO

CREATE PROC USP_GetInfoForTicket
@idVe nvarchar(50)
AS
BEGIN
	SELECT ThoiGianChieu, TienBanVe, TenPhim, MaGheNgoi, Ve.id, Ve.idKhachHang,TenPhong
	FROM [dbo].LichChieu,[dbo].Ve,[dbo].Phim,[dbo].DinhDangPhim,[dbo].PhongChieu
	WHERE @idVe = Ve.id AND Ve.idLichChieu = LichChieu.id 
											AND LichChieu.idDinhDang = DinhDangPhim.id 
											AND DinhDangPhim.idPhim = Phim.id
											AND LichChieu.idPhong = PhongChieu.id
END
GO 

--Tin Nhan
--Drop PROC USP_InsertMessage
CREATE PROC USP_InsertMessage
@body nvarchar(500), @user_from nvarchar(500), @user_to nvarchar(500), @date_sent datetime
AS
BEGIN
	INSERT [dbo].Messages( body, user_from, user_to, date_sent)
	VALUES (@body, @user_from, @user_to,@date_sent)
END

GO

CREATE PROC USP_GetMessage
@username nvarchar(50)
AS
BEGIN
	SELECT user_from AS [Người gửi], user_to AS [Người nhận], date_sent AS [Thời gian gửi],body AS [Nội dung]
	FROM [dbo].Messages
	WHERE @username = user_to OR user_from = @username
END
GO

CREATE PROC USP_DeleteMessage
@username nvarchar(50)
AS
BEGIN
	DELETE FROM Messages
	WHERE @username = user_to OR user_from = @username
END
GO

CREATE PROC USP_GetCusNeedSupportForAdmin
AS
BEGIN 
	 SELECT  user_from AS [Khách Hàng Cần Hỗ Trợ], date_sent AS [Thời gian gửi]
		FROM    (SELECT id_msg, user_from,date_sent, ROW_NUMBER() OVER (PARTITION BY user_from ORDER BY id_msg) AS RowNumber
                
         FROM   [dbo].Messages
         WHERE  user_to = 'admin') AS a
		WHERE   a.RowNumber = 1
END
GO


CREATE PROC USP_GetMessageForAdmin
@username_from nvarchar(50)
AS
BEGIN
	SELECT user_from AS [Người gửi], user_to AS [Người nhận], date_sent AS [Thời gian gửi],body AS [Nội dung]
	FROM [dbo].Messages
	WHERE user_to = N'admin' AND user_from = @username_from
END
GO



--Insert Dữ Liệu
SET IDENTITY_INSERT [dbo].[Messages] ON
GO
SET DATEFORMAT YMD
INSERT INTO [Messages] ([id_msg], [body], [user_from], [user_to], [date_sent]) VALUES (1, N'Hello', N'Tai',   N'admin', '2022-05-26 08:50:00.000');
INSERT INTO [Messages] ([id_msg], [body], [user_from], [user_to], [date_sent]) VALUES (2, N'Hello', N'Tai',   N'admin', '2022-05-26 08:50:00.000');
INSERT INTO [Messages] ([id_msg], [body], [user_from], [user_to], [date_sent]) VALUES (3, N'Hello', N'admin', N'Tai',   '2022-05-26 08:50:00.000');
INSERT INTO [Messages] ([id_msg], [body], [user_from], [user_to], [date_sent]) VALUES (4, N'Hello', N'admin', N'Tai',   '2022-05-26 08:50:00.000');
INSERT INTO [Messages] ([id_msg], [body], [user_from], [user_to], [date_sent]) VALUES (5, N'Hello', N'admin', N'Bao',   '2022-05-26 08:50:00.000');
INSERT INTO [Messages] ([id_msg], [body], [user_from], [user_to], [date_sent]) VALUES (6, N'Hello', N'admin', N'Bao',   '2022-05-26 08:50:00.000');
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO


INSERT INTO [TheLoai] ([id], [TenTheLoai], [MoTa]) VALUES ('TL01', N'Hành Động', NULL);  
INSERT INTO [TheLoai] ([id], [TenTheLoai], [MoTa]) VALUES ('TL02', N'Hoạt Hình', NULL);  
INSERT INTO [TheLoai] ([id], [TenTheLoai], [MoTa]) VALUES ('TL03', N'Hài', NULL);  
INSERT INTO [TheLoai] ([id], [TenTheLoai], [MoTa]) VALUES ('TL04', N'Viễn Tưởng', NULL);  
INSERT INTO [TheLoai] ([id], [TenTheLoai], [MoTa]) VALUES ('TL05', N'Phiêu lưu', NULL);  
INSERT INTO [TheLoai] ([id], [TenTheLoai], [MoTa]) VALUES ('TL06', N'Gia đình', NULL);  
INSERT INTO [TheLoai] ([id], [TenTheLoai], [MoTa]) VALUES ('TL07', N'Tình Cảm', NULL);  
INSERT INTO [TheLoai] ([id], [TenTheLoai], [MoTa]) VALUES ('TL08', N'Tâm Lý', NULL);  


INSERT INTO [KhachHang] ([id], [HoTen], [NgaySinh], [DiaChi], [SDT], [CMND]) VALUES ('ADMIN', N'admin', '2001-05-03', N'...', '0123456789', 100000000);  
INSERT INTO [KhachHang] ([id], [HoTen], [NgaySinh], [DiaChi], [SDT], [CMND]) VALUES ('KH01', N'Nguyen Anh Tai', '2002-04-11', N'DakSak', '091237123', 24512345);  
INSERT INTO [KhachHang] ([id], [HoTen], [NgaySinh], [DiaChi], [SDT], [CMND]) VALUES ('KH02', N'Nguyễn Văn Bao', '2001-02-03', N'Bla Bla', '0123456789', 218161565);  
INSERT INTO [KhachHang] ([id], [HoTen], [NgaySinh], [DiaChi], [SDT], [CMND]) VALUES ('KH03', N'Nguyễn Văn DucAnh', '2000-05-03', N'Bla Bla', '0123456789', 218161564);  
INSERT INTO [KhachHang] ([id], [HoTen], [NgaySinh], [DiaChi], [SDT], [CMND]) VALUES ('KH04', N'Nguyễn Văn MaiLy', '2001-05-06', N'Bla Bla', '0123456789', 218161657);  
INSERT INTO [KhachHang] ([id], [HoTen], [NgaySinh], [DiaChi], [SDT], [CMND]) VALUES ('KH05', N'Nguyễn Văn Hoan', '2001-05-03', N'Bla Bla', '0123456789', 218161654);  


INSERT INTO [TaiKhoan] ([UserName], [Pass], [LoaiTK], [id]) VALUES (N'admin', '59113821474147731767615617822114745333', 1, 'ADMIN');  
INSERT INTO [TaiKhoan] ([UserName], [Pass], [LoaiTK], [id]) VALUES (N'Tai', '2115753541275705119798271895814423', 2, 'KH01');  
INSERT INTO [TaiKhoan] ([UserName], [Pass], [LoaiTK], [id]) VALUES (N'Bao', '2115753541275705119798271895814423', 2, 'KH02'); 
INSERT INTO [TaiKhoan] ([UserName], [Pass], [LoaiTK], [id]) VALUES (N'DucAnh', '2115753541275705119798271895814423', 2, 'KH03'); 
INSERT INTO [TaiKhoan] ([UserName], [Pass], [LoaiTK], [id]) VALUES (N'MaiLy', '2115753541275705119798271895814423', 2, 'KH04');
INSERT INTO [TaiKhoan] ([UserName], [Pass], [LoaiTK], [id]) VALUES (N'Hoan', '2115753541275705119798271895814423', 2, 'KH05');


INSERT INTO [LoaiManHinh] ([id], [TenMH]) VALUES ('MH01', N'2D');  
INSERT INTO [LoaiManHinh] ([id], [TenMH]) VALUES ('MH02', N'3D');  
INSERT INTO [LoaiManHinh] ([id], [TenMH]) VALUES ('MH03', N'IMAX');  
INSERT INTO [LoaiManHinh] ([id], [TenMH]) VALUES ('MH04', N'4D');  

INSERT INTO [PhongChieu] ([id], [TenPhong], [idManHinh], [SoChoNgoi], [TinhTrang], [SoHangGhe], [SoGheMotHang]) VALUES ('PC01', N'CINEMA 01', 'MH01', 140, 1, 10, 14);  
INSERT INTO [PhongChieu] ([id], [TenPhong], [idManHinh], [SoChoNgoi], [TinhTrang], [SoHangGhe], [SoGheMotHang]) VALUES ('PC02', N'CINEMA 02', 'MH01', 140, 1, 10, 14);  
INSERT INTO [PhongChieu] ([id], [TenPhong], [idManHinh], [SoChoNgoi], [TinhTrang], [SoHangGhe], [SoGheMotHang]) VALUES ('PC03', N'CINEMA 03', 'MH01', 140, 1, 10, 14);  
INSERT INTO [PhongChieu] ([id], [TenPhong], [idManHinh], [SoChoNgoi], [TinhTrang], [SoHangGhe], [SoGheMotHang]) VALUES ('PC04', N'CINEMA 04', 'MH01', 140, 1, 10, 14);  

SET DATEFORMAT YMD;
INSERT INTO [Phim] ([id], [TenPhim], [MoTa], [ThoiLuong], [NgayKhoiChieu], [NgayKetThuc], [SanXuat], [DaoDien], [NamSX], [ApPhich]) VALUES ('P01', N'Avengers: Cuộc Chiến Vô Cực', N'Avengers: Infinity War', 150, '2022-05-26', '2022-09-01', N'Mỹ', N'Anthony Russo,  Joe Russo', 2021, NULL);  
INSERT INTO [Phim] ([id], [TenPhim], [MoTa], [ThoiLuong], [NgayKhoiChieu], [NgayKetThuc], [SanXuat], [DaoDien], [NamSX], [ApPhich]) VALUES ('P02', N'Lật Mặt: 3 Chàng Khuyết', N'Lat Mat 3 Chang Khuyet', 100, '2022-05-26', '2022-09-01', N'Việt Nam', N'Lý Hải', 2021, NULL);  
INSERT INTO [Phim] ([id], [TenPhim], [MoTa], [ThoiLuong], [NgayKhoiChieu], [NgayKetThuc], [SanXuat], [DaoDien], [NamSX], [ApPhich]) VALUES ('P03', N'100 Ngày Bên Em', N'', 100, '2022-05-26', '2022-09-01', N'Việt Nam', N'Vũ Ngọc Phượng', 2021, NULL);  
INSERT INTO [Phim] ([id], [TenPhim], [MoTa], [ThoiLuong], [NgayKhoiChieu], [NgayKetThuc], [SanXuat], [DaoDien], [NamSX], [ApPhich]) VALUES ('P04', N'Ngỗng Vịt Phiêu Lưu Ký', N'Duck Duck Goose', 91, '2022-05-26', '2022-09-01', N'Mỹ', N'Christopher Jenkins', 2021, NULL);  

INSERT INTO [PhanLoaiPhim] ([idPhim], [idTheLoai]) VALUES ('P01', 'TL01');  
INSERT INTO [PhanLoaiPhim] ([idPhim], [idTheLoai]) VALUES ('P01', 'TL04');  
INSERT INTO [PhanLoaiPhim] ([idPhim], [idTheLoai]) VALUES ('P01', 'TL05');  
INSERT INTO [PhanLoaiPhim] ([idPhim], [idTheLoai]) VALUES ('P02', 'TL01');  
INSERT INTO [PhanLoaiPhim] ([idPhim], [idTheLoai]) VALUES ('P02', 'TL07');  
INSERT INTO [PhanLoaiPhim] ([idPhim], [idTheLoai]) VALUES ('P02', 'TL08');  
INSERT INTO [PhanLoaiPhim] ([idPhim], [idTheLoai]) VALUES ('P03', 'TL03');  
INSERT INTO [PhanLoaiPhim] ([idPhim], [idTheLoai]) VALUES ('P03', 'TL07');  
INSERT INTO [PhanLoaiPhim] ([idPhim], [idTheLoai]) VALUES ('P03', 'TL08');  
INSERT INTO [PhanLoaiPhim] ([idPhim], [idTheLoai]) VALUES ('P04', 'TL02');  
INSERT INTO [PhanLoaiPhim] ([idPhim], [idTheLoai]) VALUES ('P04', 'TL03');  
INSERT INTO [PhanLoaiPhim] ([idPhim], [idTheLoai]) VALUES ('P04', 'TL05');  

INSERT INTO [DinhDangPhim] ([id], [idPhim], [idLoaiManHinh]) VALUES ('DD01', 'P01', 'MH01');  
INSERT INTO [DinhDangPhim] ([id], [idPhim], [idLoaiManHinh]) VALUES ('DD02', 'P02', 'MH01');  
INSERT INTO [DinhDangPhim] ([id], [idPhim], [idLoaiManHinh]) VALUES ('DD03', 'P03', 'MH01');  
INSERT INTO [DinhDangPhim] ([id], [idPhim], [idLoaiManHinh]) VALUES ('DD04', 'P04', 'MH01');  

INSERT INTO [LichChieu] ([id], [ThoiGianChieu], [idPhong], [idDinhDang], [GiaVe], [TrangThai]) VALUES ('LC01', '2022-05-26 08:50:00.000', 'PC01', 'DD01', 85000.0000, 1);  
INSERT INTO [LichChieu] ([id], [ThoiGianChieu], [idPhong], [idDinhDang], [GiaVe], [TrangThai]) VALUES ('LC02', '2022-05-26 08:05:00.000', 'PC02', 'DD02', 85000.0000, 1);  
INSERT INTO [LichChieu] ([id], [ThoiGianChieu], [idPhong], [idDinhDang], [GiaVe], [TrangThai]) VALUES ('LC03', '2022-05-26 08:10:00.000', 'PC03', 'DD03', 85000.0000, 1);  
INSERT INTO [LichChieu] ([id], [ThoiGianChieu], [idPhong], [idDinhDang], [GiaVe], [TrangThai]) VALUES ('LC04', '2022-05-26 09:20:00.000', 'PC04', 'DD04', 85000.0000, 0);  


SET IDENTITY_INSERT [dbo].[Ve] ON
GO
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (141, 0, 'LC02', 'A1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (142, 0, 'LC02', 'A2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (143, 0, 'LC02', 'A3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (144, 0, 'LC02', 'A4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (145, 0, 'LC02', 'A5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (146, 0, 'LC02', 'A6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (147, 0, 'LC02', 'A7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (148, 0, 'LC02', 'A8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (149, 0, 'LC02', 'A9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (150, 0, 'LC02', 'A10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (151, 0, 'LC02', 'A11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (152, 0, 'LC02', 'A12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (153, 0, 'LC02', 'A13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (154, 0, 'LC02', 'A14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (155, 0, 'LC02', 'B1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (156, 0, 'LC02', 'B2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (157, 0, 'LC02', 'B3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (158, 0, 'LC02', 'B4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (159, 0, 'LC02', 'B5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (160, 0, 'LC02', 'B6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (161, 0, 'LC02', 'B7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (162, 0, 'LC02', 'B8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (163, 0, 'LC02', 'B9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (164, 0, 'LC02', 'B10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (165, 0, 'LC02', 'B11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (166, 0, 'LC02', 'B12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (167, 0, 'LC02', 'B13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (168, 0, 'LC02', 'B14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (169, 0, 'LC02', 'C1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (170, 0, 'LC02', 'C2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (171, 0, 'LC02', 'C3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (172, 0, 'LC02', 'C4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (173, 0, 'LC02', 'C5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (174, 0, 'LC02', 'C6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (175, 0, 'LC02', 'C7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (176, 0, 'LC02', 'C8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (177, 0, 'LC02', 'C9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (178, 0, 'LC02', 'C10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (179, 0, 'LC02', 'C11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (180, 0, 'LC02', 'C12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (181, 0, 'LC02', 'C13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (182, 0, 'LC02', 'C14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (183, 0, 'LC02', 'D1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (184, 0, 'LC02', 'D2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (185, 0, 'LC02', 'D3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (186, 0, 'LC02', 'D4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (187, 0, 'LC02', 'D5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (188, 0, 'LC02', 'D6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (189, 0, 'LC02', 'D7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (190, 0, 'LC02', 'D8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (191, 0, 'LC02', 'D9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (192, 0, 'LC02', 'D10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (193, 0, 'LC02', 'D11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (194, 0, 'LC02', 'D12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (195, 0, 'LC02', 'D13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (196, 0, 'LC02', 'D14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (197, 0, 'LC02', 'E1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (198, 0, 'LC02', 'E2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (199, 0, 'LC02', 'E3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (200, 0, 'LC02', 'E4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (201, 0, 'LC02', 'E5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (202, 0, 'LC02', 'E6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (203, 0, 'LC02', 'E7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (204, 0, 'LC02', 'E8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (205, 0, 'LC02', 'E9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (206, 0, 'LC02', 'E10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (207, 0, 'LC02', 'E11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (208, 0, 'LC02', 'E12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (209, 0, 'LC02', 'E13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (210, 0, 'LC02', 'E14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (211, 0, 'LC02', 'F1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (212, 0, 'LC02', 'F2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (213, 0, 'LC02', 'F3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (214, 0, 'LC02', 'F4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (215, 0, 'LC02', 'F5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (216, 0, 'LC02', 'F6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (217, 0, 'LC02', 'F7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (218, 0, 'LC02', 'F8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (219, 0, 'LC02', 'F9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (220, 0, 'LC02', 'F10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (221, 0, 'LC02', 'F11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (222, 0, 'LC02', 'F12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (223, 0, 'LC02', 'F13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (224, 0, 'LC02', 'F14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (225, 0, 'LC02', 'G1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (226, 0, 'LC02', 'G2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (227, 0, 'LC02', 'G3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (228, 0, 'LC02', 'G4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (229, 0, 'LC02', 'G5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (230, 0, 'LC02', 'G6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (231, 0, 'LC02', 'G7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (232, 0, 'LC02', 'G8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (233, 0, 'LC02', 'G9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (234, 0, 'LC02', 'G10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (235, 0, 'LC02', 'G11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (236, 0, 'LC02', 'G12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (237, 0, 'LC02', 'G13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (238, 0, 'LC02', 'G14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (239, 0, 'LC02', 'H1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (240, 0, 'LC02', 'H2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (241, 0, 'LC02', 'H3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (242, 0, 'LC02', 'H4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (243, 0, 'LC02', 'H5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (244, 0, 'LC02', 'H6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (245, 0, 'LC02', 'H7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (246, 0, 'LC02', 'H8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (247, 0, 'LC02', 'H9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (248, 0, 'LC02', 'H10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (249, 0, 'LC02', 'H11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (250, 0, 'LC02', 'H12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (251, 0, 'LC02', 'H13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (252, 0, 'LC02', 'H14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (253, 0, 'LC02', 'I1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (254, 0, 'LC02', 'I2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (255, 0, 'LC02', 'I3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (256, 0, 'LC02', 'I4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (257, 0, 'LC02', 'I5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (258, 0, 'LC02', 'I6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (259, 0, 'LC02', 'I7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (260, 0, 'LC02', 'I8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (261, 0, 'LC02', 'I9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (262, 0, 'LC02', 'I10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (263, 0, 'LC02', 'I11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (264, 0, 'LC02', 'I12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (265, 0, 'LC02', 'I13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (266, 0, 'LC02', 'I14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (267, 0, 'LC02', 'J1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (268, 0, 'LC02', 'J2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (269, 0, 'LC02', 'J3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (270, 0, 'LC02', 'J4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (271, 0, 'LC02', 'J5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (272, 0, 'LC02', 'J6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (273, 0, 'LC02', 'J7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (274, 0, 'LC02', 'J8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (275, 0, 'LC02', 'J9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (276, 0, 'LC02', 'J10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (277, 0, 'LC02', 'J11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (278, 0, 'LC02', 'J12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (279, 0, 'LC02', 'J13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (280, 0, 'LC02', 'J14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (281, 0, 'LC03', 'A1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (282, 0, 'LC03', 'A2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (283, 0, 'LC03', 'A3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (284, 0, 'LC03', 'A4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (285, 0, 'LC03', 'A5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (286, 0, 'LC03', 'A6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (287, 0, 'LC03', 'A7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (288, 0, 'LC03', 'A8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (289, 0, 'LC03', 'A9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (290, 0, 'LC03', 'A10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (291, 0, 'LC03', 'A11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (292, 0, 'LC03', 'A12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (293, 0, 'LC03', 'A13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (294, 0, 'LC03', 'A14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (295, 0, 'LC03', 'B1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (296, 0, 'LC03', 'B2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (297, 0, 'LC03', 'B3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (298, 0, 'LC03', 'B4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (299, 0, 'LC03', 'B5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (300, 0, 'LC03', 'B6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (301, 0, 'LC03', 'B7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (302, 0, 'LC03', 'B8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (303, 0, 'LC03', 'B9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (304, 0, 'LC03', 'B10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (305, 0, 'LC03', 'B11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (306, 0, 'LC03', 'B12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (307, 0, 'LC03', 'B13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (308, 0, 'LC03', 'B14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (309, 0, 'LC03', 'C1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (310, 0, 'LC03', 'C2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (311, 0, 'LC03', 'C3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (312, 0, 'LC03', 'C4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (313, 0, 'LC03', 'C5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (314, 0, 'LC03', 'C6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (315, 0, 'LC03', 'C7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (316, 0, 'LC03', 'C8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (317, 0, 'LC03', 'C9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (318, 0, 'LC03', 'C10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (319, 0, 'LC03', 'C11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (320, 0, 'LC03', 'C12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (321, 0, 'LC03', 'C13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (322, 0, 'LC03', 'C14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (323, 0, 'LC03', 'D1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (324, 0, 'LC03', 'D2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (325, 0, 'LC03', 'D3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (326, 0, 'LC03', 'D4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (327, 0, 'LC03', 'D5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (328, 0, 'LC03', 'D6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (329, 0, 'LC03', 'D7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (330, 0, 'LC03', 'D8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (331, 0, 'LC03', 'D9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (332, 0, 'LC03', 'D10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (333, 0, 'LC03', 'D11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (334, 0, 'LC03', 'D12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (335, 0, 'LC03', 'D13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (336, 0, 'LC03', 'D14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (337, 0, 'LC03', 'E1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (338, 0, 'LC03', 'E2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (339, 0, 'LC03', 'E3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (340, 0, 'LC03', 'E4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (341, 0, 'LC03', 'E5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (342, 0, 'LC03', 'E6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (343, 0, 'LC03', 'E7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (344, 0, 'LC03', 'E8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (345, 0, 'LC03', 'E9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (346, 0, 'LC03', 'E10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (347, 0, 'LC03', 'E11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (348, 0, 'LC03', 'E12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (349, 0, 'LC03', 'E13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (350, 0, 'LC03', 'E14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (351, 0, 'LC03', 'F1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (352, 0, 'LC03', 'F2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (353, 0, 'LC03', 'F3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (354, 0, 'LC03', 'F4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (355, 0, 'LC03', 'F5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (356, 0, 'LC03', 'F6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (357, 0, 'LC03', 'F7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (358, 0, 'LC03', 'F8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (359, 0, 'LC03', 'F9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (360, 0, 'LC03', 'F10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (361, 0, 'LC03', 'F11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (362, 0, 'LC03', 'F12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (363, 0, 'LC03', 'F13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (364, 0, 'LC03', 'F14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (365, 0, 'LC03', 'G1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (366, 0, 'LC03', 'G2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (367, 0, 'LC03', 'G3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (368, 0, 'LC03', 'G4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (369, 0, 'LC03', 'G5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (370, 0, 'LC03', 'G6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (371, 0, 'LC03', 'G7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (372, 0, 'LC03', 'G8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (373, 0, 'LC03', 'G9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (374, 0, 'LC03', 'G10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (375, 0, 'LC03', 'G11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (376, 0, 'LC03', 'G12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (377, 0, 'LC03', 'G13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (378, 0, 'LC03', 'G14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (379, 0, 'LC03', 'H1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (380, 0, 'LC03', 'H2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (381, 0, 'LC03', 'H3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (382, 0, 'LC03', 'H4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (383, 0, 'LC03', 'H5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (384, 0, 'LC03', 'H6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (385, 0, 'LC03', 'H7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (386, 0, 'LC03', 'H8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (387, 0, 'LC03', 'H9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (388, 0, 'LC03', 'H10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (389, 0, 'LC03', 'H11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (390, 0, 'LC03', 'H12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (391, 0, 'LC03', 'H13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (392, 0, 'LC03', 'H14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (393, 0, 'LC03', 'I1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (394, 0, 'LC03', 'I2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (395, 0, 'LC03', 'I3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (396, 0, 'LC03', 'I4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (397, 0, 'LC03', 'I5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (398, 0, 'LC03', 'I6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (399, 0, 'LC03', 'I7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (400, 0, 'LC03', 'I8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (401, 0, 'LC03', 'I9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (402, 0, 'LC03', 'I10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (403, 0, 'LC03', 'I11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (404, 0, 'LC03', 'I12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (405, 0, 'LC03', 'I13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (406, 0, 'LC03', 'I14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (407, 0, 'LC03', 'J1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (408, 0, 'LC03', 'J2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (409, 0, 'LC03', 'J3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (410, 0, 'LC03', 'J4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (411, 0, 'LC03', 'J5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (412, 0, 'LC03', 'J6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (413, 0, 'LC03', 'J7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (414, 0, 'LC03', 'J8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (415, 0, 'LC03', 'J9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (416, 0, 'LC03', 'J10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (417, 0, 'LC03', 'J11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (418, 0, 'LC03', 'J12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (419, 0, 'LC03', 'J13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (420, 0, 'LC03', 'J14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (421, 0, 'LC01', 'A1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (422, 0, 'LC01', 'A2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (423, 0, 'LC01', 'A3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (424, 0, 'LC01', 'A4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (425, 0, 'LC01', 'A5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (426, 0, 'LC01', 'A6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (427, 0, 'LC01', 'A7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (428, 0, 'LC01', 'A8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (429, 0, 'LC01', 'A9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (430, 0, 'LC01', 'A10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (431, 0, 'LC01', 'A11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (432, 0, 'LC01', 'A12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (433, 0, 'LC01', 'A13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (434, 0, 'LC01', 'A14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (435, 0, 'LC01', 'B1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (436, 0, 'LC01', 'B2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (437, 0, 'LC01', 'B3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (438, 0, 'LC01', 'B4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (439, 0, 'LC01', 'B5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (440, 0, 'LC01', 'B6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (441, 0, 'LC01', 'B7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (442, 0, 'LC01', 'B8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (443, 0, 'LC01', 'B9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (444, 0, 'LC01', 'B10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (445, 0, 'LC01', 'B11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (446, 0, 'LC01', 'B12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (447, 0, 'LC01', 'B13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (448, 0, 'LC01', 'B14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (449, 0, 'LC01', 'C1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (450, 0, 'LC01', 'C2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (451, 0, 'LC01', 'C3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (452, 0, 'LC01', 'C4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (453, 0, 'LC01', 'C5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (454, 0, 'LC01', 'C6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (455, 0, 'LC01', 'C7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (456, 0, 'LC01', 'C8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (457, 0, 'LC01', 'C9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (458, 0, 'LC01', 'C10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (459, 0, 'LC01', 'C11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (460, 0, 'LC01', 'C12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (461, 0, 'LC01', 'C13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (462, 0, 'LC01', 'C14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (463, 0, 'LC01', 'D1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (464, 0, 'LC01', 'D2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (465, 0, 'LC01', 'D3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (466, 0, 'LC01', 'D4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (467, 0, 'LC01', 'D5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (468, 0, 'LC01', 'D6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (469, 0, 'LC01', 'D7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (470, 0, 'LC01', 'D8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (471, 0, 'LC01', 'D9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (472, 0, 'LC01', 'D10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (473, 0, 'LC01', 'D11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (474, 0, 'LC01', 'D12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (475, 0, 'LC01', 'D13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (476, 0, 'LC01', 'D14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (477, 0, 'LC01', 'E1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (478, 0, 'LC01', 'E2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (479, 0, 'LC01', 'E3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (480, 0, 'LC01', 'E4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (481, 0, 'LC01', 'E5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (482, 0, 'LC01', 'E6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (483, 0, 'LC01', 'E7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (484, 0, 'LC01', 'E8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (485, 0, 'LC01', 'E9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (486, 0, 'LC01', 'E10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (487, 0, 'LC01', 'E11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (488, 0, 'LC01', 'E12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (489, 0, 'LC01', 'E13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (490, 0, 'LC01', 'E14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (491, 0, 'LC01', 'F1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (492, 0, 'LC01', 'F2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (493, 0, 'LC01', 'F3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (494, 0, 'LC01', 'F4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (495, 0, 'LC01', 'F5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (496, 0, 'LC01', 'F6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (497, 0, 'LC01', 'F7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (498, 0, 'LC01', 'F8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (499, 0, 'LC01', 'F9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (500, 0, 'LC01', 'F10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (501, 0, 'LC01', 'F11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (502, 0, 'LC01', 'F12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (503, 0, 'LC01', 'F13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (504, 0, 'LC01', 'F14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (505, 0, 'LC01', 'G1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (506, 0, 'LC01', 'G2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (507, 0, 'LC01', 'G3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (508, 0, 'LC01', 'G4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (509, 0, 'LC01', 'G5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (510, 0, 'LC01', 'G6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (511, 0, 'LC01', 'G7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (512, 0, 'LC01', 'G8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (513, 0, 'LC01', 'G9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (514, 0, 'LC01', 'G10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (515, 0, 'LC01', 'G11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (516, 0, 'LC01', 'G12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (517, 0, 'LC01', 'G13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (518, 0, 'LC01', 'G14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (519, 0, 'LC01', 'H1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (520, 0, 'LC01', 'H2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (521, 0, 'LC01', 'H3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (522, 0, 'LC01', 'H4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (523, 0, 'LC01', 'H5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (524, 0, 'LC01', 'H6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (525, 0, 'LC01', 'H7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (526, 0, 'LC01', 'H8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (527, 0, 'LC01', 'H9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (528, 0, 'LC01', 'H10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (529, 0, 'LC01', 'H11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (530, 0, 'LC01', 'H12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (531, 0, 'LC01', 'H13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (532, 0, 'LC01', 'H14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (533, 0, 'LC01', 'I1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (534, 0, 'LC01', 'I2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (535, 0, 'LC01', 'I3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (536, 0, 'LC01', 'I4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (537, 0, 'LC01', 'I5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (538, 0, 'LC01', 'I6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (539, 0, 'LC01', 'I7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (540, 0, 'LC01', 'I8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (541, 0, 'LC01', 'I9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (542, 0, 'LC01', 'I10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (543, 0, 'LC01', 'I11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (544, 0, 'LC01', 'I12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (545, 0, 'LC01', 'I13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (546, 0, 'LC01', 'I14', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (547, 0, 'LC01', 'J1', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (548, 0, 'LC01', 'J2', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (549, 0, 'LC01', 'J3', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (550, 0, 'LC01', 'J4', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (551, 0, 'LC01', 'J5', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (552, 0, 'LC01', 'J6', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (553, 0, 'LC01', 'J7', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (554, 0, 'LC01', 'J8', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (555, 0, 'LC01', 'J9', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (556, 0, 'LC01', 'J10', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (557, 0, 'LC01', 'J11', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (558, 0, 'LC01', 'J12', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (559, 0, 'LC01', 'J13', NULL, 0, .0000);  
INSERT INTO [Ve] ([id], [LoaiVe], [idLichChieu], [MaGheNgoi], [idKhachHang], [TrangThai], [TienBanVe]) VALUES (560, 0, 'LC01', 'J14', NULL, 0, .0000);  

SET IDENTITY_INSERT [dbo].[Ve] OFF
GO
