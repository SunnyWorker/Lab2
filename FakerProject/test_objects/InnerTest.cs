namespace Faker.test_objects;

public struct InnerTest
{
    public string randomString;
    public override string ToString()
    {
        return "{"+randomString+"}";
    }
}