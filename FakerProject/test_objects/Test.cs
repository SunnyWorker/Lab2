namespace Faker.test_objects;

public class Test
{
    private int a;
    public int b
    {
        get;
    }

    public InnerTest InnerTest
    {
        get;
        set;
    }

    public Test(int b)
    {
        this.b = b;
    }

    public override string ToString()
    {
        return a + "\n" + b + "\n" + InnerTest + "\n";
    }
}