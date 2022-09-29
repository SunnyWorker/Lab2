namespace Faker.generators;

public class FloatGenerator : IValueGenerator
{
    public object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        return context.Random.NextSingle()*float.MaxValue*(context.Random.Next(0,2)*2-1);

    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(float);
    }
}