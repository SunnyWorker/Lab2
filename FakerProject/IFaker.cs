using Faker.generators;

namespace Faker;

public interface IFaker
{
    public T Create<T>();
    public IValueGenerator GetGenerator(Type t);
}