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
        var configDictionary = context.Faker.config.GetTypeConfig(typeToGenerate);
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
                if(configDictionary.ContainsKey(parameterInfo.Name)) parameters.Add(configDictionary[parameterInfo.Name].Generate(parameterInfo.ParameterType,context));
                else parameters.Add(context.Faker.Create(parameterInfo.ParameterType));
            }

            resultObject = chosenConstructor.Invoke(parameters.ToArray());
        }


        foreach (var property in typeToGenerate.GetProperties())
        {
            
            if (property.GetValue(resultObject) != context.Faker.GetDefaultValue(property.PropertyType) && 
                !property.GetValue(resultObject).Equals(context.Faker.GetDefaultValue(property.PropertyType))) continue;
            if (configDictionary.ContainsKey(property.Name))
            {
                property.SetValue(resultObject,configDictionary[property.Name].Generate(property.PropertyType,context));
                continue;
            }
            property.SetValue(resultObject,context.Faker.Create(property.PropertyType));
        }
        
        foreach (var field in typeToGenerate.GetFields())
        {
            if (field.GetValue(resultObject) != context.Faker.GetDefaultValue(field.FieldType) && 
                !field.GetValue(resultObject).Equals(context.Faker.GetDefaultValue(field.FieldType))) continue;
            if (configDictionary.ContainsKey(field.Name))
            {
                field.SetValue(resultObject,configDictionary[field.Name].Generate(field.FieldType,context));
                continue;
            }
            field.SetValue(resultObject,context.Faker.Create(field.FieldType));
        }
        return resultObject;

    }

    public bool CanGenerate(Type type)
    {
        return !type.IsPrimitive && !type.GetInterfaces().Contains(typeof(IList)) && type!=typeof(string);
    }
    

}