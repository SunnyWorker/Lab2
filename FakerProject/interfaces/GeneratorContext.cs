using System.Collections.Concurrent;

namespace Faker.generators;

public class GeneratorContext
{
    public Random Random { get; }
    public Faker Faker { get; }

    public ConcurrentDictionary<Type, int> CycleControl;

    public GeneratorContext(ConcurrentDictionary<Type, int> cycleControl, Random random, Faker faker)
    {
        CycleControl = cycleControl;
        Random = random;
        Faker = faker;
    }
}