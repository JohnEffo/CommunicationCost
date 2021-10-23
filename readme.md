# Communication Cost

Just a simple console application to demonstrate the cost of performing communication vs the cost of performing a calculation.

## Running

* Create the database
The console expects run against a localDB called Benchmark. To create this run SQLQuery1.sql, this will create a data base and populate it with the required data. If you decide not to use LocalDb you will have to change the connection string, hard coded in the database, appropriately. 

The connection string assumes its running on windows, if you’re running on Linux you may also need to change it.

* Build and run

The program uses [Benchmark.Net](https://benchmarkdotnet.org/) so needs to be built in release mode use:

dotnet build -c Release

And to execute

 .\bin\Release\net5.0\Benchmark

## The output
 
To save you the bother here is the results from executing the program:

```
|                        Method |          Mean |        Error |        StdDev |     Ratio | RatioSD |
|------------------------------ |--------------:|-------------:|--------------:|----------:|--------:|
|    'Perform Local Local Task' |      40.91 ns |     0.581 ns |      0.515 ns |      1.00 |    0.00 |
|      'Read 1k file from Disc' |  42,525.91 ns |   792.737 ns |    702.741 ns |  1,039.71 |   22.91 |
| 'Retrieve Data from local DB' | 495,373.64 ns | 9,765.848 ns | 11,625.547 ns | 12,173.05 |  329.84 |
```




