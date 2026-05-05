using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Attributes.Enemy;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class ForbidFromSceneAttribute : Attribute
{
    public Scene Scene { get; }
    public ForbidFromSceneAttribute(Scene scene)
    {
        Scene = scene;
    }
}
