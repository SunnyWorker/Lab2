namespace Faker.generators;

public class ListGenerator : IValueGenerator
{
    public object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        return context.Random.Next(Byte.MinValue, Byte.MaxValue);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(byte);
    }
}