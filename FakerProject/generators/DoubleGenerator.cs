namespace Faker.generators;

public class DoubleGenerator : IValueGenerator
{
    public object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        return context.Random.NextDouble()*Double.MaxValue*(context.Random.Next(0,2)*2-1);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(double);
    }
}