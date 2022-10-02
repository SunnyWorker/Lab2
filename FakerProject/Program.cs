// See https://aka.ms/new-console-template for more information


using System.Threading.Channels;
using Faker;
using Faker.generators;
using Faker.test_objects;

var config = new FakerConfig();
config.Add<Test, int, AGenerator>(test => test.a);
var faker = new Faker.Faker(config);
var i = faker.Create<A>(); // 542
Console.WriteLine(i);
//TODO tests
