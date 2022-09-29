// See https://aka.ms/new-console-template for more information


using System.Threading.Channels;
using Faker.test_objects;

var faker = new Faker.Faker();
var i = faker.Create<Test>(); // 542
Console.WriteLine(i);