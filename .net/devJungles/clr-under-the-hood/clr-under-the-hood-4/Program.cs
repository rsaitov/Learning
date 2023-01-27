// .NET Threads

using Microsoft.Win32;
using System.Net;

Console.WriteLine("--> Asm");

Memory.Write(0, "x");

var program1 = new List<IAtomicOperation>
{
    new PutConstantToRegister(0, 0),
    new WriteOperation("local1")
};

program1.AddRange(new WhileOperation(
    condition: new IOperation[]
    {
        new ReadOperation("local1", 0),
        new PutConstantToRegister(10, 1),
        new IsLtOperation()
    },
    body: new IOperation[]
    {
        new IncrementOperation("x"),
        new IncrementOperation("local1")
    }).GetOperations());

var program2 = new List<IAtomicOperation>
{
    new PutConstantToRegister(0, 0),
    new WriteOperation("local2")
};

program2.AddRange(new WhileOperation(
    condition: new IOperation[]
    {
        new ReadOperation("local2", 0),
        new PutConstantToRegister(10, 1),
        new IsLtOperation()
    },
    body: new IOperation[]
    {
        new IncrementOperation("x"),
        new IncrementOperation("local2")
    }).GetOperations());

var thread1 = new ExecutionThread(program1.ToArray());
var thread2 = new ExecutionThread(program2.ToArray());
new ThreadPlanner(thread1, thread2).Execute();
Console.WriteLine(Memory.Read<int>("x"));


internal class ThreadPlanner
{
    private readonly List<ExecutionThread> _threads;

    public ThreadPlanner(params ExecutionThread[] threads)
    {
        this._threads = threads.ToList();
    }

