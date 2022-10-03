namespace Faker.generators;

public class BGenerator : IValueGenerator
{
    public object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        return 100;
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(int);
    }
}