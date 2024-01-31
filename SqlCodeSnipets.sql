create database EmployeePayRollDB
use EmployeePayRolldb

create table EmployeePayRoll(
EmployeeId int primary key identity(1,1),
FullName varchar(50),
ImagePath varchar(50),
Gender varchar(10),
Department varchar(10),
Salary decimal(10,2),
StartDate date,
notes varchar(10)
)
select * from EmployeePayRoll;
insert into EmployeePayRoll values ('Maya Trivedi','c:/pictures/screenshot/horse.jpeg','F','Cse',25000,'2024/01/23','Good');
--stored Procedure for insert employee
create proc InsertEmployee(
@FullName varchar(50),
@ImagePath varchar(50),
@Gender char,
@Department varchar(50),
@Salary dec(10,2),
@StartDate date,
@Notes varchar(10))
as
begin
insert into EmployeePayRoll(FullName,ImagePath,Gender,Department,Salary,StartDate,notes) values 
(@FullName,@ImagePath,@Gender,@Department,@Salary,@StartDate,@Notes)
end


--Stored procedure for get all employees
create proc GetAllEmployees
as
begin
select * from EmployeePayRoll;
end

--stored procedure for get employee by id 
alter proc GetEmpById(
@EmployeeId int)
as
begin
select * from EmployeePayRoll where EmployeeId=@EmployeeId;
end

--stored procedure for get employee by name
create proc GetEmpByName(
@Name varchar(50))
as
begin
select * from EmployeePayRoll where FullName=@Name;
end

--stored procedure for Update by Id
create procedure UpdateEmployee(
@EmployeeId int,
@FullName varchar(50),
@ImagePath varchar(50),
@Gender char,
@Department varchar(50),
@Salary dec(10,2),
@StartDate date,
@Notes varchar(10)
)
as
begin
update EmployeePayRoll set FullName=@FullName,ImagePath=@ImagePath,Gender=@Gender,Department=@Department,Salary=@Salary,
StartDate=@StartDate,notes=@Notes  
where EmployeeId=@EmployeeId;
end

--stored procedure for deleting by id
create proc DeleteEmp(
@EmployeeId int )
as 
begin
delete from EmployeePayRoll where EmployeeId=@EmployeeId;
end


--stored procedure for get employee by name or department
alter proc GetEmpByNameOrDept(
@searchString varchar(50))
as
begin
select * from EmployeePayRoll where  (FullName=@searchString) or( Department=@searchString);
end

--stored procedure for get employee in the range between dates
create proc GetDateBwtRange(
@StartDate date,
@EndDate date)
as
begin
select * from EmployeePayRoll where StartDate between @StartDate and @EndDate;
end
