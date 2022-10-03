namespace Faker.generators;

public class StringGenerator : IValueGenerator
{
    public object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        int size = context.Random.Next(10,50);
        string? s = "";
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = (char) (Math.Abs((char)context.Faker.Create(typeof(char))) % 255);
            s += ch;
        }

        return s;
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(string);
    }
}