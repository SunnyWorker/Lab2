namespace Faker.generators;

public class StringGenerator : IValueGenerator
{
    public object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        int size = 100;
        string? s = "";
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = (char) (Math.Abs(context.Faker.Create<char>()) % 255);
            s += ch;
        }

        return s;
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(string);
    }
}