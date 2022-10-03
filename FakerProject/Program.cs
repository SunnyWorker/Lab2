// See https://aka.ms/new-console-template for more information


using System.Threading.Channels;
using Faker;
using Faker.generators;
using Faker.test_objects;

var config = new FakerConfig();
var faker = new Faker.Faker(config);
config.Add<Test,int,AGenerator>(test => test.b);
var i = faker.Create<Test>(); // 542
Console.WriteLine(i);
//TODO find solving for config
