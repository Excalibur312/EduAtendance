using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace EduAttendance.Benchmark;

[Config(typeof(Config))]
public class BenchmarkService
{
    private class Config : ManualConfig
    {
        public Config()
        {
            SummaryStyle = BenchmarkDotNet.Reports.SummaryStyle.Default.WithRatioStyle(BenchmarkDotNet.Columns.RatioStyle.Trend);
        }
    }
    ApplicationDbContext dbContext = new();


    [Benchmark(Baseline =true)]
    public async Task GetAll()
    {
        await dbContext.Students.ToListAysnc();
    }

    public async Task GetAllAsNoTracking()
    {
        await dbContext.Students.AsNoTracking().ToListAysnc();
    }
}
