CREATE DATABASE Hetrogiupquyetdinh
GO
USE Hetrogiupquyetdinh
GO
CREATE TABLE Gioitinh(
Id INT PRIMARY KEY NOT NULL IDENTITY,
Name NVARCHAR(10) NOT NULL,
--cũng từ 1 tới 5
Douutien INT NOT NULL
)
GO 



CREATE TABLE SoThich(
Id INT PRIMARY KEY NOT NULL IDENTITY,
Name NVARCHAR(MAX) NOT NULL,
)
GO 

CREATE TABLE TinhCach(
Id INT PRIMARY KEY NOT NULL IDENTITY,
Name NVARCHAR(MAX) NOT NULL,
)
GO 

CREATE TABLE Truong(
Id INT PRIMARY KEY NOT NULL IDENTITY,
Name NVARCHAR(MAX) NOT NULL

)
GO 

CREATE TABLE Nganh(
Id INT PRIMARY KEY NOT NULL IDENTITY,
TruongId INT NOT NULL,
Name NVARCHAR(MAX) NOT NULL,
--nhu cầu nhân lực: rất thấp:1, thấp:2,trung bình:3,cao :4, rất cao: 5
Nhucaunhanluc int NOT NULL,
DiemNamNgoai float NOT NULL
)
GO 
ALTER TABLE dbo.Nganh ADD	FOREIGN KEY(TruongId) REFERENCES dbo.Truong(Id)

--thông tin riêng: giới tính, sở thích, tính cách,điểm số
CREATE TABLE DoPhuHop_Thongtinrieng(
--id của ngành
Id INT NOT NULL PRIMARY KEY,
--tên của thuộc tính
Name INT NOT NULL,
--cũng từ 1 tới 5
Douutien INT NOT NULL,
DoPhuHop FLOAT NOT NULL
)
GO 
ALTER TABLE dbo.DoPhuHopCuaNganh ADD	FOREIGN KEY(Name) REFERENCES dbo.Thuoctinh(Name)
GO 

--thông tin chung: nhu cầu nhân lực của ngành, điểm số ngành năm ngoái

CREATE TABLE Thuoctinh(
Id INT NOT NULL PRIMARY KEY IDENTITY,
Name NVARCHAR(MAX)  NOT NULL,
Douutien INT NOT NULL
)
GO 

CREATE TABLE ThiSinh(
Id INT PRIMARY KEY NOT NULL IDENTITY,
GioitinhId INT NOT NULL,
SothichId INT NOT NULL,
TinhcachId INT NOT NULL,
Diemso FLOAT NOT NULL,
NguyenvongId INT NOT NULL
)
GO 

ALTER TABLE dbo.ThiSinh ADD	FOREIGN KEY(GioitinhId) REFERENCES dbo.Gioitinh(Id)
ALTER TABLE dbo.ThiSinh ADD	FOREIGN KEY(NguyenvongId) REFERENCES dbo.Nganh(Id)
ALTER TABLE dbo.ThiSinh ADD	FOREIGN KEY(SothichId) REFERENCES dbo.SoThich(Id)
ALTER TABLE dbo.ThiSinh ADD	FOREIGN KEY(TinhcachId) REFERENCES dbo.TinhCach(Id)
GO 

CREATE	TABLE DiemCacNam(
IDVien INT NOT NULL,
Nam2014 FLOAT NOT NULL,
Nam2015 FLOAT NOT NULL,
Nam2016 FLOAT NOT NULL,
Nam2017 FLOAT NOT NULL,
)
ALTER TABLE dbo.DiemCacNam ADD	FOREIGN KEY(IDVien) REFERENCES dbo.Vien(Id)
GO 
