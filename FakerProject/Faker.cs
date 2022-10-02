using System.Collections;
using System.Runtime.CompilerServices;
using Faker.generators;

namespace Faker;

public class Faker : IFaker
{
    private FakerConfig config; 
    private List<IValueGenerator> Generators
    {
        get;
    }

    public T Create<T>()
    {
        var fakeObject = (T) Create(typeof(T));
        config.Change<T>(fakeObject);
        return fakeObject;
    }

    public object? Create(Type t)
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


    public Faker(FakerConfig config)
    {
        this.config = config;
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
        Generators.Add(new ListGenerator());
        Generators.Add(new ObjectGenerator());
    }
}