// See https://aka.ms/new-console-template for more information


using System.Threading.Channels;
using Faker;
using Faker.generators;
using Faker.test_objects;

var config = new FakerConfig();
IValueGenerator generator = new ListGenerator();
config.Add<Test, int, AGenerator>(test => test.a);
var faker = new Faker.Faker(config);
var i = faker.Create<Test>(); // 542
Console.WriteLine(i);
//TODO Cycle situation
//TODO tests
//TODO dynamic adding of generators