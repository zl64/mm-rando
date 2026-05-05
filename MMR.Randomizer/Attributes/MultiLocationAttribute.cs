using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Attributes;

public class MultiLocationAttribute : Attribute
{
    public Item[] Locations { get; }

    public MultiLocationAttribute(params Item[] locations)
    {
        Locations = locations;
    }
}
