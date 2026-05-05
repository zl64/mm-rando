using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class StartingTingleMapAttribute : Attribute
{
    public TingleMap TingleMap { get; }

    public StartingTingleMapAttribute(TingleMap tingleMap)
    {
        TingleMap = tingleMap;
    }
}
