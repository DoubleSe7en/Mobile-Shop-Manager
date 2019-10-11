create database MobileShopDB
go

use MobileShopDB
go

create table Employee
(
	ID varchar(50)primary key,
	Name nvarchar(200),
	Gender nvarchar(10) check(Gender in (N'Nam', N'Nữ')),
	Birthday date,
	CMND varchar(20),
	DateStarted date ,
	Salary float,
	PhoneNumber varchar(20),
	IsDeleted int default 1
)
go
alter table Employee add  DateStarted date

create table Account
(
	IDEmployee varchar(50),
	UserName varchar(200) primary key,
	PassWord varchar(200),
	TypeAccount nvarchar(200),
	foreign key (IDEmployee) references Employee(ID)
)
go 


create table Category
(
	ID varchar(50) primary key,
	Name nvarchar(200),
	IsDeleted int default 1,
)
go

create table Product
(
	ID varchar(50) primary key,
	Name nvarchar(200),
	IDCategory varchar(50),
	Price float,
	IsDeleted int default 1,
	SalePercent float default 0,
	foreign key (IDCategory) references Category(ID)
)
go

create table ProductDetail
(
	IDProduct varchar(50) primary key,
	Screen nvarchar(200),
	OperatingSystem nvarchar(200),
	RearCamera nvarchar(200),
	FontCamera nvarchar(200),
	CPU nvarchar(200),
	Ram nvarchar(200),
	Rom nvarchar(200),
	MemoryCard nvarchar(200),
	Sim nvarchar(200),
	Battery nvarchar(200),
	Manufacturer nvarchar(200),
	Orgin nvarchar(200),
	WarranttyPeriod nvarchar(200),
	foreign key (IDProduct) references Product(ID)
)
go

create table Customer
(
	ID varchar(50) primary key,
	Name nvarchar(200),
	Point int,
	IsDeleted int default 1
)
go

create table Feedback
(
	ID int identity primary key,
	IDCustomer varchar(50),
	Rate int,
	Content nvarchar(max),
	DateRate date default getdate(),
	foreign key (IDCustomer) references Customer(ID)
)
go

create table Bill
(
	ID int identity primary key,
	IDCustomer varchar(50),
	IDEmployee varchar(50),
	DateCheckOut datetime,
	TotalPayment float,
	IsDeleted int default 1,
	foreign key (IDCustomer) references Customer(ID),
	foreign key (IDEmployee) references Employee(ID)
)
go

create table BillDetail
(
	IDBill int,
	IDProduct varchar(50),
	Count int,
	primary key (IDBill, IDProduct),
	foreign key (IDBill) references Bill(ID),
	foreign key (IDProduct) references Product(ID)
)
go

create table WareHouse
(
	ID int identity primary key,
	IDProduct varchar(50),
	AmountAdded int, 
	AmountRemaining int,
	DateAdded date,
	Orgin nvarchar(200),
	foreign key (IDProduct) references Product(ID)
)
go


--Lấy account khi đăng nhập
create proc USP_GetAccountByUserName
@username varchar(200), @password varchar(200)
as
begin
	select * from Account where UserName = @username and PassWord = @password
end
go

--Lấy nhân viên bằng ID
create proc USP_GetEmployeeByID
@id varchar(50)
as
begin
	select * from Employee where ID = @id
end
go

--Cập nhật lại mật khẩu sau khi đổi hoạc sau khi phân quyền
create proc USP_UpdateAccount
@userName varchar(200), @password varchar(200), @typeAccount nvarchar(200)
as
begin
	update Account set PassWord = @password, TypeAccount = @typeAccount where UserName = @userName
end
go


--Lấy khách hàng theo ID
create proc USP_GetCustomerByID
@id varchar(50)
as
begin
	select * from Customer where ID = @id
end
go
--Cập nhật điểm của khách hàng
create proc USP_UpdatePointCustomer
@id varchar(50), @point int
as
begin
	Update Customer set Point = @point where ID = @id
end
go

