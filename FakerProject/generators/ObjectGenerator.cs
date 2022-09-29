using System.Reflection;

namespace Faker.generators;

public class ObjectGenerator : IValueGenerator
{

    public object? Generate(Type typeToGenerate, GeneratorContext context)
    {
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
                var generator = context.Faker.GetGenerator(parameterInfo.ParameterType);
                parameters.Add(generator.Generate(parameterInfo.ParameterType,context));
            }

            resultObject = chosenConstructor.Invoke(parameters.ToArray());
        }


        foreach (var property in typeToGenerate.GetProperties())
        {
            if (property.GetValue(resultObject) != GetDefaultValue(property.PropertyType) && 
                !property.GetValue(resultObject).Equals(GetDefaultValue(property.PropertyType))) continue;
            var generator = context.Faker.GetGenerator(property.PropertyType); 
            property.SetValue(resultObject,generator.Generate(property.PropertyType,context));
        }
        
        foreach (var field in typeToGenerate.GetFields())
        {
            if (field.GetValue(resultObject) != GetDefaultValue(field.FieldType) && 
                !field.GetValue(resultObject).Equals(GetDefaultValue(field.FieldType))) continue;
            var generator = context.Faker.GetGenerator(field.FieldType); 
            field.SetValue(resultObject,generator.Generate(field.FieldType,context));
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