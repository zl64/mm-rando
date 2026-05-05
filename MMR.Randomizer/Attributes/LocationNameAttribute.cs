namespace MMR.Randomizer.Attributes;

public class LocationNameAttribute : Attribute
{
    public string Name { get; private set; }

    public LocationNameAttribute(string name)
    {
        Name = name;
    }
}
