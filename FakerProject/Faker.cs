using System.Collections;
using System.Reflection;
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

    private IValueGenerator GetGenerator(Type t)
    {
        foreach (var generator in Generators)
        {
            if (generator.CanGenerate(t)) return generator;
        }

        return null;
    }


    public Faker(FakerConfig config)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        this.config = config;
        String path = "..\\..\\..\\generators";
        Generators = new();
        foreach (var file in Directory.GetFiles(path))
        {
            Generators.Add((IValueGenerator)assembly.CreateInstance("Faker.generators."+Path.GetFileNameWithoutExtension(file)));
        }

    }
}