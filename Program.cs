using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Dapper;

namespace Benchmark
{
    internal delegate int Operation(int num1, int num2);

    public class BenchMarks
    {
        private static readonly List<Operation> Operations = new List<Operation>
        {
            Add, Sub, Mult, Div
        };

        private static int Add(int num1, int num2) => num1 + num2;

        private static int Sub(int num1, int num2) => num1 - num2;

        private static int Mult(int num1, int num2) => num1 * num2;

        private static int Div(int num1, int num2) => num1 < num2 ? num2 / num1 : num1 / num2;

        private static readonly Random rand = new Random();

        [Benchmark(Description = "Perform Local Local Task", Baseline = true)]
        public int LocalWork()
        {
            var num1 = rand.Next(1, 1000);
            var num2 = rand.Next(1, 1000);
            return Operations[rand.Next(0, 4)](num1, num2);
        }


        [Benchmark(Description = "Read 1k file from Disc")]
        public string ReadFromDisk()
        {
            using (var fil = File.OpenRead("A1kFile.txt"))
            using (var mr = new StreamReader(fil))
            {
                return mr.ReadToEnd();
            }
        }

        [Benchmark(Description = "Retrieve Data from local DB")]
        public Guid DbRetreive()
        {
            var Id = rand.Next(99999);

            using (var cn = new SqlConnection(
                "Data Source=(localdb)\\MSSQLLocalDB;" +
                "Database=Benchmark;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;" +
                "TrustServerCertificate=True;ApplicationIntent=ReadWrite;" +
                "MultiSubnetFailover=False;" +
                "Application Name=LoadTest"
            ))
            {
                return cn.QuerySingle<Guid>("Select SomeId From Simple where Id = @id", new { Id });
            }
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<BenchMarks>();
        }
    }
}