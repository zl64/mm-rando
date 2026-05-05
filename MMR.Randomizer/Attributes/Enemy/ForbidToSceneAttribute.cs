using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Attributes.Enemy;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class ForbidToSceneAttribute : Attribute
{
    public Scene Scene { get; }
    public ForbidToSceneAttribute(Scene scene)
    {
        Scene = scene;
    }
}
