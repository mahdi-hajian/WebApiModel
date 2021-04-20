USE TEST3

GO

SELECT DepartmentID,Year,Revenue ,
SUM(Revenue) OVER (PARTITION BY DepartmentID ORDER BY [Year]
					ROWS BETWEEN 3 PRECEDING AND CURRENT ROW ) AS Prev3,
SUM(Revenue) OVER (PARTITION BY DepartmentID ORDER BY [Year]
					ROWS BETWEEN CURRENT ROW AND 3 FOLLOWING ) AS Next3,
MAX(Revenue) OVER (PARTITION BY DepartmentID ORDER BY [Year]
					ROWS UNBOUNDED PRECEDING ) AS MinRevenue,
SUM(Revenue) OVER (PARTITION BY DepartmentID ORDER BY [Year]
					ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW ) AS SumLast, --ROWS UNBOUNDED PRECEDING
SUM(Revenue) OVER (PARTITION BY DepartmentID ORDER BY [Year]
					ROWS  BETWEEN UNBOUNDED PRECEDING AND unbounded FOLLOWING ) AS SumAll,
					LEAD(Revenue,2,0) OVER (PARTITION BY DepartmentID ORDER BY [Year]) as LeadValue,
					lag(Revenue,2,0) OVER (PARTITION BY DepartmentID ORDER BY [Year]) as LagValue,
					FIRST_VALUE(Revenue) OVER (PARTITION BY DepartmentID ORDER BY [Year] RANGE BETWEEN UNBOUNDED PRECEDING AND UNBOUNDED FOLLOWING) AS FirstValue,
					LAST_VALUE(Revenue) OVER (PARTITION BY DepartmentID ORDER BY [Year] RANGE BETWEEN UNBOUNDED PRECEDING AND UNBOUNDED FOLLOWING) AS LastValue,
					CUME_DIST() OVER(ORDER BY Revenue ) AS [CUME_DIST] --// بزرگترین را یک قرار میدهد و بقیه اعداد را نسبت به آن بررسی میکند
FROM Revenue
ORDER BY DepartmentID, Year


--تفاوت رنج و رووز rang and rows
--DECLARE @Test TABLE
--(
--RowID INT IDENTITY,
--FName VARCHAR(20),
--Salary SMALLINT
--);
--INSERT INTO @Test (FName, Salary)
--VALUES ('George', 800),
--('Sam', 950),
--('Diane', 1100),
--('Nicholas', 1250),
--('Samuel', 1250),
--('Patricia', 1300),
--('Brian', 3000),
--('Thomas', 1600),
--('Fran', 2450),
--('Debbie', 2850),
--('Mark', 2975),
--('James', 3000),
--('Cynthia', 3000),
--('Christopher', 5000);
--SELECT RowID,FName,Salary,
--       SumByRows = SUM(Salary) OVER (ORDER BY Salary ROWS UNBOUNDED PRECEDING),
--   SumByRange = SUM(Salary) OVER (ORDER BY Salary RANGE UNBOUNDED PRECEDING)
--FROM @Test
--ORDER BY RowID;

--/////////////////////////////////////////////////////////////////////////////////////////
----Row_Number،Rank،Dense_Rank،NTILE
---- به هر رکورد یک عدد نسبت میدهد و اوردر اجباری مباشد
--Select *, ROW_NUMBER() OVER ( ORDER BY Revenue DESC) AS RN from Revenue
---- پارتیشن اضافه میشود
--Select *, ROW_NUMBER() OVER ( PARTITION BY DepartmentID ORDER BY Revenue DESC) AS RN from Revenue

---- رنک ایجاد میکند یعنی تمام قیمت ها یک مقدار میگیرند و از بعد تعدادشان شروع میشود
--Select *,RANK() over ( ORDER BY Revenue ) AS RANK from Revenue
--Select *,RANK() over (PARTITION BY DepartmentID ORDER BY Revenue ) AS RANK from Revenue

---- رنک ایجاد میکند یعنی تمام قیمت ها یک مقدار میگیرند ولی برخلاف بالا از عدد بعدی شروع میشود و gap ایجاد نمیشود
--Select *,dense_RANK() over (ORDER BY Revenue ) AS dense_RANK from Revenue
--Select *,dense_RANK() over (PARTITION BY DepartmentID ORDER BY Revenue ) AS dense_RANK from Revenue

---- کل رکورد هارا به تعداد قطعه های دلخواه پارتیشن بعدی میکند
--Select * ,NTILE(4) over ( ORDER BY Revenue desc) as NTILE from Revenue
--Select * ,NTILE(2) over ( PARTITION BY DepartmentID ORDER BY Revenue desc) as NTILE from Revenue

---- هر نوع فانکشنی را میتوان اجرا کرد
--Select *,avg(Revenue) over (ORDER BY Revenue ) AS dense_RANK from Revenue
--Select *,SUM(Revenue) over (PARTITION by DepartmentID ORDER BY Revenue ) AS dense_RANK from Revenue

--/////////////////////////////////////////////////////////////////////////////////////////


--CREATE TABLE Revenue
--(
--[DepartmentID] int,
--[Revenue] int,
--[Year] int
--);

--insert into Revenue
--values (1,10030,1998),(2,20000,1998),(3,40000,1998),
-- (1,20000,1999),(2,60000,1999),(3,50000,1999),
-- (1,40000,2000),(2,40000,2000),(3,60000,2000),
-- (1,30000,2001),(2,30000,2001),(3,70000,2001),
-- (1,90000,2002),(2,20000,2002),(3,80000,2002),
-- (1,10300,2003),(2,1000,2003), (3,90000,2003),
-- (1,10000,2004),(2,10000,2004),(3,10000,2004),
-- (1,20000,2005),(2,20000,2005),(3,20000,2005),
-- (1,40000,2006),(2,30000,2006),(3,30000,2006),
-- (1,70000,2007),(2,40000,2007),(3,40000,2007),
-- (1,50000,2008),(2,50000,2008),(3,50000,2008),
-- (1,20000,2009),(2,60000,2009),(3,60000,2009),
-- (1,30000,2010),(2,70000,2010),(3,70000,2010),
-- (1,80000,2011),(2,80000,2011),(3,80000,2011),
-- (1,10000,2012),(2,90000,2012),(3,90000,2012)

--/////////////////////////////////////////////////////////////////////////////////////////

-- --آشنایی با Window Function 
--https://www.dntips.ir/post/1142
--https://www.dntips.ir/post/1144
--https://www.dntips.ir/post/1156
--https://www.dntips.ir/post/1163
--https://www.dntips.ir/post/1284

-- --توابع Window و مساله های آماری running total و runnning average
--https://www.dntips.ir/post/1205