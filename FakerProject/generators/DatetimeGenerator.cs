namespace Faker.generators;

public class DatetimeGenerator : IValueGenerator
{
    public object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        return DateTime.FromBinary(Math.Abs((long)context.Faker.Create(typeof(long))));
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(DateTime);
    }
}