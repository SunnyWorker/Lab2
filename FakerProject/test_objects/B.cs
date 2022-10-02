namespace Faker.test_objects;

public class B
{
    public int number;
    public A a;
    public override string ToString()
    {
        return "{\n" +
               "B: \n" +
               "number = " + number + "\n" +
               "a = " + a + "\n" +
               "}";
    }
}