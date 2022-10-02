using System.Collections;

namespace Faker.generators;

public class ListGenerator : IValueGenerator
{
    public object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        Type genericType = typeToGenerate.GetGenericArguments()[0]; //IList<Test> -> Test
        int count = Math.Abs(context.Random.Next(10,50));
        var resultList = (IList)Activator.CreateInstance(typeToGenerate);
        for (int i = 0; i < 10; i++)
        {
            resultList.Add(context.Faker.Create(genericType));
        }

        return resultList;
    }

    public bool CanGenerate(Type type)
    {
        return type.GetInterfaces().Contains(typeof(IList));
    }
    //typeof(List<Test>).GetWithNoGenerics == List
}