    public void Execute()
    {
        var random = new Random();
        while (_threads.Count > 0)
        {
            foreach (var thread in _threads.ToArray())
            {
                var x = random.Next(1, 20);
                for (int i = 0; i < x; i++)
                {
                    Console.CursorLeft = _threads.IndexOf(thread) * 30;
                    if (!thread.ExecuteNextOperation())
                    {
                        _threads.Remove(thread);
                        break;
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}

internal class ExecutionThread
{
    private readonly IAtomicOperation[] operations;
    private ExecutionContext executionContext = new ExecutionContext(0, new object[2]);

    public ExecutionThread(IAtomicOperation[] operations)
    {
        this.operations = operations;
    }

    public bool ExecuteNextOperation()
    {
        var currentOperation = operations[executionContext.Current];
        executionContext = currentOperation.Execute(executionContext);

        return executionContext.Current < operations.Length;
    }

    public void Execute()
    {
        while (ExecuteNextOperation())
            ;
    }
}

static class Memory
{
    private static readonly Dictionary<string, object> _ram = new Dictionary<string, object>();

    public static T Read<T>(string address)
    {
        return (T)_ram[address];
    }

    public static void Write<T>(T obj, string address)
    {
        _ram[address] = obj;
    }
}

interface IOperation
{
    IEnumerable<IAtomicOperation> GetOperations();
}

interface IAtomicOperation
{

    ExecutionContext Execute(ExecutionContext executionContext);
}

class AtomicOperation : IAtomicOperation, IOperation
{
    public virtual ExecutionContext Execute(ExecutionContext executionContext)
    {
        return new ExecutionContext(executionContext.Current + 1, executionContext.Registers);
    }

    public IEnumerable<IAtomicOperation> GetOperations()
    {
        yield return this;
    }
}

class GotoOperation : AtomicOperation
{
    private readonly int relativeIndex;

    public GotoOperation(int relativeIndex)
    {
        this.relativeIndex = relativeIndex;
    }
    public override ExecutionContext Execute(ExecutionContext executionContext)
    {
        Console.WriteLine($"goto {relativeIndex}|{executionContext.Current + relativeIndex}");
        return new ExecutionContext(executionContext.Current + relativeIndex, executionContext.Registers);
    }
}

class ReadOperation : AtomicOperation
{
    private readonly string address;
    private readonly int registry;

    public ReadOperation(string address, int registry)
    {
        this.address = address;
        this.registry = registry;
    }

    public override ExecutionContext Execute(ExecutionContext executionContext)
    {
        Console.WriteLine($"[{registry}]->{address}");
        executionContext.Registers[registry] = Memory.Read<object>(address);
        return base.Execute(executionContext);
    }
}

class WriteOperation : AtomicOperation
{
    private readonly string address;

    public WriteOperation(string address)
    {
        this.address = address;
    }
    public override ExecutionContext Execute(ExecutionContext executionContext)
    {
        Console.WriteLine($"{address}->[0]");
        Memory.Write(executionContext.Registers[0], address);
        return base.Execute(executionContext);
    }
}

class IsGtOperation : AtomicOperation
{
    public override ExecutionContext Execute(ExecutionContext context)
    {
        var x = (int)context.Registers[0];
        var y = (int)context.Registers[1];
        context.Registers[0] = x > y; 
        Console.WriteLine($"[0]{x} > [1]{y}");
        return base.Execute(context);
    }
}

class IsLtOperation : AtomicOperation
{
    public override ExecutionContext Execute(ExecutionContext context)
    {
        var x = (int)context.Registers[0];
        var y = (int)context.Registers[1];
        context.Registers[0] = x < y;
        Console.WriteLine($"[0]{x} < [1]{y}");
        return base.Execute(context);
    }
}

class AddOperation : AtomicOperation
{
    public override ExecutionContext Execute(ExecutionContext context)
    {
        var left = (int)context.Registers[0];
        var right = (int)context.Registers[1];
        context.Registers[0] = left + right;
        Console.WriteLine($"[0]{left} + [1]{right}");

        return base.Execute(context);
    }
}

static class OperationExtensions
{

    public static IEnumerable<IAtomicOperation> Flattern(this IEnumerable<IOperation> operations)
    {
        foreach (var operation in operations)
        {
            if (operation is IAtomicOperation atomic)
                yield return atomic;
            else
            {
                foreach (var op in operation.GetOperations())
                {
                    yield return op;
                }
            }
        }
    }
}

class IfOperation : IOperation
{
    class IfAtomic : IAtomicOperation
    {
        public ExecutionContext Execute(ExecutionContext executionContext)
        {
            var result = (bool)executionContext.Registers[0];
            return new ExecutionContext(executionContext.Current + (result ? 1 : 2), executionContext.Registers);
        }
    }

    private readonly IOperation[] condition;
    private readonly IOperation[] ifTrueClause;
    private readonly IOperation[] ifFalseClause;

    public IfOperation(IOperation[] condition, IOperation[] ifTrueClause, IOperation[] ifFalseClause)
    {
        this.condition = condition;
        this.ifTrueClause = ifTrueClause;
        this.ifFalseClause = ifFalseClause;
    }

    public IEnumerable<IAtomicOperation> GetOperations()
    {
        var condtionOps = condition.Flattern();
        foreach (var operation in condtionOps)
            yield return operation;

        yield return new IfAtomic();
        yield return new GotoOperation(2);
        yield return new GotoOperation(2 + ifTrueClause.Flattern().Count());

        foreach (var operation in ifTrueClause.Flattern())
            yield return operation;

        yield return new GotoOperation(1 + ifFalseClause.Flattern().Count());

        foreach (var operation in ifFalseClause.Flattern())
            yield return operation;
    }
}

class WhileOperation : IOperation
{
    private readonly IOperation[] condition;
    private readonly IOperation[] body;

    public WhileOperation(IOperation[] condition, IOperation[] body)
    {
        this.condition = condition;
        this.body = body;
    }

    public IEnumerable<IAtomicOperation> GetOperations()
    {
        var ifOperationCount = new IfOperation(condition, body, new IOperation[0]).GetOperations().Count();
        var gt = new GotoOperation(-ifOperationCount);
        return new IfOperation(condition, body.Concat(new[] { gt }).ToArray(), new IOperation[0]).GetOperations();
    }
}

class ExecuteOperation : AtomicOperation
{
    private readonly Action<ExecutionContext> _action;

    public ExecuteOperation(Action<ExecutionContext> action)
    {
        _action = action;
    }

    public override ExecutionContext Execute(ExecutionContext context)
    {
        _action(context);
        return base.Execute(context);
    }
}

class PutConstantToRegister : AtomicOperation
{
    private readonly object _constant;
    private readonly int _register;

    public PutConstantToRegister(object constant, int register)
    {
        this._register = register;
        this._constant = constant;
    }

    public override ExecutionContext Execute(ExecutionContext executionContext)
    {
        executionContext.Registers[_register] = _constant;
        return base.Execute(executionContext);
    }
}

class IncrementOperation : IOperation
{
    private readonly string _address;

    public IncrementOperation(string address)
    {
        _address = address;
    }

    public IEnumerable<IAtomicOperation> GetOperations()
    {
        yield return new ReadOperation(_address, 0);
        yield return new PutConstantToRegister(1, 1);
        yield return new AddOperation();
        yield return new WriteOperation(_address);
    }
}

class ExecutionContext
{
    public int Current { get; }
    public object[] Registers { get; }


    public ExecutionContext(int current, object[] registers)
    {
        Current = current;
        Registers = registers;
    }
}