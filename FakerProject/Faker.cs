using System.Runtime.CompilerServices;
using Faker.generators;

namespace Faker;

public class Faker : IFaker
{
    private List<IValueGenerator> Generators
    {
        get;
    }
    public Faker()
    {
        Generators = new List<IValueGenerator>();
        Generators.Add(new IntegerGenerator());
        Generators.Add(new BooleanGenerator());
        Generators.Add(new LongGenerator());
        Generators.Add(new CharGenerator());
        Generators.Add(new DoubleGenerator());
        Generators.Add(new FloatGenerator());
        Generators.Add(new ByteGenerator());
        Generators.Add(new ShortGenerator());
        Generators.Add(new StringGenerator());
        Generators.Add(new ObjectGenerator());
    }

    public T Create<T>()
    {
        return (T) Create(typeof(T));
    }

    private object? Create(Type t)
    {
        return GetGenerator(t).Generate(t,new GeneratorContext(new Random(),this));
    }

    public IValueGenerator GetGenerator(Type t)
    {
        foreach (var generator in Generators)
        {
            if (generator.CanGenerate(t)) return generator;
        }

        return null;
    }
    
    

    
    

    
    
}