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

    public Dictionary<string, IValueGenerator> GetTypeConfig(Type t)
    {
        if(FieldDictionary.ContainsKey(t)) 
        return FieldDictionary[t];
        return new();
    }
}