namespace Faker.test_objects;

public class Test
{
    public int a;
    public int b;

    private Test(int a)
    {
        this.a = a;
    }

    public override string ToString()
    {
        return a + " " + b + "\n";
    }
}