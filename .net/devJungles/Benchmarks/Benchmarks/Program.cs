using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using CommandLine;
using Microsoft.Diagnostics.Tracing.Parsers.Clr;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftAntimalwareEngine;
using System.Text;

// BenchmarkRunner.Run<SleepVSDelayBenchmark>();

//| Method       | Duration      | Mean      | Error     | StdDev    |
//| ------------ | ---------     | ---------:| ---------:| ---------:|
//| ThreadSleep  | 1  | 15.56 ms | 0.043 ms  | 0.036 ms  |
//| TaskDelay    | 1  | 15.61 ms | 0.082 ms  | 0.073 ms  |
//| ThreadSleep  | 5  | 15.57 ms | 0.036 ms  | 0.032 ms  |
//| TaskDelay    | 5  | 15.56 ms | 0.086 ms  | 0.067 ms  |
//| ThreadSleep  | 50 | 61.93 ms | 0.604 ms  | 0.535 ms  |
//| TaskDelay    | 50 | 61.92 ms | 0.597 ms  | 0.530 ms  |

//BenchmarkRunner.Run<ThreadStartVSThreadPoolQueueVSTaskRunMenchmark>(
//    DefaultConfig.Instance.AddColumn(StatisticColumn.P95)
//);

// Result
//|         Method   |         Mean |       Error  |    StdDev    | Median |                P95 | Ratio  |
//| ---------------- | ------------:| ------------:| ------------:| ------------:| ------------:| ------:|
//| ThreadStart      | 89,419.1 ns  | 1,697.86 ns  | 2,488.70 ns  | 89,430.2 ns  | 93,058.2 ns  | 1.000  |
//| ThreadPoolQueue  | 872.6 ns     | 20.33 ns     | 59.61 ns     | 889.2 ns     | 931.2 ns     | 0.009  |
//| TaskRun          | 3,353.2 ns   | 66.03 ns     | 88.15 ns     | 3,342.8 ns   | 3,500.8 ns   | 0.037  |

BenchmarkRunner.Run<StringConcatVSStringBuilderBenchmark>();

//| Method                      | Mean      | Error     | StdDev    | Ratio  | RatioSD  | Gen0    | Allocated  | Alloc Ratio  |
//| --------------------------  | ---------:| ---------:| ---------:| ------:| --------:| -------:| ----------:| ------------:|
//| StringConcat                | 400.9 ns  | 8.10 ns   | 15.21 ns  | 1.00   | 0.00     | 1.6203  | 4.97 KB    | 1.00         |
//| StringConcatBySteps         | 642.2 ns  | 12.55 ns  | 33.50 ns  | 1.61   | 0.11     | 2.7056  | 8.3 KB     | 1.67         |
//| StringBuilder               | 927.6 ns  | 18.53 ns  | 32.45 ns  | 2.31   | 0.13     | 3.3331  | 10.22 KB   | 2.06         |
//| StringBuilderWithCapacity   | 723.3 ns  | 13.51 ns  | 27.90 ns  | 1.80   | 0.09     | 3.2148  | 9.86 KB    | 1.98         |
//| StringBuilderInitialized    | 461.1 ns  | 8.93 ns   | 8.77 ns   | 1.13   | 0.06     | 1.5998  | 4.91 KB    | 0.99         |

public class SleepVSDelayBenchmark
{
    [Params(1, 5, 50)]
    public int Duration;

    [Benchmark]
    public void ThreadSleep() => Thread.Sleep(Duration);

    [Benchmark]
    public Task TaskDelay() => Task.Delay(Duration);
}

public class ThreadStartVSThreadPoolQueueVSTaskRunMenchmark
{
    [Benchmark(Baseline = true)]
    public void ThreadStart()
    {
        bool b = false;
        var thread = new Thread(() =>
        {
            b = true;
        });
        thread.Start();

        while (!Volatile.Read(ref b))
            ;
    }

    [Benchmark]
    public void ThreadPoolQueue()
    {
        bool b = false;
        ThreadPool.QueueUserWorkItem(o =>
        {
            b = true;
        });

        while (!Volatile.Read(ref b))
            ;
    }

    [Benchmark]
    public void TaskRun()
    {
        bool b = false;
        Task.Run(() =>
        {
            b = true;
        });

        while (!Volatile.Read(ref b))
            ;
    }
}

[MemoryDiagnoser]
public class StringConcatVSStringBuilderBenchmark
{
    public string
        str1, str2, str3, str4, str5;

    public StringBuilder SB;
    public StringConcatVSStringBuilderBenchmark()
    {
        str1 = new string('1', 50);
        str2 = new string('1', 150);
        str3 = new string('1', 300);
        str4 = new string('1', 500);
        str5 = new string('1', 1500);

        SB = new StringBuilder(str1.Length + str2.Length + str3.Length + str4.Length + str5.Length);
    }

    [Benchmark(Baseline = true)]
    public string StringConcat()
    {
        return str1 + str2 + str3 + str4 + str5;
    }

    [Benchmark]
    public string StringConcatBySteps()
    {
        var resultString = "";
        resultString += str1;
        resultString += str2;
        resultString += str3;
        resultString += str4;
        resultString += str5;

        return resultString;
    }

    [Benchmark]
    public string StringBuilder()
    {
        var sb = new StringBuilder();
        sb
            .Append(str1)
            .Append(str2)
            .Append(str3)
            .Append(str4)
            .Append(str5);

        return sb.ToString();
    }

    [Benchmark]
    public string StringBuilderWithCapacity()
    {
        var sb = new StringBuilder(str1.Length + str2.Length + str3.Length + str4.Length + str5.Length);
        sb
            .Append(str1)
            .Append(str2)
            .Append(str3)
            .Append(str4)
            .Append(str5);

        return sb.ToString();
    }

    [Benchmark]
    public string StringBuilderInitialized()
    {
        SB.Clear();
        SB
            .Append(str1)
            .Append(str2)
            .Append(str3)
            .Append(str4)
            .Append(str5);

        return SB.ToString();
    }
}