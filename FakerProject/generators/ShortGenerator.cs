namespace Faker.generators;

public class ShortGenerator : IValueGenerator
{
    public object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        return context.Random.NextInt64(Int16.MinValue, Int16.MaxValue);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(long);
    }
}