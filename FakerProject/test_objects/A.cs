namespace Faker.test_objects;

public class A
{
    public B b;
    public char symbol;
    public override string ToString()
    {
        return "{\n" +
               "A: \n" +
               "symbol = " + symbol + "\n" +
               "b = " + b + "\n" +
               "}";
    }
}