namespace Faker;

public interface IFaker
{
    public T Create<T>();
}