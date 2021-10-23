create Database Benchmark
Go
Create Table Simple
(
	Id int identity primary key,
	SomeId UniqueIdentifier 
)
Go
declare @count int =0;

while (@count < 100000)
begin 
    set @count += 1
	insert Simple(SomeId) values (NEWID())
end

