# Программы, которые пишут себя сами | Метапрограммирование в C#/.NET: Начало | Expression Trees

https://www.youtube.com/watch?v=T7ogOUPeFhI&t=3790s&ab_channel=DevJungles

[Reflection] [Expression] [BenchmarkDotNet]

Создаем экземпляр класса, используя:
- конструктор;
- рефлексию;
- выражения.

Измеряем производительность всех способов создания с помощью библиотеки [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet)

Результаты:

|                Method |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD |
|---------------------- |-----------:|----------:|----------:|-----------:|------:|--------:|
|           CompileTime |   6.851 ns | 0.2694 ns | 0.7420 ns |   6.611 ns |  1.00 |    0.00 |
| WithReflectionNoCache | 292.458 ns | 5.8869 ns | 9.5062 ns | 293.358 ns | 43.61 |    4.43 |
|  WithReflectionCached | 220.466 ns | 4.2812 ns | 5.5668 ns | 221.151 ns | 32.98 |    3.24 |
|        WithExpression |  10.828 ns | 0.2137 ns | 0.3263 ns |  10.854 ns |  1.63 |    0.15 |