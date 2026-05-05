using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Attributes.Entrance;

public class PairAttribute : Attribute
{
    public Item Pair { get; set; }

    public PairAttribute(Item pair)
    {
        Pair = pair;
    }
}
