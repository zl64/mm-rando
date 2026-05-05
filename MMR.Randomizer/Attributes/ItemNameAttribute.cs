namespace MMR.Randomizer.Attributes;

public class ItemNameAttribute : Attribute
{
    public string Name { get; private set; }

    public ItemNameAttribute(string name)
    {
        Name = name;
    }
}