---Thêm hóa đơn
create proc USP_InsertBill
@idCustomer varchar(50), @idEmployee varchar(50), @dateCheckOut datetime, @totalPayment float
as
begin
	insert into Bill (IDCustomer, IDEmployee, DateCheckOut, TotalPayment)
	values (@idCustomer, @idEmployee, @dateCheckOut, @totalPayment)
end
go
---Thêm hóa đơn mà không có id khách hàng
create proc USP_InsertBillWithoutIdCustomer
@idEmployee varchar(50), @dateCheckOut datetime, @totalPayment float
as
begin
	insert into Bill (IDEmployee, DateCheckOut, TotalPayment)
	values (@idEmployee, @dateCheckOut, @totalPayment)
end
go

exec USP_InsertBill @idCustomer = null, @idEmployee = 'NV01', @dateCheckOut = '01-01-2019', @totalPayment = 1213
go

--Lấy id hóa đơn
create proc USP_GetIdtBill
@dateCheckOut datetime
as
begin
	select ID from Bill where DateCheckOut = @dateCheckOut
end
go

--Thêm chi tiết hóa đơn
create proc USP_InsertBillDetail
@idBill int, @idProduct varchar(50), @count int
as
begin
	insert into BillDetail(IDBill, IDProduct, Count)
	values (@idBill, @idProduct, @count)
end
go

--Lấy id trong kho của sản phẩm mà số lượng còn lại > 0
create proc USP_GetIdWareHouse
@idProduct varchar(50)
as
begin
	Select ID from WareHouse where IDProduct = @idProduct and AmountRemaining > 0
end
go

--Lấy số lượng còn lại của tất cả sản phẩm trong kho
create proc USP_GetAmountRemaining_ProductInWareHouse
as
begin
	Select P.*, Sum(AmountRemaining) as SL 
	from Product P, WareHouse W  
	where P.ID = W.IDProduct 
	group by P.ID, p.Name, P.IDCategory, P.Price, P.SalePercent, P.IsDeleted
end
go
--Lấy số lượng còn lại của 1 sản phẩm 
create proc USP_GetAmountRemainingProductByID
@idProduct varchar(50)
as
begin
	Select Sum(AmountRemaining) from WareHouse where IDProduct = @idProduct group by IDProduct 
end
go

--Cập nhật lại số lượng trong kho sau khi bán
create proc USP_UpdateAmountRemaining
@id int, @count int
as
begin
	Update WareHouse set AmountRemaining -= @count where ID = @id
end
go
exec USP_UpdateAmountRemaining @id = '1', @count = 4
go

--Lấy thông tin chi tiết của sản phẩm
create proc USP_GetDetailProductByID
@idProduct varchar(50)
as
begin
	Select * from ProductDetail where IDProduct = @idProduct
end
go

--Lấy danh sách sản phẩm theo loại
create proc USP_GetProductByIDCategory
@idCategory varchar(50)
as
begin
	Select * from Product where IDCategory = @idCategory
end
go

--Lấy danh sách sản phẩm của hãng khác
create proc USP_GetProductByIDCategoryOther
as
begin
	Select * from Product
	where IDCategory  not in ('AP', 'HC', 'LG', 'NK', 'SM', 'SN')
end
go


--Lấy sản phẩm sale 
Select * from Product Where SalePercent > 0 Order by SalePercent Desc

--Lấy sản phẩm bán chạy
Select P.* from Product P, BillDetail BD
Where P.ID = BD.IDProduct 
group by P.ID, P.Name, P.IDCategory, P.Price, P.SalePercent, P.Price, P.IsDeleted
Order by Sum(BD.Count) Desc

--Lấy sản phẩm trong kho
Select* From WareHouse
where DateAdded between '01-01-2019' and '01-06-2019'

Select* From Bill where DateCheckOut between '01-01-2017' and '01-06-2020'
go
--Lấy danh sách hóa đơn với tên nhân viên và tên khách hàng tương ứng
create proc USP_GetListBill
@dateStart date, @dateEnd date
as
begin
	Select B.*, E.Name as NameEmployee , Case 
											When B.IDCustomer is not null Then (Select Name From Customer Where B.IDCustomer = ID)
											Else NULL
									     End as NameCustomer						
	from Bill B, Employee E
	where B.IDEmployee = E.ID and DateCheckOut between @dateStart and  @dateEnd
