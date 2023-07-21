create database UserRegister;
use UserRegister;

Create Table ProfileData (
	Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	Name VARCHAR(50),
	IsActive BIT DEFAULT 0
);

Insert into ProfileData(Name) values ('Administrador');
Insert into ProfileData(Name) values ('Consultor');

Select * from ProfileData;

Create table UserData (
  Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
  Name VARCHAR(100),
  Email VARCHAR(100),
  Password VARCHAR(40),
  IdProfile UNIQUEIDENTIFIER FOREIGN KEY REFERENCES ProfileData(Id),
  IsActive BIT DEFAULT 0
 );

 Insert into UserData (Name, Email, Password, IdProfile) values ('Geampiere Jaramillo','Geampiere@gmail.com', '12345','010AF7A9-5E05-4612-B0E0-912DF410A632');
  
 CREATE table Contact (
 	Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
   	Name VARCHAR(100),
   	Email VARCHAR(100),
   	Phone VARCHAR(10),
	Biography VARCHAR(300),
	IsActive BIT DEFAULT 0
 );
 
 CREATE TABLE UserContact (
 	Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	IdUser UNIQUEIDENTIFIER FOREIGN KEY REFERENCES UserData(Id),
	IdContact UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Contact(Id)
);