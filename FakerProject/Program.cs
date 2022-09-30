// See https://aka.ms/new-console-template for more information


using System.Threading.Channels;
using Faker.generators;
using Faker.test_objects;

var faker = new Faker.Faker();
IValueGenerator generator = new ListGenerator();
//List<Test> tests = (List<Test>)generator.Generate<Test>(typeof(List<Test>),new GeneratorContext(new Random(),faker));
var i = faker.Create<List<Test>>(); // 542
foreach (var test in i)
{
    Console.WriteLine(test);
}
Console.WriteLine(i);