end
go

--Lấy chi tiết hóa đơn với tên sản phẩm
create proc USP_GetBillDetail
@idBill varchar(50)
as
begin
	Select BD.*, P.Name as NameProduct
	From BillDetail BD, Product P
	Where BD.IDProduct = P.ID and BD.IDBill = @idBill
end
go

--Thêm nhân viên
create proc USP_InsertEmployee
@Id varchar(50), @Name nvarchar(200), @Gender nvarchar(10), @Birthday date, @CMND varchar(20), @Salary float, @PhoneNumber varchar(20)
as
begin
	Insert into Employee(ID, Name, Gender, Birthday, CMND, Salary, PhoneNumber)
	values(@Id, @Name, @Gender, @Birthday, @CMND, @Salary, @PhoneNumber)
end
go

--Sửa nhân viên
create proc USP_UpdateEmployee
@Id varchar(50), @Name nvarchar(200), @Gender nvarchar(10), @Birthday date, @CMND varchar(20), @Salary float, @PhoneNumber varchar(20)
as
begin
	Update Employee set Name = @Name, Gender = @Gender, Birthday = @Birthday, CMND = @CMND, Salary = @Salary, PhoneNumber = @PhoneNumber
	where ID = @Id
end
go

--Thêm tài khoản
create proc USP_InsertAccount
@IDEmployee varchar(50), @UserName varchar(200), @PassWord varchar(200), @TypeAccount nvarchar(200)
as
begin
	Insert into Account
	values(@IDEmployee, @UserName, @PassWord, @TypeAccount)
end
go

--Thêm khách hàng
create proc USP_InsertCustomer
@ID varchar(50), @Name nvarchar(200), @Point int
as
begin
	Insert into Customer(ID, Name, Point)
	values(@ID, @Name, @Point)
end
go

