using System;
using System.Linq;
using Expressive;
using Expressive.Expressions;
using Expressive.Functions;

namespace FullHealth.Expressions;

public class RandomListFunction : FunctionBase
{
    public const string FunctionName = "RandomList";

    public override object Evaluate(IExpression[] parameters, Context context)
    {
        if (parameters is not { Length: > 0 })
        {
            throw new($"{Name}() expects at least {1} argument(s)");
        }

        var values = parameters.Select(parameter => parameter.Evaluate(Variables)).ToList();

        if (values.Count == 1)
        {
            return values[0];
        }

        var random = new Random(DateTime.UtcNow.Millisecond);
        return values[random.Next(0, values.Count)];
    }

    public override string Name => FunctionName;
    public static readonly RandomListFunction Instance = new();
}