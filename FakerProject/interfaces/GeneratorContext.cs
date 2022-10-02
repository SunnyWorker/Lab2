using System.Collections.Concurrent;

namespace Faker.generators;

public class GeneratorContext
{
    public Random Random { get; }
    public IFaker Faker { get; }

    public ConcurrentDictionary<Type, int> CycleControl;

    public GeneratorContext(ConcurrentDictionary<Type, int> cycleControl, Random random, IFaker faker)
    {
        CycleControl = cycleControl;
        Random = random;
        Faker = faker;
    }
}