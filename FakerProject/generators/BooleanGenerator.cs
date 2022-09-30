using System.Collections;

namespace Faker.generators;

public class BooleanGenerator : IValueGenerator
{
    public object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        return context.Random.Next(0, 2).Equals(1);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(bool);
    }
}