create database compilerProject
 use compilerProject
create table CharsId 
(
	charID int IDENTITY(2, 1) PRIMARY KEY,
	char varchar(2) NOT NULL ,  
)

create table KeyWords
(
	keyWord varchar(20) NOT NULL ,
	ReturnToken varchar(20) NOT NULL , 
)