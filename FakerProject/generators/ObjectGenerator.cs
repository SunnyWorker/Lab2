using System.Collections;
using System.Reflection;

namespace Faker.generators;

public class ObjectGenerator : IValueGenerator
{

    public object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        if (context.CycleControl.ContainsKey(typeToGenerate))
        {
            if (context.CycleControl[typeToGenerate] >= 2) return null;
            context.CycleControl.AddOrUpdate(typeToGenerate,context.CycleControl[typeToGenerate],((type, i) => i+1));
        }
        else context.CycleControl.TryAdd(typeToGenerate,1);
        ConstructorInfo[] constructorInfos =
            typeToGenerate.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        List<object?> parameters = new List<object?>();
        object? resultObject;
        if (constructorInfos.Length == 0) resultObject = Activator.CreateInstance(typeToGenerate);
        else {
            ConstructorInfo chosenConstructor = constructorInfos[0];
            foreach (var vConstructorInfo in constructorInfos) //find constructor with the most parameters
            {
                if (vConstructorInfo.GetParameters().Length > chosenConstructor.GetParameters().Length) 
                    chosenConstructor = vConstructorInfo;
            }

            foreach (var parameterInfo in chosenConstructor.GetParameters())
            {
                parameters.Add(context.Faker.Create(parameterInfo.ParameterType));
            }

            resultObject = chosenConstructor.Invoke(parameters.ToArray());
        }


        foreach (var property in typeToGenerate.GetProperties())
        {
            if (property.GetValue(resultObject) != GetDefaultValue(property.PropertyType) && 
                !property.GetValue(resultObject).Equals(GetDefaultValue(property.PropertyType))) continue;
            property.SetValue(resultObject,context.Faker.Create(property.PropertyType));
        }
        
        foreach (var field in typeToGenerate.GetFields())
        {
            if (field.GetValue(resultObject) != GetDefaultValue(field.FieldType) && 
                !field.GetValue(resultObject).Equals(GetDefaultValue(field.FieldType))) continue;
            field.SetValue(resultObject,context.Faker.Create(field.FieldType));
        }
        return resultObject;

    }

    public bool CanGenerate(Type type)
    {
        return true;
    }
    
    private object GetDefaultValue(Type t)
    {
        if (t.IsValueType)
            return Activator.CreateInstance(t);
        return null;
    }
}