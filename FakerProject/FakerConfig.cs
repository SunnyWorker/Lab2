using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using Faker.generators;

namespace Faker;

public class FakerConfig
{
    private Dictionary<Type, Dictionary<string,IValueGenerator>> FieldDictionary
    {
        get;
    } 
    public void Add<C, F, G>(Expression<Func<C,F>> expression) where G : IValueGenerator
    {
        if (!FieldDictionary.ContainsKey(typeof(C)))
        {
            FieldDictionary.Add(typeof(C),new());
        }
        FieldDictionary.GetValueOrDefault(typeof(C))?.Add((expression.Body as MemberExpression)?.Member.Name,Activator.CreateInstance<G>());

    }

    public FakerConfig()
    {
        FieldDictionary = new Dictionary<Type, Dictionary<string, IValueGenerator>>();
    }

    public void Change<F>(F f, Faker faker, ConcurrentDictionary<Type, int> CycleControl)
    {
        var typeDictionary = FieldDictionary.GetValueOrDefault(typeof(F), new());
        var fields = typeof(F).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        var properties = typeof(F).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var field in fields)
        {
            var generator = typeDictionary.GetValueOrDefault(field.Name, null);
            if(generator!=null && generator.CanGenerate(field.FieldType))
            {
                field.SetValue(f,generator.Generate(f.GetType(),new (CycleControl,new Random(),faker)));
            }
        }
        
        foreach (var property in properties)
        {
            var generator = typeDictionary.GetValueOrDefault(property.Name, null);
            if(generator!=null && generator.CanGenerate(property.PropertyType))
            {
                try
                {
                    property.SetValue(f,generator.Generate(f.GetType(),new (CycleControl,new Random(),faker)));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }

    public Dictionary<string, IValueGenerator> GetTypeConfig(Type t)
    {
        if(FieldDictionary.ContainsKey(t)) 
        return FieldDictionary[t];
        return new();
    }
}