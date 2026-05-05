using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Attributes;

public class MainLocationAttribute : Attribute
{
    public Item Location { get; }

    public MainLocationAttribute(Item location)
    {
        Location = location;
    }
}
