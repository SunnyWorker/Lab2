namespace Faker.test_objects;

public class InnerTest
{
    public string randomString;
    public override string ToString()
    {
        return "{"+randomString+"}";
    }
}