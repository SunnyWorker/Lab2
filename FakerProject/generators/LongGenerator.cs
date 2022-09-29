namespace Faker.generators;

public class LongGenerator : IValueGenerator
{
    public object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        return context.Random.NextInt64(Int64.MinValue, Int64.MaxValue);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(long);
    }
}
