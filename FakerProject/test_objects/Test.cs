namespace Faker.test_objects;

public class Test
{
    public int a;
    public int b;

    public InnerTest InnerTest;

    private Test(int a)
    {
        this.a = a;
    }

    public override string ToString()
    {
        return a + "\n" + b + "\n" + InnerTest + "\n";
    }
}