USE [MobileShopDB]
GO
INSERT [dbo].[Employee] ([ID], [Name], [Gender], [Birthday], [CMND], [Salary], [PhoneNumber], [IsDeleted], [DateStarted]) VALUES (N'AD01', N'admin1', N'Nam', CAST(N'1999-12-21' AS Date), N'898081231', 5000000, N'0912123123', 1, CAST(N'2019-01-01' AS Date))
INSERT [dbo].[Employee] ([ID], [Name], [Gender], [Birthday], [CMND], [Salary], [PhoneNumber], [IsDeleted], [DateStarted]) VALUES (N'NV01', N'Lê Văn Đạt', N'Nam', CAST(N'1999-12-21' AS Date), N'898081231', 5000000, N'0912123123', 0, CAST(N'2019-01-01' AS Date))
INSERT [dbo].[Employee] ([ID], [Name], [Gender], [Birthday], [CMND], [Salary], [PhoneNumber], [IsDeleted], [DateStarted]) VALUES (N'NV02', N'Nguyễn Huỳnh', N'Nam', CAST(N'1999-12-21' AS Date), N'898081231', 5000000, N'0912123123', 1, CAST(N'2019-01-01' AS Date))
INSERT [dbo].[Employee] ([ID], [Name], [Gender], [Birthday], [CMND], [Salary], [PhoneNumber], [IsDeleted], [DateStarted]) VALUES (N'NV03', N'Lê Văn Đạt', N'Nam', CAST(N'1999-12-21' AS Date), N'898081231', 5000000, N'0912123123', 0, NULL)
INSERT [dbo].[Employee] ([ID], [Name], [Gender], [Birthday], [CMND], [Salary], [PhoneNumber], [IsDeleted], [DateStarted]) VALUES (N'NV04', N'Nguyễn Thị Đẹp', N'Nữ', CAST(N'2000-10-11' AS Date), N'312321238', 5000000, N'0919129892', 1, NULL)
INSERT [dbo].[Employee] ([ID], [Name], [Gender], [Birthday], [CMND], [Salary], [PhoneNumber], [IsDeleted], [DateStarted]) VALUES (N'NV05', N'Nguyễn Thị Mai', N'Nữ', CAST(N'2000-10-11' AS Date), N'312321238', 5000000, N'0919129892', 1, NULL)
INSERT [dbo].[Employee] ([ID], [Name], [Gender], [Birthday], [CMND], [Salary], [PhoneNumber], [IsDeleted], [DateStarted]) VALUES (N'NV06', N'Nguyễn Thị Mai', N'Nữ', CAST(N'2000-10-11' AS Date), N'312321238', 5000000, N'0919129892', 0, NULL)
INSERT [dbo].[Employee] ([ID], [Name], [Gender], [Birthday], [CMND], [Salary], [PhoneNumber], [IsDeleted], [DateStarted]) VALUES (N'NV07', N'Nguyễn Thị trúc', N'Nữ', CAST(N'2000-10-11' AS Date), N'312321238', 5000000, N'0919129892', 0, NULL)
INSERT [dbo].[Employee] ([ID], [Name], [Gender], [Birthday], [CMND], [Salary], [PhoneNumber], [IsDeleted], [DateStarted]) VALUES (N'NV09', N'Nguyễn Thị Sen', N'Nam', CAST(N'2001-11-10' AS Date), N'3123212312', 9000000, N'091912989212', 1, NULL)
INSERT [dbo].[Employee] ([ID], [Name], [Gender], [Birthday], [CMND], [Salary], [PhoneNumber], [IsDeleted], [DateStarted]) VALUES (N'NV10', N'Nguyễn Thị Đào', N'Nữ', CAST(N'2001-11-10' AS Date), N'312321238123', 5000000123, N'0919129892123', 0, NULL)
INSERT [dbo].[Employee] ([ID], [Name], [Gender], [Birthday], [CMND], [Salary], [PhoneNumber], [IsDeleted], [DateStarted]) VALUES (N'NV111', N'Nguyễn Mai', N'Nam', CAST(N'2001-11-10' AS Date), N'3123212312', 1000000, N'09191212989212', 1, NULL)
INSERT [dbo].[Customer] ([ID], [Name], [Point], [IsDeleted]) VALUES (N'001', N'Nguyễn Mạnh Cường', 50, 0)
INSERT [dbo].[Customer] ([ID], [Name], [Point], [IsDeleted]) VALUES (N'0011', N'Nguyễn Mạnh Cường', 0, 1)
INSERT [dbo].[Customer] ([ID], [Name], [Point], [IsDeleted]) VALUES (N'002', N'Nguyễn Huỳnh Như', 0, 1)
INSERT [dbo].[Customer] ([ID], [Name], [Point], [IsDeleted]) VALUES (N'003', N'Nguyễn Cường Mạnh', 0, 1)
INSERT [dbo].[Customer] ([ID], [Name], [Point], [IsDeleted]) VALUES (N'004', N'Nguyễn Huỳnh Hoa', 0, 0)
INSERT [dbo].[Customer] ([ID], [Name], [Point], [IsDeleted]) VALUES (N'007', N'Nguyễn Huỳnh Trúc', 50, 1)
INSERT [dbo].[Customer] ([ID], [Name], [Point], [IsDeleted]) VALUES (N'008', N'Nguyễn Huỳnh Mai', 50, 1)
INSERT [dbo].[Customer] ([ID], [Name], [Point], [IsDeleted]) VALUES (N'009', N'Nguyễn Mạnh Cường', 100, 1)
INSERT [dbo].[Customer] ([ID], [Name], [Point], [IsDeleted]) VALUES (N'010', N'Nguyễn Huỳnh Trúc', 1000, 1)
INSERT [dbo].[Customer] ([ID], [Name], [Point], [IsDeleted]) VALUES (N'012', N'Nguyễn Huỳnh Mắn', 1, 0)
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([ID], [IDCustomer], [IDEmployee], [DateCheckOut], [TotalPayment], [IsDeleted]) VALUES (2028, NULL, N'AD01', CAST(N'2019-01-17T06:59:10.860' AS DateTime), 18000000, 1)
INSERT [dbo].[Bill] ([ID], [IDCustomer], [IDEmployee], [DateCheckOut], [TotalPayment], [IsDeleted]) VALUES (2029, N'001', N'AD01', CAST(N'2019-01-17T06:59:44.147' AS DateTime), 17100000, 0)
INSERT [dbo].[Bill] ([ID], [IDCustomer], [IDEmployee], [DateCheckOut], [TotalPayment], [IsDeleted]) VALUES (2030, N'001', N'AD01', CAST(N'2019-01-17T07:00:26.070' AS DateTime), 35500000, 1)
SET IDENTITY_INSERT [dbo].[Bill] OFF
INSERT [dbo].[Category] ([ID], [Name], [IsDeleted]) VALUES (N'AP', N'Apple', 1)
INSERT [dbo].[Category] ([ID], [Name], [IsDeleted]) VALUES (N'HC', N'HTC', 1)
INSERT [dbo].[Category] ([ID], [Name], [IsDeleted]) VALUES (N'LG', N'LG', 1)
INSERT [dbo].[Category] ([ID], [Name], [IsDeleted]) VALUES (N'NK', N'Nokia', 1)
INSERT [dbo].[Category] ([ID], [Name], [IsDeleted]) VALUES (N'SM', N'Samsung', 1)
INSERT [dbo].[Category] ([ID], [Name], [IsDeleted]) VALUES (N'SN', N'Sony', 1)
INSERT [dbo].[Category] ([ID], [Name], [IsDeleted]) VALUES (N'VM', N'Vsmart', 1)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'1', N'IPhone 3', N'AP', 5000000, 1, 50)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'10', N'Samsung Galaxy J2 Prime', N'SM', 6000000, 1, 50)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'11', N'Nokia 7 plus', N'NK', 1000000, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'12', N'Nokia 8.1', N'NK', 1500000, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'13', N'Nokia 6.1 Plus', N'NK', 10500000, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'14', N'Nokia 8110 4G', N'NK', 9000000, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'15', N'Nokia 3310 2017', N'NK', 6500000, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'16', N'Vsmart Active 1+', N'VM', 6290000, 1, 10)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'17', N'Vsmart Active 1', N'VM', 4990000, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'18', N'Vsmart Joy 1+', N'VM', 3390000, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'19', N'Vsmart Joy 1', N'VM', 2490000, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'2', N'IPhone 4', N'AP', 6000000, 1, 50)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'3', N'IPhone 5', N'AP', 7000000, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'4', N'IPhone 6', N'AP', 8000000, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'5', N'IPhone 7', N'AP', 9000000, 1, 50)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'6', N'Samsung Galaxy Note 9', N'SM', 12000000, 1, 50)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'7', N'Samsung Galaxy A9 (2018)', N'SM', 1000000, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'8', N'Samsung Galaxy J8', N'SM', 8000000, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [IDCategory], [Price], [IsDeleted], [SalePercent]) VALUES (N'9', N'Samsung Galaxy J4 Core', N'SM', 7000000, 1, 0)
INSERT [dbo].[BillDetail] ([IDBill], [IDProduct], [Count]) VALUES (2028, N'6', 3)
INSERT [dbo].[BillDetail] ([IDBill], [IDProduct], [Count]) VALUES (2029, N'1', 1)
INSERT [dbo].[BillDetail] ([IDBill], [IDProduct], [Count]) VALUES (2029, N'10', 1)
INSERT [dbo].[BillDetail] ([IDBill], [IDProduct], [Count]) VALUES (2029, N'2', 1)
INSERT [dbo].[BillDetail] ([IDBill], [IDProduct], [Count]) VALUES (2029, N'5', 1)
INSERT [dbo].[BillDetail] ([IDBill], [IDProduct], [Count]) VALUES (2029, N'6', 1)
INSERT [dbo].[BillDetail] ([IDBill], [IDProduct], [Count]) VALUES (2030, N'2', 1)
INSERT [dbo].[BillDetail] ([IDBill], [IDProduct], [Count]) VALUES (2030, N'5', 1)
INSERT [dbo].[BillDetail] ([IDBill], [IDProduct], [Count]) VALUES (2030, N'6', 2)
INSERT [dbo].[BillDetail] ([IDBill], [IDProduct], [Count]) VALUES (2030, N'8', 2)
INSERT [dbo].[Account] ([IDEmployee], [UserName], [PassWord], [TypeAccount]) VALUES (N'AD01', N'admin', N'cdd96d3cc73d1dbdaffa03cc6cd7339b', N'Admin')
INSERT [dbo].[Account] ([IDEmployee], [UserName], [PassWord], [TypeAccount]) VALUES (N'NV02', N'levandat', N'6fd742a61bd034804c00c49b18045020', N'Nhân viên')
INSERT [dbo].[Account] ([IDEmployee], [UserName], [PassWord], [TypeAccount]) VALUES (N'NV09', N'nguyenthidao', N'6fd742a61bd034804c00c49b18045020', N'Nhân viên')
INSERT [dbo].[Account] ([IDEmployee], [UserName], [PassWord], [TypeAccount]) VALUES (N'NV04', N'test1', N'6fd742a61bd034804c00c49b18045020', N'Admin')
INSERT [dbo].[Account] ([IDEmployee], [UserName], [PassWord], [TypeAccount]) VALUES (N'NV05', N'test2', N'3401b18ac9acc27c4038c8bfffe5a19f', N'Admin')
INSERT [dbo].[Account] ([IDEmployee], [UserName], [PassWord], [TypeAccount]) VALUES (N'NV05', N'test24', N'6fd742a61bd034804c00c49b18045020', N'Admin')
INSERT [dbo].[ProductDetail] ([IDProduct], [Screen], [OperatingSystem], [RearCamera], [FontCamera], [CPU], [Ram], [Rom], [MemoryCard], [Sim], [Battery], [Manufacturer], [Orgin], [WarranttyPeriod]) VALUES (N'6', N'Super AMOLED, 6.4", Quad HD+ (2K+)', N'Android 8.1 (Oreo)', N'2 camera 12 MP', N'8 MP', N'Exynos 9810 8 nhân 64-bit', N'6 GB', N'128 GB', N'MicroSD, hỗ trợ tối đa 512 GB', N'2 SIM Nano (SIM 2 chung khe thẻ nhớ), Hỗ trợ 4G', N'4000 mAh, có sạc nhanh', N'SamSung', N'Việt Nam', N'12 tháng')
SET IDENTITY_INSERT [dbo].[WareHouse] ON 

