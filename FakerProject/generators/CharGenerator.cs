namespace Faker.generators;

public class CharGenerator : IValueGenerator
{
    public object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        return (char)context.Random.Next(0, 1024);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(char);
    }
}