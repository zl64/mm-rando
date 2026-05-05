using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Attributes;

public class RegionAreaAttribute : Attribute
{
    public RegionArea RegionArea { get; }

    public RegionAreaAttribute(RegionArea regionArea)
    {
        RegionArea = regionArea;
    }
}
