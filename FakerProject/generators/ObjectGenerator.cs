using System.Reflection;

namespace Faker.generators;

public class ObjectGenerator : IValueGenerator
{

    public object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        ConstructorInfo[] constructorInfos =
            typeToGenerate.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        Dictionary<string, object?> parameters = new Dictionary<string, object?>();
        object? result;
        if (constructorInfos.Length == 0) result = Activator.CreateInstance(typeToGenerate);
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
                parameters.Add(parameterInfo.Name,generator.Generate(parameterInfo.ParameterType,context));
            }

            result = chosenConstructor.Invoke(parameters.Values.ToArray());
        }


        foreach (var property in typeToGenerate.GetProperties())
        {
            if(parameters.ContainsKey(property.Name)) continue;
            var generator = context.Faker.GetGenerator(property.PropertyType); 
            property.SetValue(result,generator.Generate(property.PropertyType,context));
        }
        
        foreach (var field in typeToGenerate.GetFields())
        {
            if(parameters.ContainsKey(field.Name)) continue;
            var generator = context.Faker.GetGenerator(field.FieldType); 
            field.SetValue(result,generator.Generate(field.FieldType,context));
        }

        return result;

    }

    public bool CanGenerate(Type type)
    {
        return true;
    }
}