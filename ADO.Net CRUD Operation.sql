create database Assignment13Db
use Assignment13Db

create table Players
(PlayerId int primary key,
FirstName nvarchar(50),
LastName nvarchar(50),
JerseyNumber int ,
Position nvarchar(50),
Team nvarchar(50))

insert into Players values(1,'M.S','Dhoni',7,'WicketKeeper','CSK')
select * from Players