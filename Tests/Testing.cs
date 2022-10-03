using Faker;
using Faker.generators;
using Faker.test_objects;

namespace Tests;

[TestClass]
public class Testing
{
    
    [TestMethod]
    public void TestFakeBoolean()
    {
        var config = new FakerConfig();
        var faker = new Faker.Faker(config);
        var obj = faker.Create<Boolean>(); // 542
        Assert.IsNotNull(obj);
        Assert.IsTrue(obj || !obj);
    }
    
    [TestMethod]
    public void TestFakeString()
    {
        var config = new FakerConfig();
        var faker = new Faker.Faker(config);
        var obj = faker.Create<String>(); // 542
        Assert.IsNotNull(obj);
        Assert.IsTrue(obj.GetType()==typeof(string));
        Assert.IsTrue(obj.Length>=10 && obj.Length<50);
    }
    
    [TestMethod]
    public void TestFakeList()
    {
        var config = new FakerConfig();
        var faker = new Faker.Faker(config);
        var obj = faker.Create<List<int>>(); // 542
        Assert.IsNotNull(obj);
        Assert.IsTrue(obj.Count>=5 && obj.Count<20);
        for (var i = 0; i < obj.Count; i++)
        {
            Assert.IsNotNull(obj[i]);
            Assert.IsTrue(typeof(int)==obj[i].GetType());
            Assert.AreNotEqual(obj[i],faker.GetDefaultValue(obj[i].GetType()));
        }
    }
    
    [TestMethod]
    public void TestFakeListOfList()
    {
        var config = new FakerConfig();
        var faker = new Faker.Faker(config);
        var obj = faker.Create<List<List<int>>>(); // 542
        Assert.IsNotNull(obj);
        Assert.IsTrue(obj.GetType()==typeof(List<List<int>>));
        Assert.IsTrue(obj.Count>=5 && obj.Count<20);
        for (var i = 0; i < obj.Count; i++)
        {
            Assert.IsNotNull(obj[i]);
            Assert.IsTrue(typeof(List<int>)==obj[i].GetType());
            Assert.IsTrue(obj[i].Count>=5 && obj[i].Count<20);
            for (var j = 0; j < obj[i].Count; j++)
            {
                Assert.IsNotNull(obj[i][j]);
                Assert.IsTrue(typeof(int)==obj[i][j].GetType());
            }
        }
    }
    
    [TestMethod]
    public void TestFakeObject()
    {
        var config = new FakerConfig();
        var faker = new Faker.Faker(config);
        var obj = faker.Create<Test>();
        Assert.IsNotNull(obj);
        Assert.AreNotEqual(faker.GetDefaultValue(obj.b.GetType()),obj.b);
        Assert.IsNotNull(obj.InnerTest);
        Assert.AreNotEqual(obj.InnerTest.randomString,faker.GetDefaultValue(obj.InnerTest.randomString.GetType()));
    }
    
    [TestMethod]
    public void TestFakeDateTime()
    {
        var config = new FakerConfig();
        var faker = new Faker.Faker(config);
        var obj = faker.Create<DateTime>();
        Console.WriteLine(obj);
        Assert.IsNotNull(obj);
        Assert.AreEqual(obj.GetType(),typeof(DateTime));
    }
    
    [TestMethod]
    public void TestFakerConfig()
    {
        var config = new FakerConfig();
        config.Add<Test,int,BGenerator>(test => test.b);
        var faker = new Faker.Faker(config);
        var obj = faker.Create<Test>();
        Assert.IsNotNull(obj);
        Assert.AreEqual(100,obj.b);
        Assert.IsNotNull(obj.InnerTest);
        Assert.AreNotEqual(obj.InnerTest.randomString,faker.GetDefaultValue(obj.InnerTest.randomString.GetType()));
    }
    
    [TestMethod]
    public void TestCycleSituation()
    {
        var config = new FakerConfig();
        var faker = new Faker.Faker(config);
        var i = faker.Create<A>(); // 542
        Assert.AreEqual(null,i.b.a.b.a);
    }
}