INSERT [dbo].[WareHouse] ([ID], [IDProduct], [AmountAdded], [AmountRemaining], [Orgin], [DateAdded]) VALUES (1, N'1', 10, 9, N'Việt Nam', CAST(N'2019-01-01' AS Date))
INSERT [dbo].[WareHouse] ([ID], [IDProduct], [AmountAdded], [AmountRemaining], [Orgin], [DateAdded]) VALUES (2, N'1', 20, 16, N'Việt Nam', CAST(N'2019-01-02' AS Date))
INSERT [dbo].[WareHouse] ([ID], [IDProduct], [AmountAdded], [AmountRemaining], [Orgin], [DateAdded]) VALUES (3, N'10', 20, 11, N'Việt Nam', CAST(N'2019-10-01' AS Date))
INSERT [dbo].[WareHouse] ([ID], [IDProduct], [AmountAdded], [AmountRemaining], [Orgin], [DateAdded]) VALUES (4, N'2', 12, 10, N'Việt Nam', CAST(N'2019-01-12' AS Date))
INSERT [dbo].[WareHouse] ([ID], [IDProduct], [AmountAdded], [AmountRemaining], [Orgin], [DateAdded]) VALUES (5, N'5', 18, 10, N'Việt Nam', CAST(N'2018-01-01' AS Date))
INSERT [dbo].[WareHouse] ([ID], [IDProduct], [AmountAdded], [AmountRemaining], [Orgin], [DateAdded]) VALUES (6, N'6', 29, 6, N'Việt Nam', CAST(N'2019-01-05' AS Date))
INSERT [dbo].[WareHouse] ([ID], [IDProduct], [AmountAdded], [AmountRemaining], [Orgin], [DateAdded]) VALUES (7, N'8', 10, 10, N'Việt Nam', CAST(N'2019-01-06' AS Date))
SET IDENTITY_INSERT [dbo].[WareHouse] OFF