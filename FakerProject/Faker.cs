using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;
using Faker.generators;

namespace Faker;

public class Faker : IFaker
{
    public FakerConfig config;
    private ConcurrentDictionary<Type, int> CycleControl = new();
    private List<IValueGenerator> Generators
    {
        get;
    }

    public T Create<T>()
    {
        CycleControl.Clear();
        var fakeObject = (T) Create(typeof(T));
        //config.Change<T>(fakeObject,this,CycleControl);
        return fakeObject;
    }

    public object? Create(Type t)
    {
        return GetGenerator(t).Generate(t,new GeneratorContext(CycleControl, new Random(),this));
    }

    private IValueGenerator GetGenerator(Type t)
    {
        foreach (var generator in Generators)
        {
            if (generator.CanGenerate(t)) return generator;
        }

        return null;
    }

    public object GetDefaultValue(Type t)
    {
        if (t.IsValueType)
            return Activator.CreateInstance(t);
        return null;
    }

    public Faker(FakerConfig config)
    {
        Generators = new();
        CycleControl = new();
        this.config = config;
        Assembly assembly = Assembly.GetExecutingAssembly();
        String path = "..\\..\\..\\..\\FakerProject\\generators";
        foreach (var file in Directory.GetFiles(path))
        {
            Generators.Add((IValueGenerator)assembly.CreateInstance("Faker.generators."+Path.GetFileNameWithoutExtension(file)));
        }

    }
}