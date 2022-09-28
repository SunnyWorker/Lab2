namespace Faker;

public class Faker : IFaker
{
    private Random random;

    public Faker()
    {
        random = new Random();
    }

    public T Create<T>()
    {
        if (typeof(T).Equals(typeof(int))) return T
    }

    private int GetRandomInteger()
    {
        return random.Next(Int32.MinValue, Int32.MaxValue);
    }
    
    private double GetRandomDouble()
    {
        return random.NextDouble()*Double.MaxValue*(random.Next(0,1)*2-1);
    }
    
    private float GetRandomFloat()
    {
        return random.NextSingle()*float.MaxValue*(random.Next(0,1)*2-1);
    }
    
    private float GetRandomLong()
    {
        return random.NextInt64(Int64.MinValue, Int64.MaxValue);
    }
    
    private float GetRandomBoolean()
    {
        return random.NextInt64(Int64.MinValue, Int64.MaxValue);
    }
    
    
}