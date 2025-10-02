CREATE DATABASE gym;
USE gym;
CREATE TABLE men (
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(50) NOT NULL,
    lname NVARCHAR(50) NOT NULL,
    age NVARCHAR(50),
    number NVARCHAR(50) NOT NULL,
    gender NVARCHAR(10) NOT NULL,
    classes NVARCHAR(10) NOT NULL,
    date NVARCHAR(50) NOT NULL,
    uid CHAR(50) NOT NULL,
    usertype NVARCHAR(10) NOT NULL,
    lastsing NVARCHAR(50) NOT NULL
);
CREATE TABLE admin_user(
    adminid BIGINT PRIMARY KEY IDENTITY(1,1),
    username NVARCHAR(50) NOT NULL,
    password NVARCHAR(50) NOT NULL,
    accessibility INT NOT NULL;
);
INSERT INTO admin_user (username,password,accessibility)
VALUSE
('admin','admin',1);
GO